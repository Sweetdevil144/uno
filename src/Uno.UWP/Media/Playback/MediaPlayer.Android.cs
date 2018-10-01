using System;
using Android.App;
using Android.Widget;
using Uno.Media.Playback;
using Windows.Media.Core;
using Android.Media;
using Android.OS;
using Java.Lang;
using Java.Util.Concurrent;
using Uno.Extensions;
using Android.Content.Res;
using System.IO;
using Android.Content;
using Android.Views;
using Android.Graphics;
using Android.Runtime;
using Uno.Logging;
using AndroidMediaPlayer = Android.Media.MediaPlayer;

namespace Windows.Media.Playback
{
	public partial class MediaPlayer :
		Java.Lang.Object,
		ISurfaceHolderCallback,
		AndroidMediaPlayer.IOnCompletionListener,
		AndroidMediaPlayer.IOnErrorListener,
		AndroidMediaPlayer.IOnPreparedListener,
		AndroidMediaPlayer.IOnSeekCompleteListener,
		AndroidMediaPlayer.IOnBufferingUpdateListener,
		View.IOnLayoutChangeListener
	{
		private AndroidMediaPlayer _player;

		private bool _isPlayRequested = false;
		private bool _isPlayerPrepared = false;
		private bool _hasValidHolder = false;
		private VideoStrech _currentStretch = VideoStrech.Uniform;
		private bool _isUpdatingStretch = false;

		private IScheduledExecutorService _executorService = Executors.NewSingleThreadScheduledExecutor();
		private IScheduledFuture _scheduledFuture;
		private AudioPlayerBroadcastReceiver _noisyAudioStreamReceiver;

		const string MsAppXScheme = "ms-appx";
		const string MsAppDataScheme = "ms-appdata";

		public virtual IVideoSurface RenderSurface { get; private set; } = new VideoSurface(Application.Context);

		private void Initialize()
		{
			((VideoSurface)RenderSurface).AddOnLayoutChangeListener(this);

			// Register intent to pause media when audio become noisy (unplugged headphones, for example) 
			_noisyAudioStreamReceiver = new AudioPlayerBroadcastReceiver(this);
			var intentFilter = new IntentFilter(AudioManager.ActionAudioBecomingNoisy);
			Application.Context.RegisterReceiver(_noisyAudioStreamReceiver, intentFilter);
		}

		#region Player Initialization

		private void TryDisposePlayer()
		{
			if (_player != null)
			{
				try
				{
					_isPlayRequested = false;
					_isPlayerPrepared = false;
					_player.Release();

					var surfaceView = RenderSurface as SurfaceView;
					surfaceView?.Holder?.RemoveCallback(this);
				}
				finally
				{
					_player?.Dispose();
					_player = null;
				}
			}
		}

		private void InitializePlayer()
		{
			_player = new AndroidMediaPlayer();
			var surfaceView = RenderSurface as SurfaceView;

			if (_hasValidHolder)
			{
				_player.SetDisplay(surfaceView.Holder);
				_player.SetScreenOnWhilePlaying(true);
			}
			else
			{
				surfaceView.Holder.AddCallback(this);
			}

			_player.SetOnErrorListener(this);
			_player.SetOnPreparedListener(this);
			_player.SetOnSeekCompleteListener(this);
			_player.SetOnBufferingUpdateListener(this);

			PlaybackSession.PlaybackStateChanged -= OnStatusChanged;
			PlaybackSession.PlaybackStateChanged += OnStatusChanged;
		}

		protected virtual void InitializeSource()
		{
			PlaybackSession.NaturalDuration = TimeSpan.Zero;
			PlaybackSession.PositionFromPlayer = TimeSpan.Zero;

			if (Source == null)
			{
				return;
			}

			try
			{
				// Reset player
				TryDisposePlayer();
				InitializePlayer();

				PlaybackSession.PlaybackState = MediaPlaybackState.Opening;
				SetVideoSource(((MediaSource)Source).Uri);

				_player.PrepareAsync();

				MediaOpened?.Invoke(this, null);
			}
			catch (global::System.Exception ex)
			{
				OnMediaFailed(ex);
			}
		}

		private void SetVideoSource(Uri uri)
		{
			if (!uri.IsAbsoluteUri || uri.Scheme == "")
			{
				uri = new Uri(MsAppXScheme + ":///" + uri.OriginalString.TrimStart("/"));
			}

			var isResource = uri.Scheme.Equals(MsAppXScheme, StringComparison.OrdinalIgnoreCase)
							|| uri.Scheme.Equals(MsAppDataScheme, StringComparison.OrdinalIgnoreCase);

			if (isResource)
			{
				var filename = global::System.IO.Path.GetFileName(uri.LocalPath);
				var afd = Application.Context.Assets.OpenFd(filename);
				_player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
				return;
			}

			if (uri.IsFile)
			{
				_player.SetDataSource(Application.Context, Android.Net.Uri.Parse(uri.PathAndQuery));
				return;
			}

			_player.SetDataSource(Application.Context, Android.Net.Uri.Parse(uri.ToString()));
		}

		#endregion

		private void OnStatusChanged(MediaPlaybackSession sender, object args)
		{
			CancelPlayingHandler();

			if ((MediaPlaybackState)args == MediaPlaybackState.Playing)
			{
				StartPlayingHandler();
			}
		}

		public virtual void Play()
		{
			if (Source == null || _player == null)
			{
				return;
			}

			try
			{
				// If we reached the end of media, we need to reset position to 0
				if (PlaybackSession.PlaybackState == MediaPlaybackState.None)
				{
					PlaybackSession.Position = TimeSpan.Zero;
				}

				_isPlayRequested = true;

				if (_isPlayerPrepared)
				{
					_player.Start();
					PlaybackSession.PlaybackState = MediaPlaybackState.Playing;
				}
			}
			catch (global::System.Exception ex)
			{
				OnMediaFailed(ex);
			}
		}

		private void CancelPlayingHandler()
		{
			_scheduledFuture?.Cancel(false);
		}

		private void StartPlayingHandler()
		{
			var handler = new Handler();
			var runnable = new Runnable(() => { handler.Post(OnPlaying); });
			if (!_executorService.IsShutdown)
			{
				_scheduledFuture = _executorService.ScheduleAtFixedRate(runnable, 100, 1000, TimeUnit.Milliseconds);
			}
		}

		private void OnPlaying()
		{
			PlaybackSession.PositionFromPlayer = Position;
		}

		public void OnPrepared(AndroidMediaPlayer mp)
		{
			PlaybackSession.NaturalDuration = TimeSpan.FromMilliseconds(_player.Duration);

			if (PlaybackSession.PlaybackState == MediaPlaybackState.Opening)
			{
				UpdateVideoStretch(_currentStretch);

				if (_isPlayRequested)
				{
					_player.Start();
					PlaybackSession.PlaybackState = MediaPlaybackState.Playing;
				}
				else
				{
					// To display first image of media when setting a new source. Otherwise, last image of previous source remains visible
					_player.Start();
					_player.Pause();
					_player.SeekTo(0);
				}
			}

			_isPlayerPrepared = true;
		}
		
		public bool OnError(AndroidMediaPlayer mp, MediaError what, int extra)
		{
			if (PlaybackSession.PlaybackState != MediaPlaybackState.None)
			{
				_player?.Stop();
				PlaybackSession.PlaybackState = MediaPlaybackState.None;
			}

			OnMediaFailed(message: $"MediaPlayer Error: {what}");
			return true;
		}

		public void OnCompletion(AndroidMediaPlayer mp)
		{
			MediaEnded?.Invoke(this, null);
		}

		private void OnMediaFailed(global::System.Exception ex = null, string message = null)
		{
			MediaFailed?.Invoke(this, new MediaPlayerFailedEventArgs()
			{
				Error = MediaPlayerError.Unknown,
				ExtendedErrorCode = ex,
				ErrorMessage = message ?? ex?.Message
			});

			PlaybackSession.PlaybackState = MediaPlaybackState.None;
		}

		public virtual void Pause()
		{
			if (PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
			{
				_player?.Pause();
				PlaybackSession.PlaybackState = MediaPlaybackState.Paused;
			}
		}

		public virtual void Stop()
		{
			if (PlaybackSession.PlaybackState == MediaPlaybackState.Playing || PlaybackSession.PlaybackState == MediaPlaybackState.Paused)
			{
				_player?.Pause(); // Do not call stop, otherwise player will need to be prepared again
				_player?.SeekTo(0);
				PlaybackSession.PlaybackState = MediaPlaybackState.None;
			}
		}

		private void ToggleMute()
		{
			if (IsMuted)
			{
				_player?.SetVolume(0, 0);
			}
			else
			{
				var volume = (float)Volume / 100;
				_player?.SetVolume(volume, volume);
			}
		}

		private void OnVolumeChanged()
		{
			var volume = (float)Volume / 100;
			_player?.SetVolume(volume, volume);
		}

		public virtual TimeSpan Position
		{
			get
			{
				return TimeSpan.FromMilliseconds(_player.CurrentPosition);
			}
			set
			{
				if (PlaybackSession.PlaybackState != MediaPlaybackState.None)
				{
					_player?.SeekTo((int)value.TotalMilliseconds, MediaPlayerSeekMode.Closest);
				}
			}
		}

		internal void UpdateVideoStretch(VideoStrech stretch)
		{
			if (_player != null && RenderSurface is SurfaceView surface && !_isUpdatingStretch)
			{
				try
				{
					_isUpdatingStretch = true;

					var parent = (View)surface.Parent;

					var width = parent.Width;
					var height = parent.Height;
					var parentRatio = (double)width / height;

					var videoWidth = _player.VideoWidth;
					var videoHeight = _player.VideoHeight;
					var ratio = (double)_player.VideoWidth / _player.VideoHeight;

					_currentStretch = stretch;

					switch (stretch)
					{
						case VideoStrech.Fill:
							var fillHeight = height != 0 ? height : width / ratio;
							surface.Layout(0, 0, width, (int)fillHeight);
							break;

						case VideoStrech.Uniform:
							if (parentRatio < ratio)
							{
								var uniformHeight = height - (width / ratio);
								surface.Layout(0, (int)(uniformHeight / 2), width, height - (int)(uniformHeight / 2));
							}
							else
							{
								var uniformWidth = width - (height * ratio);
								surface.Layout((int)(uniformWidth / 2), 0, width - (int)(uniformWidth / 2), height);
							}

							break;

						case VideoStrech.UniformToFill:
							if (parentRatio < ratio)
							{
								var uniformFillWidth = (height * ratio) - width;
								surface.Layout(-(int)(uniformFillWidth / 2), 0, width + (int)(uniformFillWidth / 2), height);
							}
							else
							{
								var uniformFillHeight = (width / ratio) - height;
								surface.Layout(0, -(int)(uniformFillHeight / 2), width, height + (int)(uniformFillHeight / 2));
							}

							break;

						case VideoStrech.None:
						default:
							var noneHeight = videoHeight - height;
							var nonewidth = videoWidth - width;
							surface.Layout(-nonewidth / 2, -noneHeight / 2, width + (nonewidth / 2), height + (noneHeight / 2));
							break;
					}
				}
				finally
				{
					_isUpdatingStretch = false;
				}
			}
		}

		public enum VideoStrech
		{
			Uniform,
			Fill,
			None,
			UniformToFill
		}

		#region ISurfaceHolderCallback implementation

		public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
		{
			UpdateVideoStretch(_currentStretch);
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			_player.SetDisplay(holder);
			_player.SetScreenOnWhilePlaying(true);
			_hasValidHolder = true;

			UpdateVideoStretch(_currentStretch);
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{
			_hasValidHolder = false;
		}

		#endregion

		#region AndroidMediaPlayer.IOnSeekCompleteListener implementation

		public void OnSeekComplete(AndroidMediaPlayer mp)
		{
			SeekCompleted?.Invoke(this, null);
		}

		#endregion

		#region AndroidMediaPlayer.IOnBufferingUpdateListener implementation

		public void OnBufferingUpdate(AndroidMediaPlayer mp, int percent)
		{
			PlaybackSession.BufferingProgress = percent;
		}

		public void OnLayoutChange(View v, int left, int top, int right, int bottom, int oldLeft, int oldTop, int oldRight, int oldBottom)
		{
			UpdateVideoStretch(_currentStretch);
		}

		#endregion
	}
}

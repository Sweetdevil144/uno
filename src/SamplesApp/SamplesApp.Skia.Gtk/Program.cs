﻿using System;
using GLib;
using SkiaSharp;
using Gtk;
using Uno.Foundation.Extensibility;
using System.Threading;
using Uno.UI.Runtime.Skia;
using Uno.Media.Playback;
using Windows.UI.Xaml.Controls;

namespace SkiaSharpExample
{
	class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{
			SamplesApp.App.ConfigureFilters(); // Enable tracing of the host

			ApiExtensibility.Register(typeof(IMediaPlayerExtension), o => new Uno.UI.Media.MediaPlayerExtension(o));
			ApiExtensibility.Register(typeof(IMediaPlayerPresenterExtension), o => new Uno.UI.Media.MediaPlayerPresenterExtension(o));

			ExceptionManager.UnhandledException += delegate (UnhandledExceptionArgs expArgs)
			{
				Console.WriteLine("GLIB UNHANDLED EXCEPTION" + expArgs.ExceptionObject.ToString());
				expArgs.ExitApplication = true;
			};

			var host = new GtkHost(() => new SamplesApp.App());
			host.RenderSurfaceType = RenderSurfaceType.Software;

			host.Run();
		}
	}
}

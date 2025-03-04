﻿#nullable enable
using Windows.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windows.UI.Xaml.Shapes
{
	public partial class Path
	{
		#region Data

		public Geometry? Data
		{
			get => (Geometry)this.GetValue(DataProperty);
			set => this.SetValue(DataProperty, value);
		}

		public static DependencyProperty DataProperty { get; } =
			DependencyProperty.Register(
				"Data",
				typeof(Geometry),
				typeof(Path),
				new FrameworkPropertyMetadata(
					defaultValue: null,
					options: FrameworkPropertyMetadataOptions.ValueInheritsDataContext | FrameworkPropertyMetadataOptions.LogicalChild | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange
				)
			);

		#endregion

	}
}

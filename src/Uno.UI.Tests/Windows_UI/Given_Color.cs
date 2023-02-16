﻿using Windows.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uno.UI.Tests.Windows_UI
{
	[TestClass]
	public class Given_Color
	{
		[TestMethod]
		public void When_UsingToString()
		{
			var c = Colors.FromARGB(1, 2, 3, 4);
			Assert.AreEqual("#01020304", c.ToString());
			var color = new Color() { A = 255, R = 0, G = 128, B = 255 };

			Assert.AreEqual("#FF0080FF", color.ToString());
		}

		[TestMethod]
		public void All()
		{
			Assert.AreEqual("#FFF0F8FF", Colors.AliceBlue.ToString(), "AliceBlue");
			Assert.AreEqual("#FFFAEBD7", Colors.AntiqueWhite.ToString(), "AntiqueWhite");
			Assert.AreEqual("#FF00FFFF", Colors.Aqua.ToString(), "Aqua");
			Assert.AreEqual("#FF7FFFD4", Colors.Aquamarine.ToString(), "Aquamarine");
			Assert.AreEqual("#FFF0FFFF", Colors.Azure.ToString(), "Azure");
			Assert.AreEqual("#FFF5F5DC", Colors.Beige.ToString(), "Beige");
			Assert.AreEqual("#FFFFE4C4", Colors.Bisque.ToString(), "Bisque");
			Assert.AreEqual("#FF000000", Colors.Black.ToString(), "Black");
			Assert.AreEqual("#FFFFEBCD", Colors.BlanchedAlmond.ToString(), "BlanchedAlmond");
			Assert.AreEqual("#FF0000FF", Colors.Blue.ToString(), "Blue");
			Assert.AreEqual("#FF8A2BE2", Colors.BlueViolet.ToString(), "BlueViolet");
			Assert.AreEqual("#FFA52A2A", Colors.Brown.ToString(), "Brown");
			Assert.AreEqual("#FFDEB887", Colors.BurlyWood.ToString(), "BurlyWood");
			Assert.AreEqual("#FF5F9EA0", Colors.CadetBlue.ToString(), "CadetBlue");
			Assert.AreEqual("#FF7FFF00", Colors.Chartreuse.ToString(), "Chartreuse");
			Assert.AreEqual("#FFD2691E", Colors.Chocolate.ToString(), "Chocolate");
			Assert.AreEqual("#FFFF7F50", Colors.Coral.ToString(), "Coral");
			Assert.AreEqual("#FF6495ED", Colors.CornflowerBlue.ToString(), "CornflowerBlue");
			Assert.AreEqual("#FFFFF8DC", Colors.Cornsilk.ToString(), "Cornsilk");
			Assert.AreEqual("#FFDC143C", Colors.Crimson.ToString(), "Crimson");
			Assert.AreEqual("#FF00FFFF", Colors.Cyan.ToString(), "Cyan");
			Assert.AreEqual("#FF00008B", Colors.DarkBlue.ToString(), "DarkBlue");
			Assert.AreEqual("#FF008B8B", Colors.DarkCyan.ToString(), "DarkCyan");
			Assert.AreEqual("#FFB8860B", Colors.DarkGoldenrod.ToString(), "DarkGoldenrod");
			Assert.AreEqual("#FFA9A9A9", Colors.DarkGray.ToString(), "DarkGray");
			Assert.AreEqual("#FF006400", Colors.DarkGreen.ToString(), "DarkGreen");
			Assert.AreEqual("#FFBDB76B", Colors.DarkKhaki.ToString(), "DarkKhaki");
			Assert.AreEqual("#FF8B008B", Colors.DarkMagenta.ToString(), "DarkMagenta");
			Assert.AreEqual("#FF556B2F", Colors.DarkOliveGreen.ToString(), "DarkOliveGreen");
			Assert.AreEqual("#FFFF8C00", Colors.DarkOrange.ToString(), "DarkOrange");
			Assert.AreEqual("#FF9932CC", Colors.DarkOrchid.ToString(), "DarkOrchid");
			Assert.AreEqual("#FF8B0000", Colors.DarkRed.ToString(), "DarkRed");
			Assert.AreEqual("#FFE9967A", Colors.DarkSalmon.ToString(), "DarkSalmon");
			Assert.AreEqual("#FF8FBC8F", Colors.DarkSeaGreen.ToString(), "DarkSeaGreen");
			Assert.AreEqual("#FF483D8B", Colors.DarkSlateBlue.ToString(), "DarkSlateBlue");
			Assert.AreEqual("#FF2F4F4F", Colors.DarkSlateGray.ToString(), "DarkSlateGray");
			Assert.AreEqual("#FF00CED1", Colors.DarkTurquoise.ToString(), "DarkTurquoise");
			Assert.AreEqual("#FF9400D3", Colors.DarkViolet.ToString(), "DarkViolet");
			Assert.AreEqual("#FFFF1493", Colors.DeepPink.ToString(), "DeepPink");
			Assert.AreEqual("#FF00BFFF", Colors.DeepSkyBlue.ToString(), "DeepSkyBlue");
			Assert.AreEqual("#FF696969", Colors.DimGray.ToString(), "DimGray");
			Assert.AreEqual("#FF1E90FF", Colors.DodgerBlue.ToString(), "DodgerBlue");
			Assert.AreEqual("#FFB22222", Colors.Firebrick.ToString(), "Firebrick");
			Assert.AreEqual("#FFFFFAF0", Colors.FloralWhite.ToString(), "FloralWhite");
			Assert.AreEqual("#FF228B22", Colors.ForestGreen.ToString(), "ForestGreen");
			Assert.AreEqual("#FFFF00FF", Colors.Fuchsia.ToString(), "Fuchsia");
			Assert.AreEqual("#FFDCDCDC", Colors.Gainsboro.ToString(), "Gainsboro");
			Assert.AreEqual("#FFF8F8FF", Colors.GhostWhite.ToString(), "GhostWhite");
			Assert.AreEqual("#FFFFD700", Colors.Gold.ToString(), "Gold");
			Assert.AreEqual("#FFDAA520", Colors.Goldenrod.ToString(), "Goldenrod");
			Assert.AreEqual("#FF808080", Colors.Gray.ToString(), "Gray");
			Assert.AreEqual("#FF008000", Colors.Green.ToString(), "Green");
			Assert.AreEqual("#FFADFF2F", Colors.GreenYellow.ToString(), "GreenYellow");
			Assert.AreEqual("#FFF0FFF0", Colors.Honeydew.ToString(), "Honeydew");
			Assert.AreEqual("#FFFF69B4", Colors.HotPink.ToString(), "HotPink");
			Assert.AreEqual("#FFCD5C5C", Colors.IndianRed.ToString(), "IndianRed");
			Assert.AreEqual("#FF4B0082", Colors.Indigo.ToString(), "Indigo");
			Assert.AreEqual("#FFFFFFF0", Colors.Ivory.ToString(), "Ivory");
			Assert.AreEqual("#FFF0E68C", Colors.Khaki.ToString(), "Khaki");
			Assert.AreEqual("#FFE6E6FA", Colors.Lavender.ToString(), "Lavender");
			Assert.AreEqual("#FFFFF0F5", Colors.LavenderBlush.ToString(), "LavenderBlush");
			Assert.AreEqual("#FF7CFC00", Colors.LawnGreen.ToString(), "LawnGreen");
			Assert.AreEqual("#FFFFFACD", Colors.LemonChiffon.ToString(), "LemonChiffon");
			Assert.AreEqual("#FFADD8E6", Colors.LightBlue.ToString(), "LightBlue");
			Assert.AreEqual("#FFF08080", Colors.LightCoral.ToString(), "LightCoral");
			Assert.AreEqual("#FFE0FFFF", Colors.LightCyan.ToString(), "LightCyan");
			Assert.AreEqual("#FFFAFAD2", Colors.LightGoldenrodYellow.ToString(), "LightGoldenrodYellow");
			Assert.AreEqual("#FFD3D3D3", Colors.LightGray.ToString(), "LightGray");
			Assert.AreEqual("#FF90EE90", Colors.LightGreen.ToString(), "LightGreen");
			Assert.AreEqual("#FFFFB6C1", Colors.LightPink.ToString(), "LightPink");
			Assert.AreEqual("#FFFFA07A", Colors.LightSalmon.ToString(), "LightSalmon");
			Assert.AreEqual("#FF20B2AA", Colors.LightSeaGreen.ToString(), "LightSeaGreen");
			Assert.AreEqual("#FF87CEFA", Colors.LightSkyBlue.ToString(), "LightSkyBlue");
			Assert.AreEqual("#FF778899", Colors.LightSlateGray.ToString(), "LightSlateGray");
			Assert.AreEqual("#FFB0C4DE", Colors.LightSteelBlue.ToString(), "LightSteelBlue");
			Assert.AreEqual("#FFFFFFE0", Colors.LightYellow.ToString(), "LightYellow");
			Assert.AreEqual("#FF00FF00", Colors.Lime.ToString(), "Lime");
			Assert.AreEqual("#FF32CD32", Colors.LimeGreen.ToString(), "LimeGreen");
			Assert.AreEqual("#FFFAF0E6", Colors.Linen.ToString(), "Linen");
			Assert.AreEqual("#FFFF00FF", Colors.Magenta.ToString(), "Magenta");
			Assert.AreEqual("#FF800000", Colors.Maroon.ToString(), "Maroon");
			Assert.AreEqual("#FF66CDAA", Colors.MediumAquamarine.ToString(), "MediumAquamarine");
			Assert.AreEqual("#FF0000CD", Colors.MediumBlue.ToString(), "MediumBlue");
			Assert.AreEqual("#FFBA55D3", Colors.MediumOrchid.ToString(), "MediumOrchid");
			Assert.AreEqual("#FF9370DB", Colors.MediumPurple.ToString(), "MediumPurple");
			Assert.AreEqual("#FF3CB371", Colors.MediumSeaGreen.ToString(), "MediumSeaGreen");
			Assert.AreEqual("#FF7B68EE", Colors.MediumSlateBlue.ToString(), "MediumSlateBlue");
			Assert.AreEqual("#FF00FA9A", Colors.MediumSpringGreen.ToString(), "MediumSpringGreen");
			Assert.AreEqual("#FF48D1CC", Colors.MediumTurquoise.ToString(), "MediumTurquoise");
			Assert.AreEqual("#FFC71585", Colors.MediumVioletRed.ToString(), "MediumVioletRed");
			Assert.AreEqual("#FF191970", Colors.MidnightBlue.ToString(), "MidnightBlue");
			Assert.AreEqual("#FFF5FFFA", Colors.MintCream.ToString(), "MintCream");
			Assert.AreEqual("#FFFFE4E1", Colors.MistyRose.ToString(), "MistyRose");
			Assert.AreEqual("#FFFFE4B5", Colors.Moccasin.ToString(), "Moccasin");
			Assert.AreEqual("#FFFFDEAD", Colors.NavajoWhite.ToString(), "NavajoWhite");
			Assert.AreEqual("#FF000080", Colors.Navy.ToString(), "Navy");
			Assert.AreEqual("#FFFDF5E6", Colors.OldLace.ToString(), "OldLace");
			Assert.AreEqual("#FF808000", Colors.Olive.ToString(), "Olive");
			Assert.AreEqual("#FF6B8E23", Colors.OliveDrab.ToString(), "OliveDrab");
			Assert.AreEqual("#FFFFA500", Colors.Orange.ToString(), "Orange");
			Assert.AreEqual("#FFFF4500", Colors.OrangeRed.ToString(), "OrangeRed");
			Assert.AreEqual("#FFDA70D6", Colors.Orchid.ToString(), "Orchid");
			Assert.AreEqual("#FFEEE8AA", Colors.PaleGoldenrod.ToString(), "PaleGoldenrod");
			Assert.AreEqual("#FF98FB98", Colors.PaleGreen.ToString(), "PaleGreen");
			Assert.AreEqual("#FFAFEEEE", Colors.PaleTurquoise.ToString(), "PaleTurquoise");
			Assert.AreEqual("#FFDB7093", Colors.PaleVioletRed.ToString(), "PaleVioletRed");
			Assert.AreEqual("#FFFFEFD5", Colors.PapayaWhip.ToString(), "PapayaWhip");
			Assert.AreEqual("#FFFFDAB9", Colors.PeachPuff.ToString(), "PeachPuff");
			Assert.AreEqual("#FFCD853F", Colors.Peru.ToString(), "Peru");
			Assert.AreEqual("#FFFFC0CB", Colors.Pink.ToString(), "Pink");
			Assert.AreEqual("#FFDDA0DD", Colors.Plum.ToString(), "Plum");
			Assert.AreEqual("#FFB0E0E6", Colors.PowderBlue.ToString(), "PowderBlue");
			Assert.AreEqual("#FF800080", Colors.Purple.ToString(), "Purple");
			Assert.AreEqual("#FFFF0000", Colors.Red.ToString(), "Red");
			Assert.AreEqual("#FFBC8F8F", Colors.RosyBrown.ToString(), "RosyBrown");
			Assert.AreEqual("#FF4169E1", Colors.RoyalBlue.ToString(), "RoyalBlue");
			Assert.AreEqual("#FF8B4513", Colors.SaddleBrown.ToString(), "SaddleBrown");
			Assert.AreEqual("#FFFA8072", Colors.Salmon.ToString(), "Salmon");
			Assert.AreEqual("#FFF4A460", Colors.SandyBrown.ToString(), "SandyBrown");
			Assert.AreEqual("#FF2E8B57", Colors.SeaGreen.ToString(), "SeaGreen");
			Assert.AreEqual("#FFFFF5EE", Colors.SeaShell.ToString(), "SeaShell");
			Assert.AreEqual("#FFA0522D", Colors.Sienna.ToString(), "Sienna");
			Assert.AreEqual("#FFC0C0C0", Colors.Silver.ToString(), "Silver");
			Assert.AreEqual("#FF87CEEB", Colors.SkyBlue.ToString(), "SkyBlue");
			Assert.AreEqual("#FF6A5ACD", Colors.SlateBlue.ToString(), "SlateBlue");
			Assert.AreEqual("#FF708090", Colors.SlateGray.ToString(), "SlateGray");
			Assert.AreEqual("#FFFFFAFA", Colors.Snow.ToString(), "Snow");
			Assert.AreEqual("#FF00FF7F", Colors.SpringGreen.ToString(), "SpringGreen");
			Assert.AreEqual("#FF4682B4", Colors.SteelBlue.ToString(), "SteelBlue");
			Assert.AreEqual("#FFD2B48C", Colors.Tan.ToString(), "Tan");
			Assert.AreEqual("#FF008080", Colors.Teal.ToString(), "Teal");
			Assert.AreEqual("#FFD8BFD8", Colors.Thistle.ToString(), "Thistle");
			Assert.AreEqual("#FFFF6347", Colors.Tomato.ToString(), "Tomato");
			Assert.AreEqual("#00FFFFFF", Colors.Transparent.ToString(), "Transparent");
			Assert.AreEqual("#FF40E0D0", Colors.Turquoise.ToString(), "Turquoise");
			Assert.AreEqual("#FFEE82EE", Colors.Violet.ToString(), "Violet");
			Assert.AreEqual("#FFF5DEB3", Colors.Wheat.ToString(), "Wheat");
			Assert.AreEqual("#FFFFFFFF", Colors.White.ToString(), "White");
			Assert.AreEqual("#FFF5F5F5", Colors.WhiteSmoke.ToString(), "WhiteSmoke");
			Assert.AreEqual("#FFFFFF00", Colors.Yellow.ToString(), "Yellow");
			Assert.AreEqual("#FF9ACD32", Colors.YellowGreen.ToString(), "YellowGreen");
		}
	}
}

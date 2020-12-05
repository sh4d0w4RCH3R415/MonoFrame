using System.Drawing;

namespace MonoFrame.MaterialDesign.General
{
	public static class Extensions
	{
		public static bool HasProperty(this object objectToCheck, string propertyName)
		{
			try
			{
				return objectToCheck.GetType().GetProperty(propertyName) != null;
			}
			catch
			{
				return true;
			}
		}
		public static string ToSecureString(this string plainString)
		{
			if (plainString == null)
				return null;
			string str = "";
			for (uint index = 0; (long)index < (long)plainString.Length; ++index)
				str += "●";
			return str;
		}
		public static Color ToColor(this int argb)
		{
			return Color.FromArgb((argb & 16711680) >> 16, (argb & 65280) >> 8, argb & byte.MaxValue);
		}
		public static Color RemoveAlpha(this Color color)
		{
			return Color.FromArgb(color.R, color.G, color.B);
		}
		public static int PercentageToColorComponent(this int percentage)
		{
			return (int)(percentage / 100.0 * byte.MaxValue);
		}
		public static Brush ToBrush(this Color color)
		{
			return new SolidBrush(color);
		}
		public static Color ExtractColor(this SolidBrush brush)
		{
			return brush.Color;
		}
	}
}

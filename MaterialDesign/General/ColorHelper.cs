using System;
using System.Drawing;

namespace MonoFrame.MaterialDesign.General
{
	public static class ColorHelper
	{
		public static Color Lighten(this Color color, float percent)
		{
			float _percent = percent / 100;
			float brightness = color.GetBrightness();
			float lighting = brightness + brightness * _percent;
			if (lighting > 1.0)
				lighting = 1f;
			else if (lighting <= 0.0)
				lighting = 0.1f;
			return FromHsl(color.A, color.GetHue(), color.GetSaturation(), lighting);
		}
		public static Color Darken(this Color color, float percent)
		{
			float _percent = percent / 100;
			float brightness = color.GetBrightness();
			float lighting = brightness - brightness * _percent;
			if (lighting > 1.0)
				lighting = 1f;
			else if (lighting <= 0.0)
				lighting = 0.0f;
			return FromHsl(color.A, color.GetHue(), color.GetSaturation(), lighting);
		}
		public static Color FromHsl(int alpha, float hue, float saturation, float lighting)
		{
			if (0 > alpha || byte.MaxValue < alpha)
				throw new ArgumentOutOfRangeException(nameof(alpha));
			if (0.0 > hue || 360.0 < hue)
				throw new ArgumentOutOfRangeException(nameof(hue));
			if (0.0 > saturation || 1.0 < saturation)
				throw new ArgumentOutOfRangeException(nameof(saturation));
			if (0.0 > lighting || 1.0 < lighting)
				throw new ArgumentOutOfRangeException(nameof(lighting));
			if (0.0 == saturation)
				return Color.FromArgb(alpha, Convert.ToInt32(lighting * byte.MaxValue), Convert.ToInt32(lighting * byte.MaxValue), Convert.ToInt32(lighting * byte.MaxValue));
			float num1, num2;
			if (0.5 < lighting)
			{
				num1 = lighting - lighting * saturation + saturation;
				num2 = lighting + lighting * saturation - saturation;
			}
			else
			{
				num1 = lighting + lighting * saturation;
				num2 = lighting - lighting * saturation;
			}
			int num3 = (int)Math.Floor(hue / 60.0);
			if (300.0 <= hue)
				hue -= 360f;
			hue /= 60f;
			hue -= 2f * (float)Math.Floor((num3 + 1.0) % 6.0 / 2.0);
			float num4 = num3 % 2 != 0 ? num2 - hue * (num1 - num2) : hue * (num1 - num2) + num2;
			int int_1 = Convert.ToInt32(num1 * byte.MaxValue);
			int int_2 = Convert.ToInt32(num4 * byte.MaxValue);
			int int_3 = Convert.ToInt32(num2 * byte.MaxValue);
			switch (num3)
			{
				case 1:
					return Color.FromArgb(alpha, int_2, int_1, int_3);
				case 2:
					return Color.FromArgb(alpha, int_3, int_1, int_2);
				case 3:
					return Color.FromArgb(alpha, int_3, int_2, int_1);
				case 4:
					return Color.FromArgb(alpha, int_2, int_3, int_1);
				case 5:
					return Color.FromArgb(alpha, int_1, int_3, int_2);
				default:
					return Color.FromArgb(alpha, int_1, int_2, int_3);
			}
		}
	}
}

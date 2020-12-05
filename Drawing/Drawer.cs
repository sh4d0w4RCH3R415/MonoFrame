using System;
using System.Drawing;

using MonoFrame.MaterialDesign.General;

namespace MonoFrame.Drawing
{
	public class Drawer
	{
		/// <summary>
		/// Gets the X from the specified point.
		/// </summary>
		/// <param name="point">The location specified.</param>
		/// <returns>The x co-ordinate from the specified point.</returns>
		public static float GetX(Point point)
		{
			return point.X;
		}

		/// <summary>
		/// Gets the Y from the specified point.
		/// </summary>
		/// <param name="point">The location specified.</param>
		/// <returns>The y co-ordinate from the specified point.</returns>
		public static float GetY(Point point)
		{
			return point.Y;
		}

		/// <summary>
		/// Gets the Width from the specified size.
		/// </summary>
		/// <param name="size">The size specified.</param>
		/// <returns>The width from the specified size.</returns>
		public static float GetWidth(Size size)
		{
			return size.Width;
		}

		/// <summary>
		/// Gets the Height from the specified size.
		/// </summary>
		/// <param name="size">The size specified.</param>
		/// <returns>The height from the specified size.</returns>
		public static float GetHeight(Size size)
		{
			return size.Height;
		}

		/// <summary>
		/// Draws the specified image <em>unscaled</em> on the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="image">The <see cref="Image"/> to draw.</param>
		/// <param name="X">The x co-ordinate of the image.</param>
		/// <param name="Y">The y co-ordinate of the image.</param>
		public static void DrawImage(Graphics graphics, Image image, int X, int Y)
		{
			graphics.DrawImageUnscaled(image, X, Y);
		}

		/// <summary>
		/// Draws the specified image <em>unscaled</em> on the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="image">The <see cref="Image"/> to draw.</param>
		/// <param name="point">The location of which to draw the image.</param>
		public static void DrawImage(Graphics graphics, Image image, Point point)
		{
			graphics.DrawImageUnscaled(image, point.X, point.Y);
		}

		/// <summary>
		/// Draws the specified image <em>unscaled</em> on the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="image">The <see cref="Image"/> to draw.</param>
		/// <param name="X">The x co-ordinate of the image.</param>
		/// <param name="Y">The y co-ordinate of the image.</param>
		/// <param name="Width">The width of the image.</param>
		/// <param name="Height">The height of the image.</param>
		public static void DrawImageScaled(Graphics graphics, Image image, float X, float Y, float Width, float Height)
		{
			graphics.DrawImage(image, X, Y, Width, Height);
		}

		/// <summary>
		/// Draws the specified image <em>unscaled</em> on the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="image">The <see cref="Image"/> to draw.</param>
		/// <param name="point">The location of which to draw the image.</param>
		/// <param name="size">The size in which the image will be drawn.</param>
		public static void DrawImageScaled(Graphics graphics, Image image, Point point, Size size)
		{
			graphics.DrawImage(image, point.X, point.Y, size.Width, size.Height);
		}

		/// <summary>
		/// Draws the specified image <em>unscaled</em> on the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="image">The <see cref="Image"/> to draw.</param>
		/// <param name="point">The location of which to draw the image.</param>
		/// <param name="size">The size in which the image will be drawn.</param>
		public static void DrawImageScaled(Graphics graphics, Image image, PointF point, SizeF size)
		{
			graphics.DrawImage(image, point.X, point.Y, size.Width, size.Height);
		}

		/// <summary>
		/// Draws a border within the region of the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="thickness">The thickness of the border. Minimum: 1 | Maximum: 5</param>
		/// <param name="borderColor">The color of the border.</param>
		public static void DrawBorder(Graphics graphics, float thickness, Color borderColor)
		{
			if (thickness >= 1)
			{
				if (thickness <= 5)
				{
					graphics.FillRectangle(new SolidBrush(borderColor), 0, 0, graphics.VisibleClipBounds.Width, thickness); // top
					graphics.FillRectangle(new SolidBrush(borderColor), 0, 0, thickness, graphics.VisibleClipBounds.Height); // left
					graphics.FillRectangle(new SolidBrush(borderColor), 0, graphics.VisibleClipBounds.Height - thickness, graphics.VisibleClipBounds.Width, thickness); // bottom
					graphics.FillRectangle(new SolidBrush(borderColor), graphics.VisibleClipBounds.Width - thickness, 0, thickness, graphics.VisibleClipBounds.Height); // right
				}
				else if (thickness > 5)
				{
					throw new ArgumentOutOfRangeException("thickness");
				}
			}
			else if (thickness < 1)
			{
				throw new ArgumentOutOfRangeException("thickness");
			}
		}

		/// <summary>
		/// Draws a border within the region of the specified <see cref="Graphics"/> board.
		/// </summary>
		/// <param name="graphics">The <see cref="Graphics"/> board.</param>
		/// <param name="thickness">The thickness of the border. Minimum: 1 | Maximum: 5</param>
		/// <param name="borderColor">The color of the border.</param>
		/// <param name="X">The x co-ordinate of the border.</param>
		/// <param name="Y">The y co-ordinate of the border.</param>
		public static void DrawBorderCentered(Graphics graphics, float thickness, Color borderColor, float X, float Y)
		{
			if (thickness >= 1)
			{
				if (thickness <= 5)
				{
					graphics.FillRectangle(new SolidBrush(borderColor), X, Y, graphics.VisibleClipBounds.Width - (X * 2), thickness); // top
					graphics.FillRectangle(new SolidBrush(borderColor), X, Y, thickness, graphics.VisibleClipBounds.Height - (Y * 2)); // left
					graphics.FillRectangle(new SolidBrush(borderColor), X, graphics.VisibleClipBounds.Height - thickness - Y, graphics.VisibleClipBounds.Width - (X * 2), thickness); // bottom
					graphics.FillRectangle(new SolidBrush(borderColor), graphics.VisibleClipBounds.Width - thickness - X, Y, thickness, graphics.VisibleClipBounds.Height - (Y * 2)); // right
				}
				else if (thickness > 5)
				{
					throw new ArgumentOutOfRangeException("thickness");
				}
			}
			else if (thickness < 1)
			{
				throw new ArgumentOutOfRangeException("thickness");
			}
		}
	}
}

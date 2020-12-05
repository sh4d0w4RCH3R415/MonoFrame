﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace MonoFrame.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.Label))]
	public class Label : System.Windows.Forms.Label
	{
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			Size textSize = new Size(TextRenderer.MeasureText(Text, Font).Width + 4, TextRenderer.MeasureText(Text, Font).Height + 2);
			if (Width < textSize.Width) Width = textSize.Width;
			if (Height < textSize.Height) Height = textSize.Height;
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			base.OnResize(e);
		}
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			base.OnResize(e);
			Invalidate();
		}

		public Label()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			Font = new Font("Roboto", 12f);
			TextAlign = ContentAlignment.MiddleCenter;
		}

		private StringFormat GetAlignment(ContentAlignment ca)
		{
			switch (ca)
			{
				case ContentAlignment.TopLeft:
					return new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Near
					};
				case ContentAlignment.TopCenter:
					return new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Near
					};
				case ContentAlignment.TopRight:
					return new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Near
					};
				case ContentAlignment.MiddleLeft:
					return new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Center
					};
				case ContentAlignment.MiddleCenter:
					return new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
				case ContentAlignment.MiddleRight:
					return new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Center
					};
				case ContentAlignment.BottomLeft:
					return new StringFormat
					{
						Alignment = StringAlignment.Near,
						LineAlignment = StringAlignment.Far
					};
				case ContentAlignment.BottomCenter:
					return new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Far
					};
				case ContentAlignment.BottomRight:
					return new StringFormat
					{
						Alignment = StringAlignment.Far,
						LineAlignment = StringAlignment.Far
					};
			}
			return null;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;
			gfx.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));
			gfx.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(0, 2, Width, Height),
				GetAlignment(TextAlign));
		}
	}
}

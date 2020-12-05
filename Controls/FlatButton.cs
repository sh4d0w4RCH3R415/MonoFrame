using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using MonoFrame.MaterialDesign.General;

namespace MonoFrame.Controls
{
	[ToolboxBitmap(@"V:\Icons & Images\ControlIconBitmaps\flatbutton.png")]
	public class FlatButton : Control
	{
		private bool clickAnimations = false;

		[Category("Behavior")]
		public bool ClickAnimations
		{
			get { return clickAnimations; }
			set { clickAnimations = value; Invalidate(); }
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (clickAnimations)
			{
				BackColor = Color.FromArgb(
					BackColor.R >= BackColor.Darken(23).R ? BackColor.Darken(23).R : BackColor.Lighten(23).R,
					BackColor.G >= BackColor.Darken(23).G ? BackColor.Darken(23).G : BackColor.Lighten(23).G,
					BackColor.B >= BackColor.Darken(23).B ? BackColor.Darken(23).B : BackColor.Lighten(23).B
					);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (clickAnimations)
			{
				BackColor = Color.FromArgb(
					BackColor.R <= BackColor.Lighten(30).R ? BackColor.Lighten(30).R : BackColor.Darken(30).R,
					BackColor.G <= BackColor.Lighten(30).G ? BackColor.Lighten(30).G : BackColor.Darken(30).G,
					BackColor.B <= BackColor.Lighten(30).B ? BackColor.Lighten(30).B : BackColor.Darken(30).B
					);
			}
		}
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

		public FlatButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			Size = new Size(100, 35);
			Font = new Font("Roboto", 12f);

			BackColor = Color.FromArgb(45, 47, 49);
			ForeColor = SystemColors.Window;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;
			gfx.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(1, 2, Width, Height),
				new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
	}
}

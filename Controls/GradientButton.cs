using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MonoFrame.Controls
{
	[ToolboxBitmap(@"V:\Icons & Images\ControlIconBitmaps\gradientbutton.png")]
	public class GradientButton : Control
	{
		private Color gradient1 = Color.FromArgb(36, 146, 246);
		private Color gradient2 = Color.FromArgb(36, 246, 36);

		[Category("Appearance")]
		public Color Gradient1
		{
			get { return gradient1; }
			set { gradient1 = value; Invalidate(); }
		}

		[Category("Appearance")]
		public Color Gradient2
		{
			get { return gradient2; }
			set { gradient2 = value; Invalidate(); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Empty;

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

		public GradientButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
			DoubleBuffered = true;
			Size = new Size(138, 35);
			Font = new Font("Roboto", 12f);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var gfx = e.Graphics;
			LinearGradientBrush gradientBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), gradient1, gradient2, 45);
			gfx.FillRectangle(gradientBrush, new Rectangle(0, 0, Width, Height));
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(1, 1, Width, Height),
				new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}
	}
}

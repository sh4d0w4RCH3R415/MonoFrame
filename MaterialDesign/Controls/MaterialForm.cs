using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;

using MonoFrame.MaterialDesign.General;

namespace MonoFrame.MaterialDesign.Controls
{
	public partial class MaterialForm : Form
	{
		public MaterialForm()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
			InitializeComponent();
			DoubleBuffered = true;
			ForeColor = SystemColors.Window;
			Win32.CreateDropShadow(this, CreateParams);

			Font = new Font("Roboto", 14f);
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered => true;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => containerColor;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MaximizeBox => false;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MinimizeBox => false;

		[Browsable(false)]
		public new FormWindowState WindowState
		{
			get { return base.WindowState; }
			set { base.WindowState = value; Invalidate(); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new SizeGripStyle SizeGripStyle => SizeGripStyle.Hide;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Color TransparencyKey => Color.Empty;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool UseWaitCursor => false;

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new double Opacity
		{
			get { return base.Opacity; }
			set { base.Opacity = value; Invalidate(); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode => ImeMode.NoControl;

		private Rectangle header,
			container,
			left1,
			right1,
			left2,
			right2,
			top,
			topleft,
			topright,
			bottom,
			bottomleft,
			bottomright,
			minimize,
			maximize,
			close;
		private Image closeimg = close_inactive,
			maximizeimg = maximize_inactive,
			minimizeimg = minimize_inactive;
		private static readonly Image close_inactive = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Close-Inactive_32x32.png"),
			close_active = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Close-Active_32x32.png"),
			minimize_inactive = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Minimize-Inactive_32x32.png"),
			minimize_active = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Minimize-Active-Blue_32x32.png"),
			maximize_inactive = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Maximize-Inactive_32x32.png"),
			maximize_active = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Maximize-Active-Blue_32x32.png"),
			restore_inactive = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Restore-Inactive_32x32.png"),
			restore_active = Image.FromFile(@"V:\PNG Icons\Caption-Bar\32\Restore-Active-Blue_32x32.png");
		private const int HTCAPTION = 2,
			HTLEFT = 10,
			HTRIGHT = 11,
			HTTOP = 12,
			HTTOPLEFT = 13,
			HTTOPRIGHT = 14,
			HTBOTTOM = 15,
			HTBOTTOMLEFT = 16,
			HTBOTTOMRIGHT = 17;

		private Color headerColor = Color.FromArgb(62, 73, 99);
		private Color containerColor = Color.FromArgb(32, 43, 59);
		private bool canMinimize = true;
		private bool canMaximize = true;
		private bool canResize = true;
		private bool isHovering = false;

		private FormWindowState windowState;

		[Category("Appearance")]
		public Color HeaderColor
		{
			get { return headerColor; }
			set { headerColor = value; Invalidate(); }
		}

		[Category("Appearance")]
		public Color ContainerColor
		{
			get { return containerColor; }
			set { containerColor = value; Invalidate(); }
		}

		[Category("Appearance")]
		public bool CanMinimize
		{
			get { return canMinimize; }
			set
			{
				canMinimize = value;
				if (!value && canMaximize)
				{
					canMaximize = false;
				}
				Invalidate();
			}
		}

		[Category("Appearance")]
		public bool CanMaximize
		{
			get { return canMaximize; }
			set
			{
				if (canMinimize)
				{
					canMaximize = value;
					canResize = value;
				}
				Invalidate();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			var pos = e.Location;

			Win32.ReleaseCapture();

			if (!canResize)
			{
				if (left1.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else if (right1.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else if (topleft.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else if (top.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else if (topright.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else if (header.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else
				{
					return;
				}
			}
			else if (canResize)
			{
				if (left1.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTLEFT, 0);
				}
				else if (right1.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTRIGHT, 0);
				}
				else if (topleft.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTTOPLEFT, 0);
				}
				else if (top.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTTOP, 0);
				}
				else if (topright.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTTOPRIGHT, 0);
				}
				else if (left2.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTLEFT, 0);
				}
				else if (right2.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTRIGHT, 0);
				}
				else if (bottomleft.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTBOTTOMLEFT, 0);
				}
				else if (bottom.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTBOTTOM, 0);
				}
				else if (bottomright.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTBOTTOMRIGHT, 0);
				}
				else if (header.Contains(pos))
				{
					Win32.SendMessage(Handle, 161, HTCAPTION, 0);
				}
				else
				{
					return;
				}
			}

			if (close.Contains(pos))
			{
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					Opacity -= .025d;
					if (Opacity <= 0)
					{
						t.Enabled = false;
						Close();
					}
				};
				t.Start();
			}
			if (canMaximize)
			{
				if (maximize.Contains(pos))
				{
					if (WindowState == FormWindowState.Maximized)
					{
						Timer t = new Timer { Interval = 1 };
						t.Tick += delegate (object sender_, EventArgs e_)
						{
							Opacity -= .025d;
							if (Opacity <= 0)
							{
								t.Enabled = false;
								WindowState = FormWindowState.Normal;
								Timer t_ = new Timer { Interval = 1 };
								t_.Tick += delegate (object sender__, EventArgs e__)
								{
									Opacity += .025d;
									if (Opacity >= 1)
									{
										t_.Enabled = false;
										Opacity = 1;
									}
								};
								t_.Start();
							}
						};
						t.Start();
					}
					else if (WindowState == FormWindowState.Normal)
					{
						Timer t = new Timer { Interval = 1 };
						t.Tick += delegate (object sender_, EventArgs e_)
						{
							Opacity -= .025d;
							if (Opacity <= 0)
							{
								t.Enabled = false;
								WindowState = FormWindowState.Maximized;
								Timer t_ = new Timer { Interval = 1 };
								t_.Tick += delegate (object sender__, EventArgs e__)
								{
									Opacity += .025d;
									if (Opacity >= 1)
									{
										t_.Enabled = false;
										Opacity = 1;
									}
								};
								t_.Start();
							}
						};
						t.Start();
					}
				}
			}
			if (canMinimize)
			{
				if (minimize.Contains(pos))
				{
					Timer t = new Timer { Interval = 1 };
					t.Tick += delegate (object sender_, EventArgs e_)
					{
						Opacity -= .025d;
						if (Opacity <= 0)
						{
							t.Enabled = false;
							windowState = WindowState;
							WindowState = FormWindowState.Minimized;
						}
					};
					t.Start();
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			var pos = e.Location;

			if (!canResize)
			{
				if (left1.Contains(pos))
				{
					Cursor = Cursors.Arrow;
				}
				else if (right1.Contains(pos))
				{
					Cursor = Cursors.Arrow;
				}
				else if (topleft.Contains(pos))
				{
					Cursor = Cursors.Arrow;
				}
				else if (top.Contains(pos))
				{
					Cursor = Cursors.Arrow;
				}
				else if (topright.Contains(pos))
				{
					Cursor = Cursors.Arrow;
				}
				else if (bottomright.Contains(pos))
				{
					Cursor = Cursors.Arrow;
				}
				else
				{
					Cursor = Cursors.Arrow;
				}
			}
			else if (canResize)
			{
				if (left1.Contains(pos))
				{
					Cursor = Cursors.SizeWE;
				}
				else if (right1.Contains(pos))
				{
					Cursor = Cursors.SizeWE;
				}
				else if (topleft.Contains(pos))
				{
					Cursor = Cursors.SizeNWSE;
				}
				else if (top.Contains(pos))
				{
					Cursor = Cursors.SizeNS;
				}
				else if (topright.Contains(pos))
				{
					Cursor = Cursors.SizeNESW;
				}
				else if (left2.Contains(pos))
				{
					Cursor = Cursors.SizeWE;
				}
				else if (right2.Contains(pos))
				{
					Cursor = Cursors.SizeWE;
				}
				else if (bottomleft.Contains(pos))
				{
					Cursor = Cursors.SizeNESW;
				}
				else if (bottom.Contains(pos))
				{
					Cursor = Cursors.SizeNS;
				}
				else if (bottomright.Contains(pos))
				{
					Cursor = Cursors.SizeNWSE;
				}
				else
				{
					Cursor = Cursors.Arrow;
				}
			}

			if (close.Contains(pos))
			{
				closeimg = close_active;
				Invalidate();
			}
			else if (!close.Contains(pos))
			{
				closeimg = close_inactive;
				Invalidate();
			}

			if (canMaximize)
			{
				if (maximize.Contains(pos))
				{
					if (WindowState == FormWindowState.Maximized)
					{
						maximizeimg = restore_active;
						isHovering = true;
					}
					if (WindowState == FormWindowState.Normal)
					{
						maximizeimg = maximize_active;
						isHovering = true;
					}
					Invalidate();
				}
				else if (!maximize.Contains(pos))
				{
					if (WindowState == FormWindowState.Maximized)
					{
						maximizeimg = restore_inactive;
						isHovering = false;
					}
					if (WindowState == FormWindowState.Normal)
					{
						maximizeimg = maximize_inactive;
						isHovering = false;
					}
					Invalidate();
				}
			}

			if (canMinimize)
			{
				if (minimize.Contains(pos))
				{
					minimizeimg = minimize_active;
					Invalidate();
				}
				else if (!minimize.Contains(pos))
				{
					minimizeimg = minimize_inactive;
					Invalidate();
				}
			}
		}
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (isHovering)
			{
				if (WindowState == FormWindowState.Maximized) maximizeimg = restore_active;
				if (WindowState == FormWindowState.Normal) maximizeimg = maximize_active;
			}
			else if (!isHovering)
			{
				if (WindowState == FormWindowState.Maximized) maximizeimg = restore_inactive;
				if (WindowState == FormWindowState.Normal) maximizeimg = maximize_inactive;
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			left1 = new Rectangle(0, 6, 6, 40);
			left2 = new Rectangle(0, 46, 6, Height - 40);
			right1 = new Rectangle(Width - 6, 6, 6, 40);
			right2 = new Rectangle(Width - 6, 46, 6, Height - 40);
			topleft = new Rectangle(0, 0, 6, 6);
			top = new Rectangle(6, 0, Width - 12, 6);
			topright = new Rectangle(Width - 6, 0, 6, 6);
			bottomleft = new Rectangle(0, Height - 6, 6, 6);
			bottom = new Rectangle(6, Height - 6, Width - 12, 6);
			bottomright = new Rectangle(Width - 6, Height - 6, 6, 6);

			header = new Rectangle(6, 6, Width - 12, 40);
			container = new Rectangle(6, 46, Width - 12, Height - 40);

			Invalidate();
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			base.OnResize(e);
		}
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);

			if (Font.Size > 14f)
			{
				Font = new Font(Font.Name, 14f);
			}
			else if (Font.Size < 11f)
			{
				Font = new Font(Font.Name, 11f);
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var graphics = e.Graphics;

			var headerBrush = headerColor.ToBrush();
			var containerBrush = containerColor.ToBrush();

			left1 = new Rectangle(0, 6, 6, 40);
			left2 = new Rectangle(0, 46, 6, Height - 52);
			right1 = new Rectangle(Width - 6, 6, 6, 40);
			right2 = new Rectangle(Width - 6, 46, 6, Height - 52);
			topleft = new Rectangle(0, 0, 6, 6);
			top = new Rectangle(6, 0, Width - 12, 6);
			topright = new Rectangle(Width - 6, 0, 6, 6);
			bottomleft = new Rectangle(0, Height - 6, 6, 6);
			bottom = new Rectangle(6, Height - 6, Width - 12, 6);
			bottomright = new Rectangle(Width - 6, Height - 6, 6, 6);

			header = new Rectangle(6, 6, Width - 12, 40);
			container = new Rectangle(6, 46, Width - 12, Height - 40);

			close = new Rectangle(Width - 36, 11, 24, 24);
			if (canMaximize && canMinimize) maximize = new Rectangle(Width - 67, 11, 24, 24);
			if (canMinimize) minimize = new Rectangle(canMaximize ? Width - 99 : Width - 66, 11, 24, 24);

			graphics.FillRectangle(headerBrush, left1);
			graphics.FillRectangle(headerBrush, right1);
			graphics.FillRectangle(headerBrush, topleft);
			graphics.FillRectangle(headerBrush, top);
			graphics.FillRectangle(headerBrush, topright);

			graphics.FillRectangle(containerBrush, left2);
			graphics.FillRectangle(containerBrush, right2);
			graphics.FillRectangle(containerBrush, bottomleft);
			graphics.FillRectangle(containerBrush, bottom);
			graphics.FillRectangle(containerBrush, bottomright);

			graphics.FillRectangle(headerBrush, header);
			graphics.FillRectangle(containerBrush, container);

			graphics.DrawImage(closeimg, close);
			if (canMaximize && canMinimize) graphics.DrawImage(maximizeimg, maximize);
			if (canMinimize) graphics.DrawImage(minimizeimg, minimize);

			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			graphics.DrawString(Text, Font, ForeColor.ToBrush(),
				new Rectangle(header.X + 5, header.Y - 1, header.Width - 5, header.Height),
				new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			Opacity = 0;

			Timer t = new Timer { Interval = 1 };
			t.Tick += delegate (object sender_, EventArgs e_)
			{
				Opacity += .025d;
				if (Opacity >= 1)
				{
					t.Enabled = false;
					WindowState = windowState;
				}
			};
			t.Start();
		}
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			if (WindowState == FormWindowState.Minimized) Opacity = 0;
			Timer t = new Timer { Interval = 1 };
			t.Tick += delegate (object sender_, EventArgs e_)
			{
				WindowState = windowState;
				Opacity += .025d;
				if (Opacity >= 1)
				{
					t.Enabled = false;
					Opacity = 1;
				}
			};
			t.Start();
		}
	}
}

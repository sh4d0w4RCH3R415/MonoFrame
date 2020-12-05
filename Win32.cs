using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MonoFrame
{
	public class Win32
	{
		#region DropShadow
		private static bool m_aeroEnabled;
		private static readonly int cs_dropShadow = 0x00020000;

		private const string api = "dwmapi";

		[DllImport(api)]
		private static extern int DwmExtendFrameIntoClientArea(IntPtr handle, ref MARGINS pMarInset);

		[DllImport(api)]
		private static extern int DwmSetWindowAttribute(IntPtr handle, int attr, ref int attrValue, int attrSize);

		[DllImport(api)]
		private static extern int DwmIsCompositionEnabled(ref int pfEnabled);

		private struct MARGINS
		{
			public int LeftWidth,
				RightWidth,
				TopHeight,
				BottomHeight;
		}

		/// <summary>
		/// Adds the <see langword="CS_DROPSHADOW"/> ClassStyle to the specified <see cref="CreateParams"/> and returns it <see cref="CreateParams"/>.
		/// </summary>
		/// <param name="cp">The <see cref="CreateParams"/> to modify.</param>
		/// <returns>A <see cref="CreateParams"/> with the <see langword="CS_DROPSHADOW"/> ClassStyle.</returns>
		private static CreateParams GetParams(CreateParams cp)
		{
			m_aeroEnabled = CheckAeroEnabled();
			if (!m_aeroEnabled) cp.ClassStyle |= cs_dropShadow;
			return cp;
		}

		/// <summary>
		/// Checks to see if the user is on a Windows device.
		/// </summary>
		/// <returns>
		/// If so, returns <see langword="true"/>.<br/>
		/// If not, returns <see langword="false"/>.
		/// </returns>
		private static bool CheckAeroEnabled()
		{
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int enabled = 0;
				DwmIsCompositionEnabled(ref enabled);
				return (enabled == 1) ? true : false;
			}
			return false;
		}

		/// <summary>
		/// Adds the Windows <see langword="DropShadow"/> style to your control.
		/// </summary>
		/// <param name="c">The control to modify.</param>
		/// <param name="controlParams">The control's <see cref="CreateParams"/> to modify.</param>
		public static void CreateDropShadow(Control c, CreateParams controlParams)
		{
			controlParams = GetParams(controlParams);
			c.Paint += delegate (object sender, PaintEventArgs e)
			{
				var gfx = e.Graphics;
				if (m_aeroEnabled)
				{
					var v = 2;
					DwmSetWindowAttribute(c.Handle, 2, ref v, 4);
					MARGINS margins = new MARGINS
					{
						BottomHeight = -1,
						LeftWidth = -1,
						RightWidth = -1,
						TopHeight = -1,
					};
					DwmExtendFrameIntoClientArea(c.Handle, ref margins);
				}
			};
		}
		#endregion
		#region Object-Dragging
		private const string user = "user32";

		[DllImport(user)]
		public static extern bool ReleaseCapture();

		[DllImport(user)]
		public static extern int SendMessage(IntPtr handle, int msg, int wp, int lp);

		/// <summary>
		/// Allows the specified control: <see langword="dragger"/> to drag the control: <see langword="target"/>.
		/// </summary>
		/// <param name="dragger">The control that <see langword="drags"/> the target control.</param>
		/// <param name="target">The control to be dragged.</param>
		public static void AllowMouseDrag(Control dragger, Control target)
		{
			dragger.MouseDown += delegate (object sender, MouseEventArgs e)
			{
				ReleaseCapture();
				SendMessage(target.Handle, 161, 2, 0);
			};
		}
		#endregion
		#region Object-Resizing
		public static void Resize(Control c, Rectangle r, int htBorder)
		{
			c.MouseDown += delegate (object sender, MouseEventArgs e)
			{
				if (r.Contains(e.Location))
				{
					ReleaseCapture();
					SendMessage(c.Handle, 161, htBorder, 0);
				}
			};
		}
		public static void Resize(Control resizer, Control target, int htBorder)
		{
			resizer.MouseDown += delegate (object sender, MouseEventArgs e)
			{
				ReleaseCapture();
				SendMessage(target.Handle, 161, htBorder, 0);
			};
		}
		#endregion
		#region General
		public delegate void Method();
		public static void Wait(double Seconds, Method action)
		{
			Timer t = new Timer { Interval = (int)(Seconds * 1000) };
			t.Tick += delegate (object sender, EventArgs e)
			{
				t.Enabled = false;
				action.Invoke();
			};
			t.Start();
		}

		public static void SendMessage(string Text, bool Info)
		{
			MessageBox.Show(Text, "Message", MessageBoxButtons.OK, Info ? MessageBoxIcon.Information : MessageBoxIcon.None);
		}
		public static void SendMessage(string Text, string Title)
		{
			MessageBox.Show(Text, Title);
		}
		public static void SendMessage(string Text, string Title, MessageBoxButtons ButtonSet, bool Info)
		{
			MessageBox.Show(Text, Title, ButtonSet, Info ? MessageBoxIcon.Information : MessageBoxIcon.None);
		}
		public static void SendMessage(string Text, string Title, MessageBoxIcon Icon)
		{
			MessageBox.Show(Text, Title, MessageBoxButtons.OK, Icon);
		}
		public static void SendMessage(string Text, string Title, MessageBoxButtons ButtonSet, MessageBoxIcon Icon)
		{
			MessageBox.Show(Text, Title, ButtonSet, Icon);
		}
		public static void SendMessage(string Text, MessageBoxButtons ButtonSet, bool Info)
		{
			MessageBox.Show(Text, "Message", ButtonSet, Info ? MessageBoxIcon.Information : MessageBoxIcon.None);
		}
		public static void SendMessage(string Text, MessageBoxIcon Icon)
		{
			MessageBox.Show(Text, "Message", MessageBoxButtons.OK, Icon);
		}
		public static void SendMessage(string Text, MessageBoxButtons ButtonSet, MessageBoxIcon Icon)
		{
			MessageBox.Show(Text, "Message", ButtonSet, Icon);
		}
		#endregion
	}
}

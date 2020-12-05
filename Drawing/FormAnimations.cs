using System;
using System.Windows.Forms;

namespace MonoFrame.Drawing
{
	public class FormAnimations
	{
		private static FormWindowState windowState;
		private static double change = .025d;

		/// <summary>
		/// Generates fade in/out animations for the given <see cref="Form"/>'s focusing, minimizing, closing, and maximizing states.
		/// </summary>
		/// <param name="form">The <see cref="Form"/> to animate.</param>
		/// <param name="minimizer">The <see cref="Control"/> to add the "minimize animation" to.</param>
		/// <param name="closer">The <see cref="Control"/> to add the "close animation" to.</param>
		public static void GenerateAnimations(Form form, Control minimizer, Control closer)
		{
			windowState = form.WindowState;
			form.Shown += delegate (object sender, EventArgs e)
			{
				form.Opacity = 0;

				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.Opacity += change;
					if (form.Opacity >= 1)
					{
						t.Enabled = false;
						form.WindowState = windowState;
					}
				};
				t.Start();
			};
			minimizer.Click += delegate (object sender, EventArgs e)
			{
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.Opacity -= change;
					if (form.Opacity <= 0)
					{
						t.Enabled = false;
						windowState = form.WindowState;
						form.WindowState = FormWindowState.Minimized;
					}
				};
				t.Start();
			};
			closer.Click += delegate (object sender, EventArgs e)
			{
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.Opacity -= change;
					if (form.Opacity <= 0)
					{
						t.Enabled = false;
						form.Close();
					}
				};
				t.Start();
			};
			form.Activated += delegate (object sender, EventArgs e)
			{
				if (form.WindowState == FormWindowState.Minimized) form.Opacity = 0;
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.WindowState = windowState;
					form.Opacity += change;
					if (form.Opacity >= 1)
					{
						t.Enabled = false;
						form.Opacity = 1;
					}
				};
				t.Start();
			};
		}

		/// <summary>
		/// Generates fade in/out animations for the given <see cref="Form"/>'s focusing, minimizing, closing, and maximizing states.
		/// </summary>
		/// <param name="form">The <see cref="Form"/> to animate.</param>
		/// <param name="minimizer">The <see cref="Control"/> to add the "minimize animation" to.</param>
		/// <param name="closer">The <see cref="Control"/> to add the "close animation" to.</param>
		/// <param name="maximizer">The <see cref="Control"/> to add the "maximize/restore animation" to.</param>
		/// <param name="secondsToFinish">The amount of time in <see langword="seconds"/> for the animations to finish.</param>
		public static void GenerateAnimations(Form form, Control minimizer, Control maximizer, Control closer)
		{
			windowState = form.WindowState;
			form.Shown += delegate (object sender, EventArgs e)
			{
				form.Opacity = 0;

				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.Opacity += change;
					if (form.Opacity >= 1)
					{
						t.Enabled = false;
						form.WindowState = windowState;
					}
				};
				t.Start();
			};
			minimizer.Click += delegate (object sender, EventArgs e)
			{
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.Opacity -= change;
					if (form.Opacity <= 0)
					{
						t.Enabled = false;
						windowState = form.WindowState;
						form.WindowState = FormWindowState.Minimized;
					}
				};
				t.Start();
			};
			closer.Click += delegate (object sender, EventArgs e)
			{
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.Opacity -= change;
					if (form.Opacity <= 0)
					{
						t.Enabled = false;
						form.Close();
					}
				};
				t.Start();
			};
			maximizer.Click += delegate (object sender, EventArgs e)
			{
				if (form.WindowState == FormWindowState.Maximized)
				{
					Timer t = new Timer { Interval = 1 };
					t.Tick += delegate (object sender_, EventArgs e_)
					{
						form.Opacity -= change;
						if (form.Opacity <= 0)
						{
							t.Enabled = false;
							form.WindowState = FormWindowState.Normal;
							Timer t_ = new Timer { Interval = 1 };
							t_.Tick += delegate (object sender__, EventArgs e__)
							{
								form.Opacity += change;
								if (form.Opacity >= 1)
								{
									t_.Enabled = false;
									form.Opacity = 1;
								}
							};
							t_.Start();
						}
					};
					t.Start();
				}
				else if (form.WindowState == FormWindowState.Normal)
				{
					Timer t = new Timer { Interval = 1 };
					t.Tick += delegate (object sender_, EventArgs e_)
					{
						form.Opacity -= change;
						if (form.Opacity <= 0)
						{
							t.Enabled = false;
							form.WindowState = FormWindowState.Maximized;
							Timer t_ = new Timer { Interval = 1 };
							t_.Tick += delegate (object sender__, EventArgs e__)
							{
								form.Opacity += change;
								if (form.Opacity >= 1)
								{
									t_.Enabled = false;
									form.Opacity = 1;
								}
							};
							t_.Start();
						}
					};
					t.Start();
				}
			};
			form.Activated += delegate (object sender, EventArgs e)
			{
				if (form.WindowState == FormWindowState.Minimized) form.Opacity = 0;
				Timer t = new Timer { Interval = 1 };
				t.Tick += delegate (object sender_, EventArgs e_)
				{
					form.WindowState = windowState;
					form.Opacity += change;
					if (form.Opacity >= 1)
					{
						t.Enabled = false;
						form.Opacity = 1;
					}
				};
				t.Start();
			};
		}
	}
}

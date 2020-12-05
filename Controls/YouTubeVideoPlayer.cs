using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MonoFrame.Controls
{
	[ToolboxBitmap(typeof(WebBrowser))]
	public class YouTubeVideoPlayer : WebBrowser
	{
		private string videoId;

		public event EventHandler VideoIDChanged;
		protected virtual void OnVideoIDChanged(EventArgs e)
		{
			VideoIDChanged?.Invoke(this, e);
			LoadVideo(videoId);
			Invalidate();
		}

		public void LoadVideo(string videoId)
		{
			string html = "<html>\n" +
				"\t<head>\n" +
				"\t\t<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>\n" +
				"\t</head>\n" +
				"\t<body>\n" +
				"\t\t<iframe id='video' src='{0}' width='{1}' height='{2}' frameborder='0' allowfullscreen></iframe>\n" +
				"\t</body>\n" +
				"</html>";

			DocumentText = string.Format(html, string.Format(Url.OriginalString, videoId), Width - 21, Height - 21);
		}

		[Category("Behavior")]
		public string VideoID
		{
			get { return videoId; }
			set { videoId = value; OnVideoIDChanged(null); }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Uri Url => new Uri("https://www.youtube.com/embed/{0}");

		public YouTubeVideoPlayer()
		{
			Size = new Size(250, 225);
			DoubleBuffered = true;
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			Dock = DockStyle.None;
			ScriptErrorsSuppressed = true;
		}
	}
}

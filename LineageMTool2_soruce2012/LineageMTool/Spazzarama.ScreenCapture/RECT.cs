using System;
using System.Drawing;

namespace LineageMTool.Spazzarama.ScreenCapture
{
	[Serializable]
	internal struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;

		public Rectangle AsRectangle
		{
			get
			{
				return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
			}
		}

		public RECT(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

		public static RECT FromRectangle(Rectangle rect)
		{
			return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}

		public static RECT FromXYWH(int x, int y, int width, int height)
		{
			return new RECT(x, y, x + width, y + height);
		}
	}
}
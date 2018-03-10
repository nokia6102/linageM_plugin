using System;
using System.Runtime.CompilerServices;

namespace LineageMTool
{
	public class Config
	{
		public int ArrowSelectIndex
		{
			get;
			set;
		}

		public int BackHomeSelectIndex
		{
			get;
			set;
		}

		public int HealSelectIndex
		{
			get;
			set;
		}

		public Rect HpRect
		{
			get;
			set;
		}

		public int HpToMpSelectIndex
		{
			get;
			set;
		}

		public Rect MpRect
		{
			get;
			set;
		}

		public string numericUp1DownText
		{
			get;
			set;
		}

		public string numericUp2DownText
		{
			get;
			set;
		}

		public string numericUp3DownText
		{
			get;
			set;
		}

		public string numericUp4DownText
		{
			get;
			set;
		}

		public string numericUp6DownText
		{
			get;
			set;
		}

		public string numericUp7DownText
		{
			get;
			set;
		}

		public string numericUp8DownText
		{
			get;
			set;
		}

		public int OrangeSelectIndex
		{
			get;
			set;
		}

		public string RefreshTime
		{
			get;
			set;
		}

		public string SimulatorName
		{
			get;
			set;
		}

		public int Version
		{
			get;
			set;
		}

		public Config()
		{
			this.HpRect = new Rect();
			this.MpRect = new Rect();
			this.Version = 0;
		}
	}
}
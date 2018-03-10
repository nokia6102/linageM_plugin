using System;
using System.Runtime.CompilerServices;

namespace LineageMTool
{
	public class Config
	{
		public string ArrowHotKey
		{
			get;
			set;
		}

		public string BackHomeHotKey
		{
			get;
			set;
		}

		public int comboBoxCaptureSettingSelectIndex
		{
			get;
			set;
		}

		public string DetoxificationHotKey
		{
			get;
			set;
		}

		public string HealHpHotKey
		{
			get;
			set;
		}

		public Rect HpRect
		{
			get;
			set;
		}

		public string HpToMpHotKey
		{
			get;
			set;
		}

		public bool IsArrowHotKeyEnable
		{
			get;
			set;
		}

		public bool IsBackHomeHotKeyEnable
		{
			get;
			set;
		}

		public bool IsDetoxificationHotKeyEnable
		{
			get;
			set;
		}

		public bool IsHealHpHotKeyEnable
		{
			get;
			set;
		}

		public bool IsHpToMpHotKeyEnable
		{
			get;
			set;
		}

		public bool IsOrangeHotKeyEnable
		{
			get;
			set;
		}

		public int LineNotifyInterval
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

		public string OrangeHotKey
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

		public string Uid
		{
			get;
			set;
		}

		public System.Version Version
		{
			get;
			set;
		}

		public Config()
		{
			this.HpRect = new Rect();
			this.MpRect = new Rect();
			this.Version = new System.Version(1, 1);
		}
	}
}
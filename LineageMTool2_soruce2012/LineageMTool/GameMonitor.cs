using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace LineageMTool
{
	internal class GameMonitor
	{
		public PlayerInfo Player;

		public SimulatorInfo Simulator;

		public Config _config;

		public Action<PlayerInfo> PlayerInfoChangedNotify;

		public Action<Image> MonitorScreenChagedNotify;

		public Action<string, Color, string> MonitorStateNotify;

		public LineageMTool.State State
		{
			get;
			set;
		}

		public GameMonitor(Config config)
		{
			this.State = LineageMTool.State.Stop;
			this._config = config;
			this.Simulator = new SimulatorInfo(config);
			this.Player = new PlayerInfo(config, this.Simulator);
		}

		private void CheckAction(int hp, int mp)
		{
			if (this._config.IsBackHomeHotKeyEnable && hp < int.Parse(this._config.numericUp7DownText))
			{
				this.Simulator.SendMessage(this._config.BackHomeHotKey);
				Thread.Sleep(500);
				this.Simulator.SendMessage(this._config.BackHomeHotKey);
				Thread.Sleep(500);
				this.Simulator.SendMessage(this._config.BackHomeHotKey);
				this.Player.State = RoleState.BackHome;
				this.State = LineageMTool.State.Stop;
				return;
			}
			if (this._config.IsDetoxificationHotKeyEnable && this.Player.State == RoleState.Detoxification)
			{
				this.Simulator.SendMessage(this._config.DetoxificationHotKey);
			}
			if (this._config.IsOrangeHotKeyEnable && hp < int.Parse(this._config.numericUp8DownText))
			{
				this.Simulator.SendMessage(this._config.OrangeHotKey);
			}
			if (this._config.IsHealHpHotKeyEnable && hp < int.Parse(this._config.numericUp3DownText) && mp > int.Parse(this._config.numericUp4DownText))
			{
				this.Simulator.SendMessage(this._config.HealHpHotKey);
				return;
			}
			if (this._config.IsHpToMpHotKeyEnable && mp < int.Parse(this._config.numericUp1DownText) && hp > int.Parse(this._config.numericUp2DownText))
			{
				this.Simulator.SendMessage(this._config.HpToMpHotKey);
				return;
			}
			if (this._config.IsArrowHotKeyEnable && mp > int.Parse(this._config.numericUp6DownText))
			{
				for (int i = 0; i < 3; i++)
				{
					this.Simulator.SendMessage(this._config.ArrowHotKey);
				}
			}
		}

		public void Monitor()
		{
			try
			{
				try
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					while (this.State == LineageMTool.State.Run)
					{
						try
						{
							Image image = this.Simulator.GetImage((CaptureMode)this._config.comboBoxCaptureSettingSelectIndex);
							this.Player.CalculateHpPercent(image);
							this.Player.CalculateMpPercent(image);
							Action<Image> monitorScreenChagedNotify = this.MonitorScreenChagedNotify;
							if (monitorScreenChagedNotify != null)
							{
								monitorScreenChagedNotify(image);
							}
							else
							{
							}
							Action<PlayerInfo> playerInfoChangedNotify = this.PlayerInfoChangedNotify;
							if (playerInfoChangedNotify != null)
							{
								playerInfoChangedNotify(this.Player);
							}
							else
							{
							}
							if (this.Player.Hp == 0 && this.Player.Mp == 0)
							{
								this.Player.State = RoleState.Error;
								Action<string, Color, string> monitorStateNotify = this.MonitorStateNotify;
								if (monitorStateNotify != null)
								{
									monitorStateNotify("異常", Color.Red, "請確認組隊視窗是否開啟，以及XY範圍設定正確");
								}
								else
								{
								}
							}
							else if (this.Player.Hp != 0 || this.Player.Mp <= 0)
							{
								Action<string, Color, string> action = this.MonitorStateNotify;
								if (action != null)
								{
									action("正常", Color.Green, string.Empty);
								}
								else
								{
								}
								this.CheckAction(this.Player.Hp, this.Player.Mp);
							}
							else
							{
								this.Player.State = RoleState.Die;
							}
						}
						catch (Exception exception)
						{
						}
						double num = double.Parse(this._config.RefreshTime);
						if (num > 0)
						{
							Thread.Sleep((int)(num * 1000));
						}
						stopwatch.Restart();
					}
				}
				catch (Exception exception2)
				{
					Exception exception1 = exception2;
					Action<string, Color, string> monitorStateNotify1 = this.MonitorStateNotify;
					if (monitorStateNotify1 != null)
					{
						monitorStateNotify1("異常", Color.Red, string.Concat("異常發生，停止外掛，錯誤原因:", exception1.Message));
					}
					else
					{
					}
				}
			}
			finally
			{
				this.State = LineageMTool.State.Stop;
				Action<string, Color, string> action1 = this.MonitorStateNotify;
				if (action1 != null)
				{
					action1("待機", Color.DarkBlue, string.Empty);
				}
				else
				{
				}
			}
		}
	}
}
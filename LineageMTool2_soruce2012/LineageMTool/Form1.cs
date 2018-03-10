using LineageMTool.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineageMTool
{
	public class Form1 : Form
	{
		private Task _monitorTask;

		private string _configPath = "Config.json";

		private Version _version = new Version(1, 1);

		private About frmAbout;

		private LineMessage _lineMessage;

		private GameMonitor _gameMonitor;

		private Config _config = new Config();

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private Button button2;

		private Label label40;

		private Label label39;

		private Label label38;

		private Label labelError;

		private ListBox listBox1;

		private Label label34;

		private Label label35;

		private Label label36;

		private Label label37;

		private Label label32;

		private Label label33;

		private Label label30;

		private Label label31;

		private Label label29;

		private Label label28;

		private Label label27;

		private Label label26;

		private TextBox textBox5;

		private TextBox textBox6;

		private TextBox textBox7;

		private TextBox textBox8;

		private Label label25;

		private TextBox textBox3;

		private TextBox textBox4;

		private TextBox textBox2;

		private TextBox textBox1;

		private Label label24;

		private Label label23;

		private Label label11;

		private TextBox textBoxSimulatorName;

		private TextBox textBoxMp;

		private TextBox textBoxHp;

		private Label label22;

		private Label label21;

		private Label label20;

		private TextBox textBoxRefresh;

		private Label label19;

		private NumericUpDown numericUpDown7;

		private Label label14;

		private Label label17;

		private ComboBox comboBoxBackToHome;

		private NumericUpDown numericUpDown8;

		private Label label15;

		private Label label16;

		private NumericUpDown numericUpDown6;

		private Label label12;

		private Label label13;

		private NumericUpDown numericUpDown3;

		private NumericUpDown numericUpDown4;

		private Label label8;

		private Label label9;

		private Label label10;

		private NumericUpDown numericUpDown2;

		private NumericUpDown numericUpDown1;

		private Label label7;

		private Label label5;

		private Label label6;

		private ComboBox comboBoxOrange;

		private ComboBox comboBoxArrow;

		private ComboBox comboBoxHealHp;

		private ComboBox comboBoxHpToMp;

		private Button button1;

		private TabPage tabPage2;

		private PictureBox pictureBox1;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private ToolStripStatusLabel toolStripStatusLabel3;

		private Label label41;

		private TextBox textBoxUid;

		private PictureBox pictureBox2;

		private Label label42;

		private ComboBox comboBoxCaptureSetting;

		private NumericUpDown numericUpDownLineNotifyMinute;

		private Label label43;

		private Timer timer1;

		private Button button3;

		private CheckBox checkBox6;

		private ComboBox comboBox1;

		private CheckBox checkBox5;

		private CheckBox checkBox4;

		private CheckBox checkBox3;

		private CheckBox checkBox2;

		private CheckBox checkBox1;

		public Form1()
		{
			this.InitializeComponent();
			this.Initial();
			this.timer1.Interval = (int)((this.numericUpDownLineNotifyMinute.Value * new decimal(60)) * new decimal(1000));
			this.timer1.Start();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this._gameMonitor.State != State.Stop)
			{
				this._gameMonitor.State = State.Stop;
				this.button1.Text = "啟動";
				this.timer1.Stop();
				return;
			}
			this.ConverUItoConfig();
			if (!this._gameMonitor.Simulator.IsSimulatorOpen())
			{
				MessageBox.Show("找不到雷電模擬器，請確認是否已經開啟，並進入天堂M遊戲");
				return;
			}
			if (!this.IsParamterValid())
			{
				MessageBox.Show("請確認輸入的參數正確後再啟動外掛");
				return;
			}
			this._gameMonitor.State = State.Run;
			this.button1.Text = "停止";
			this._gameMonitor.Player.State = RoleState.Normal;
			this._monitorTask = Task.Run(new Action(this._gameMonitor.Monitor));
			this.timer1.Stop();
			this.timer1.Start();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.frmAbout != null && !this.frmAbout.IsDisposed && this.frmAbout.Visible)
			{
				return;
			}
			this.frmAbout = new About()
			{
				TopMost = true
			};
			this.frmAbout.Show();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.listBox1.Items.Clear();
		}

		private void ConverConfigToUI()
		{
			this.textBoxSimulatorName.Text = this._config.SimulatorName;
			this.textBoxRefresh.Text = this._config.RefreshTime;
			this.comboBoxCaptureSetting.SelectedIndex = this._config.comboBoxCaptureSettingSelectIndex;
			this.comboBoxHpToMp.Text = this._config.HpToMpHotKey;
			this.comboBoxHealHp.Text = this._config.HealHpHotKey;
			this.comboBoxArrow.Text = this._config.ArrowHotKey;
			this.comboBoxOrange.Text = this._config.OrangeHotKey;
			this.comboBoxBackToHome.Text = this._config.BackHomeHotKey;
			this.comboBox1.Text = this._config.DetoxificationHotKey;
			this.checkBox3.Checked = this._config.IsArrowHotKeyEnable;
			this.checkBox5.Checked = this._config.IsBackHomeHotKeyEnable;
			this.checkBox6.Checked = this._config.IsDetoxificationHotKeyEnable;
			this.checkBox2.Checked = this._config.IsHealHpHotKeyEnable;
			this.checkBox1.Checked = this._config.IsHpToMpHotKeyEnable;
			this.checkBox4.Checked = this._config.IsOrangeHotKeyEnable;
			this.numericUpDown1.Text = this._config.numericUp1DownText;
			this.numericUpDown2.Text = this._config.numericUp2DownText;
			this.numericUpDown3.Text = this._config.numericUp3DownText;
			this.numericUpDown4.Text = this._config.numericUp4DownText;
			this.numericUpDown6.Text = this._config.numericUp6DownText;
			this.numericUpDown7.Text = this._config.numericUp7DownText;
			this.numericUpDown8.Text = this._config.numericUp8DownText;
			this.textBox4.Text = this._config.HpRect.Top;
			this.textBox3.Text = this._config.HpRect.Down;
			this.textBox1.Text = this._config.HpRect.Left;
			this.textBox2.Text = this._config.HpRect.Right;
			this.textBox6.Text = this._config.MpRect.Top;
			this.textBox5.Text = this._config.MpRect.Down;
			this.textBox8.Text = this._config.MpRect.Left;
			this.textBox7.Text = this._config.MpRect.Right;
			this.numericUpDownLineNotifyMinute.Value = this._config.LineNotifyInterval;
			this.textBoxUid.Text = this._config.Uid;
		}

		private void ConverUItoConfig()
		{
			this._config.SimulatorName = this.textBoxSimulatorName.Text;
			this._config.RefreshTime = this.textBoxRefresh.Text;
			this._config.ArrowHotKey = this.comboBoxArrow.Text;
			this._config.BackHomeHotKey = this.comboBoxBackToHome.Text;
			this._config.comboBoxCaptureSettingSelectIndex = this.comboBoxCaptureSetting.SelectedIndex;
			this._config.HealHpHotKey = this.comboBoxHealHp.Text;
			this._config.HpToMpHotKey = this.comboBoxHpToMp.Text;
			this._config.OrangeHotKey = this.comboBoxOrange.Text;
			this._config.DetoxificationHotKey = this.comboBox1.Text;
			this._config.IsArrowHotKeyEnable = this.checkBox3.Checked;
			this._config.IsBackHomeHotKeyEnable = this.checkBox5.Checked;
			this._config.IsDetoxificationHotKeyEnable = this.checkBox6.Checked;
			this._config.IsHealHpHotKeyEnable = this.checkBox2.Checked;
			this._config.IsHpToMpHotKeyEnable = this.checkBox1.Checked;
			this._config.IsOrangeHotKeyEnable = this.checkBox4.Checked;
			this._config.numericUp1DownText = this.numericUpDown1.Text;
			this._config.numericUp2DownText = this.numericUpDown2.Text;
			this._config.numericUp3DownText = this.numericUpDown3.Text;
			this._config.numericUp4DownText = this.numericUpDown4.Text;
			this._config.numericUp6DownText = this.numericUpDown6.Text;
			this._config.numericUp7DownText = this.numericUpDown7.Text;
			this._config.numericUp8DownText = this.numericUpDown8.Text;
			this._config.HpRect.Top = this.textBox4.Text;
			this._config.HpRect.Down = this.textBox3.Text;
			this._config.HpRect.Left = this.textBox1.Text;
			this._config.HpRect.Right = this.textBox2.Text;
			this._config.MpRect.Top = this.textBox6.Text;
			this._config.MpRect.Down = this.textBox5.Text;
			this._config.MpRect.Left = this.textBox8.Text;
			this._config.MpRect.Right = this.textBox7.Text;
			this._config.LineNotifyInterval = (int)this.numericUpDownLineNotifyMinute.Value;
			this._config.Uid = this.textBoxUid.Text;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void ErrorNotify(string lineError)
		{
			this.listBox1.Invoke(new Action(() => {
				this.listBox1.Items.Add(lineError);
				this.listBox1.TopIndex = this.listBox1.Items.Count - 1;
			}));
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this._gameMonitor.State = State.Stop;
				this.ConverUItoConfig();
				string str = JsonConvert.SerializeObject(this._config);
				File.WriteAllText(this._configPath, str);
			}
			catch
			{
			}
		}

		private Color GetColorAt(Point point)
		{
			Color pixel;
			try
			{
				pixel = ((Bitmap)this.pictureBox1.Image).GetPixel(point.X, point.Y);
			}
			catch
			{
				this.ErrorNotify("無法偵測到畫面，請不要把模擬器縮小到工具列");
				pixel = new Color();
			}
			return pixel;
		}

		private string GetComboboxText(ComboBox combobox)
		{
			string empty = string.Empty;
			combobox.Invoke(new Action(() => empty = combobox.Text));
			return empty;
		}

		private void Initial()
		{
			this.comboBoxHpToMp.Items.AddRange(SimulatorInfo.HotKeyList.Split(new char[] { ',' }));
			this.comboBoxHealHp.Items.AddRange(SimulatorInfo.HotKeyList.Split(new char[] { ',' }));
			this.comboBoxArrow.Items.AddRange(SimulatorInfo.HotKeyList.Split(new char[] { ',' }));
			this.comboBoxOrange.Items.AddRange(SimulatorInfo.HotKeyList.Split(new char[] { ',' }));
			this.comboBoxBackToHome.Items.AddRange(SimulatorInfo.HotKeyList.Split(new char[] { ',' }));
			this.comboBox1.Items.AddRange(SimulatorInfo.HotKeyList.Split(new char[] { ',' }));
			this.comboBoxCaptureSetting.Items.Add("DirectX");
			this.comboBoxCaptureSetting.Items.Add("GDI");
			if (!File.Exists(this._configPath))
			{
				this.comboBoxHpToMp.SelectedIndex = 0;
				this.comboBoxHealHp.SelectedIndex = 1;
				this.comboBoxArrow.SelectedIndex = 2;
				this.comboBoxOrange.SelectedIndex = 3;
				this.comboBoxBackToHome.SelectedIndex = 5;
				this.ConverUItoConfig();
			}
			else
			{
				string str = File.ReadAllText(this._configPath);
				this._config = JsonConvert.DeserializeObject<Config>(str);
				if (this._config.Version >= this._version)
				{
					this.ConverConfigToUI();
				}
				else
				{
					this.comboBoxHpToMp.SelectedIndex = 0;
					this.comboBoxHealHp.SelectedIndex = 1;
					this.comboBoxArrow.SelectedIndex = 2;
					this.comboBoxOrange.SelectedIndex = 3;
					this.comboBoxBackToHome.SelectedIndex = 5;
				}
			}
			this.labelError.Text = "待機";
			this.labelError.ForeColor = Color.DarkBlue;
			this._gameMonitor = new GameMonitor(this._config);
			this._gameMonitor.MonitorScreenChagedNotify += new Action<Image>(this.MonitorScreenChagedNotify);
			this._gameMonitor.MonitorStateNotify += new Action<string, Color, string>(this.MonitorStateNotify);
			this._gameMonitor.PlayerInfoChangedNotify += new Action<PlayerInfo>(this.PlayerInfoChangedNotify);
			this._lineMessage = new LineMessage();
			this._lineMessage.ErrorCallBack += new Action<string>(this.ErrorNotify);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.checkBox6 = new CheckBox();
			this.comboBox1 = new ComboBox();
			this.checkBox5 = new CheckBox();
			this.checkBox4 = new CheckBox();
			this.checkBox3 = new CheckBox();
			this.checkBox2 = new CheckBox();
			this.checkBox1 = new CheckBox();
			this.button3 = new Button();
			this.numericUpDownLineNotifyMinute = new NumericUpDown();
			this.label43 = new Label();
			this.comboBoxCaptureSetting = new ComboBox();
			this.label42 = new Label();
			this.pictureBox2 = new PictureBox();
			this.label41 = new Label();
			this.textBoxUid = new TextBox();
			this.button2 = new Button();
			this.label40 = new Label();
			this.label39 = new Label();
			this.label38 = new Label();
			this.labelError = new Label();
			this.listBox1 = new ListBox();
			this.label34 = new Label();
			this.label35 = new Label();
			this.label36 = new Label();
			this.label37 = new Label();
			this.label32 = new Label();
			this.label33 = new Label();
			this.label30 = new Label();
			this.label31 = new Label();
			this.label29 = new Label();
			this.label28 = new Label();
			this.label27 = new Label();
			this.label26 = new Label();
			this.textBox5 = new TextBox();
			this.textBox6 = new TextBox();
			this.textBox7 = new TextBox();
			this.textBox8 = new TextBox();
			this.label25 = new Label();
			this.textBox3 = new TextBox();
			this.textBox4 = new TextBox();
			this.textBox2 = new TextBox();
			this.textBox1 = new TextBox();
			this.label24 = new Label();
			this.label23 = new Label();
			this.label11 = new Label();
			this.textBoxSimulatorName = new TextBox();
			this.textBoxMp = new TextBox();
			this.textBoxHp = new TextBox();
			this.label22 = new Label();
			this.label21 = new Label();
			this.label20 = new Label();
			this.textBoxRefresh = new TextBox();
			this.label19 = new Label();
			this.numericUpDown7 = new NumericUpDown();
			this.label14 = new Label();
			this.label17 = new Label();
			this.comboBoxBackToHome = new ComboBox();
			this.numericUpDown8 = new NumericUpDown();
			this.label15 = new Label();
			this.label16 = new Label();
			this.numericUpDown6 = new NumericUpDown();
			this.label12 = new Label();
			this.label13 = new Label();
			this.numericUpDown3 = new NumericUpDown();
			this.numericUpDown4 = new NumericUpDown();
			this.label8 = new Label();
			this.label9 = new Label();
			this.label10 = new Label();
			this.numericUpDown2 = new NumericUpDown();
			this.numericUpDown1 = new NumericUpDown();
			this.label7 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.comboBoxOrange = new ComboBox();
			this.comboBoxArrow = new ComboBox();
			this.comboBoxHealHp = new ComboBox();
			this.comboBoxHpToMp = new ComboBox();
			this.button1 = new Button();
			this.tabPage2 = new TabPage();
			this.pictureBox1 = new PictureBox();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new ToolStripStatusLabel();
			this.timer1 = new Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.numericUpDownLineNotifyMinute).BeginInit();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.numericUpDown7).BeginInit();
			((ISupportInitialize)this.numericUpDown8).BeginInit();
			((ISupportInitialize)this.numericUpDown6).BeginInit();
			((ISupportInitialize)this.numericUpDown3).BeginInit();
			((ISupportInitialize)this.numericUpDown4).BeginInit();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(998, 651);
			this.tabControl1.TabIndex = 77;
			this.tabPage1.Controls.Add(this.checkBox6);
			this.tabPage1.Controls.Add(this.comboBox1);
			this.tabPage1.Controls.Add(this.checkBox5);
			this.tabPage1.Controls.Add(this.checkBox4);
			this.tabPage1.Controls.Add(this.checkBox3);
			this.tabPage1.Controls.Add(this.checkBox2);
			this.tabPage1.Controls.Add(this.checkBox1);
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.numericUpDownLineNotifyMinute);
			this.tabPage1.Controls.Add(this.label43);
			this.tabPage1.Controls.Add(this.comboBoxCaptureSetting);
			this.tabPage1.Controls.Add(this.label42);
			this.tabPage1.Controls.Add(this.pictureBox2);
			this.tabPage1.Controls.Add(this.label41);
			this.tabPage1.Controls.Add(this.textBoxUid);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.label40);
			this.tabPage1.Controls.Add(this.label39);
			this.tabPage1.Controls.Add(this.label38);
			this.tabPage1.Controls.Add(this.labelError);
			this.tabPage1.Controls.Add(this.listBox1);
			this.tabPage1.Controls.Add(this.label34);
			this.tabPage1.Controls.Add(this.label35);
			this.tabPage1.Controls.Add(this.label36);
			this.tabPage1.Controls.Add(this.label37);
			this.tabPage1.Controls.Add(this.label32);
			this.tabPage1.Controls.Add(this.label33);
			this.tabPage1.Controls.Add(this.label30);
			this.tabPage1.Controls.Add(this.label31);
			this.tabPage1.Controls.Add(this.label29);
			this.tabPage1.Controls.Add(this.label28);
			this.tabPage1.Controls.Add(this.label27);
			this.tabPage1.Controls.Add(this.label26);
			this.tabPage1.Controls.Add(this.textBox5);
			this.tabPage1.Controls.Add(this.textBox6);
			this.tabPage1.Controls.Add(this.textBox7);
			this.tabPage1.Controls.Add(this.textBox8);
			this.tabPage1.Controls.Add(this.label25);
			this.tabPage1.Controls.Add(this.textBox3);
			this.tabPage1.Controls.Add(this.textBox4);
			this.tabPage1.Controls.Add(this.textBox2);
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Controls.Add(this.label24);
			this.tabPage1.Controls.Add(this.label23);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.textBoxSimulatorName);
			this.tabPage1.Controls.Add(this.textBoxMp);
			this.tabPage1.Controls.Add(this.textBoxHp);
			this.tabPage1.Controls.Add(this.label22);
			this.tabPage1.Controls.Add(this.label21);
			this.tabPage1.Controls.Add(this.label20);
			this.tabPage1.Controls.Add(this.textBoxRefresh);
			this.tabPage1.Controls.Add(this.label19);
			this.tabPage1.Controls.Add(this.numericUpDown7);
			this.tabPage1.Controls.Add(this.label14);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.comboBoxBackToHome);
			this.tabPage1.Controls.Add(this.numericUpDown8);
			this.tabPage1.Controls.Add(this.label15);
			this.tabPage1.Controls.Add(this.label16);
			this.tabPage1.Controls.Add(this.numericUpDown6);
			this.tabPage1.Controls.Add(this.label12);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Controls.Add(this.numericUpDown3);
			this.tabPage1.Controls.Add(this.numericUpDown4);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.numericUpDown2);
			this.tabPage1.Controls.Add(this.numericUpDown1);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.comboBoxOrange);
			this.tabPage1.Controls.Add(this.comboBoxArrow);
			this.tabPage1.Controls.Add(this.comboBoxHealHp);
			this.tabPage1.Controls.Add(this.comboBoxHpToMp);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(990, 625);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.checkBox6.AutoSize = true;
			this.checkBox6.Font = new System.Drawing.Font("新細明體", 12f);
			this.checkBox6.Location = new Point(208, 215);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(59, 20);
			this.checkBox6.TabIndex = 164;
			this.checkBox6.Text = "解毒";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Point(284, 215);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(102, 20);
			this.comboBox1.TabIndex = 163;
			this.checkBox5.AutoSize = true;
			this.checkBox5.Font = new System.Drawing.Font("新細明體", 12f);
			this.checkBox5.Location = new Point(155, 176);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(123, 20);
			this.checkBox5.TabIndex = 162;
			this.checkBox5.Text = "回程卷軸熱鍵";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox4.AutoSize = true;
			this.checkBox4.Font = new System.Drawing.Font("新細明體", 12f);
			this.checkBox4.Location = new Point(185, 139);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(91, 20);
			this.checkBox4.TabIndex = 161;
			this.checkBox4.Text = "橘水熱鑑";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox3.AutoSize = true;
			this.checkBox3.Font = new System.Drawing.Font("新細明體", 12f);
			this.checkBox3.Location = new Point(171, 99);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(107, 20);
			this.checkBox3.TabIndex = 160;
			this.checkBox3.Text = "三重矢熱鍵";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox2.AutoSize = true;
			this.checkBox2.Font = new System.Drawing.Font("新細明體", 12f);
			this.checkBox2.Location = new Point(185, 61);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(91, 20);
			this.checkBox2.TabIndex = 159;
			this.checkBox2.Text = "高治熱鑑";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Font = new System.Drawing.Font("新細明體", 12f);
			this.checkBox1.Location = new Point(185, 26);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(91, 20);
			this.checkBox1.TabIndex = 158;
			this.checkBox1.Text = "魂體熱鑑";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.button3.Location = new Point(904, 195);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(76, 30);
			this.button3.TabIndex = 157;
			this.button3.Text = "清除";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.numericUpDownLineNotifyMinute.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDownLineNotifyMinute.Location = new Point(486, 375);
			this.numericUpDownLineNotifyMinute.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDownLineNotifyMinute.Name = "numericUpDownLineNotifyMinute";
			this.numericUpDownLineNotifyMinute.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDownLineNotifyMinute.Size = new System.Drawing.Size(87, 27);
			this.numericUpDownLineNotifyMinute.TabIndex = 156;
			this.numericUpDownLineNotifyMinute.TextAlign = HorizontalAlignment.Right;
			this.numericUpDownLineNotifyMinute.Value = new decimal(new int[] { 5, 0, 0, 0 });
			this.numericUpDownLineNotifyMinute.ValueChanged += new EventHandler(this.numericUpDownLineNotifyMinute_ValueChanged);
			this.label43.AutoSize = true;
			this.label43.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label43.Location = new Point(296, 379);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(184, 16);
			this.label43.TabIndex = 155;
			this.label43.Text = "幾分鐘送出一次Line訊息:";
			this.comboBoxCaptureSetting.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxCaptureSetting.FormattingEnabled = true;
			this.comboBoxCaptureSetting.Location = new Point(27, 197);
			this.comboBoxCaptureSetting.Name = "comboBoxCaptureSetting";
			this.comboBoxCaptureSetting.Size = new System.Drawing.Size(121, 20);
			this.comboBoxCaptureSetting.TabIndex = 154;
			this.label42.AutoSize = true;
			this.label42.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label42.Location = new Point(296, 330);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(273, 16);
			this.label42.TabIndex = 153;
			this.label42.Text = "加入好友後送出/showmyid訊息得到uid";
			this.pictureBox2.Image = Resources.QR;
			this.pictureBox2.Location = new Point(97, 320);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(193, 193);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 152;
			this.pictureBox2.TabStop = false;
			this.label41.AutoSize = true;
			this.label41.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label41.Location = new Point(27, 268);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(64, 16);
			this.label41.TabIndex = 148;
			this.label41.Text = "Line uid:";
			this.textBoxUid.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxUid.Location = new Point(97, 262);
			this.textBoxUid.Name = "textBoxUid";
			this.textBoxUid.Size = new System.Drawing.Size(344, 27);
			this.textBoxUid.TabIndex = 147;
			this.textBoxUid.Text = "Uf65bb0bcdc6e631cafc09c79cb2183df";
			this.textBoxUid.TextAlign = HorizontalAlignment.Right;
			this.button2.Location = new Point(901, 13);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(76, 30);
			this.button2.TabIndex = 146;
			this.button2.Text = "關於我";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label40.AutoSize = true;
			this.label40.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label40.Location = new Point(901, 76);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(76, 16);
			this.label40.TabIndex = 145;
			this.label40.Text = "錯誤訊息:";
			this.label39.AutoSize = true;
			this.label39.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label39.Location = new Point(662, 320);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(76, 16);
			this.label39.TabIndex = 144;
			this.label39.Text = "範圍設定:";
			this.label38.AutoSize = true;
			this.label38.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label38.Location = new Point(656, 205);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(76, 16);
			this.label38.TabIndex = 143;
			this.label38.Text = "偵測數據:";
			this.labelError.AutoSize = true;
			this.labelError.Font = new System.Drawing.Font("新細明體", 24f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.labelError.Location = new Point(828, 236);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(102, 32);
			this.labelError.TabIndex = 142;
			this.labelError.Text = "label38";
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new Point(675, 101);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(311, 88);
			this.listBox1.TabIndex = 141;
			this.label34.AutoSize = true;
			this.label34.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label34.Location = new Point(765, 515);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(40, 16);
			this.label34.TabIndex = 140;
			this.label34.Text = "下面";
			this.label35.AutoSize = true;
			this.label35.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label35.Location = new Point(691, 515);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(40, 16);
			this.label35.TabIndex = 139;
			this.label35.Text = "上面";
			this.label36.AutoSize = true;
			this.label36.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label36.Location = new Point(765, 463);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(40, 16);
			this.label36.TabIndex = 138;
			this.label36.Text = "右邊";
			this.label37.AutoSize = true;
			this.label37.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label37.Location = new Point(691, 463);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(40, 16);
			this.label37.TabIndex = 137;
			this.label37.Text = "左邊";
			this.label32.AutoSize = true;
			this.label32.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label32.Location = new Point(761, 404);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(40, 16);
			this.label32.TabIndex = 136;
			this.label32.Text = "下面";
			this.label33.AutoSize = true;
			this.label33.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label33.Location = new Point(687, 404);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(40, 16);
			this.label33.TabIndex = 135;
			this.label33.Text = "上面";
			this.label30.AutoSize = true;
			this.label30.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label30.Location = new Point(814, 533);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(19, 16);
			this.label30.TabIndex = 134;
			this.label30.Text = "Y";
			this.label31.AutoSize = true;
			this.label31.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label31.Location = new Point(814, 483);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(19, 16);
			this.label31.TabIndex = 133;
			this.label31.Text = "X";
			this.label29.AutoSize = true;
			this.label29.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label29.Location = new Point(814, 428);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(19, 16);
			this.label29.TabIndex = 132;
			this.label29.Text = "Y";
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label28.Location = new Point(814, 375);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(19, 16);
			this.label28.TabIndex = 131;
			this.label28.Text = "X";
			this.label27.AutoSize = true;
			this.label27.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label27.Location = new Point(762, 347);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(40, 16);
			this.label27.TabIndex = 130;
			this.label27.Text = "右邊";
			this.label26.AutoSize = true;
			this.label26.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label26.Location = new Point(688, 347);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(40, 16);
			this.label26.TabIndex = 129;
			this.label26.Text = "左邊";
			this.textBox5.Location = new Point(761, 534);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(44, 22);
			this.textBox5.TabIndex = 128;
			this.textBox5.Text = "303";
			this.textBox6.Location = new Point(688, 534);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(44, 22);
			this.textBox6.TabIndex = 127;
			this.textBox6.Text = "302";
			this.textBox7.Location = new Point(761, 482);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(44, 22);
			this.textBox7.TabIndex = 126;
			this.textBox7.Text = "180";
			this.textBox8.Location = new Point(688, 482);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(44, 22);
			this.textBox8.TabIndex = 125;
			this.textBox8.Text = "78";
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label25.Location = new Point(653, 483);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(33, 16);
			this.label25.TabIndex = 124;
			this.label25.Text = "MP:";
			this.textBox3.Location = new Point(761, 428);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(44, 22);
			this.textBox3.TabIndex = 123;
			this.textBox3.Text = "293";
			this.textBox4.Location = new Point(688, 428);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(44, 22);
			this.textBox4.TabIndex = 122;
			this.textBox4.Text = "290";
			this.textBox2.Location = new Point(761, 373);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(44, 22);
			this.textBox2.TabIndex = 121;
			this.textBox2.Text = "180";
			this.textBox1.Location = new Point(688, 373);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(44, 22);
			this.textBox1.TabIndex = 120;
			this.textBox1.Text = "78";
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label24.Location = new Point(653, 377);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(31, 16);
			this.label24.TabIndex = 119;
			this.label24.Text = "HP:";
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label23.Location = new Point(773, 271);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(21, 16);
			this.label23.TabIndex = 118;
			this.label23.Text = "%";
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label11.Location = new Point(773, 239);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(21, 16);
			this.label11.TabIndex = 117;
			this.label11.Text = "%";
			this.textBoxSimulatorName.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxSimulatorName.Location = new Point(30, 150);
			this.textBoxSimulatorName.Name = "textBoxSimulatorName";
			this.textBoxSimulatorName.Size = new System.Drawing.Size(119, 27);
			this.textBoxSimulatorName.TabIndex = 116;
			this.textBoxSimulatorName.Text = "雷電模擬器-1";
			this.textBoxSimulatorName.TextAlign = HorizontalAlignment.Center;
			this.textBoxMp.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxMp.Location = new Point(691, 268);
			this.textBoxMp.Name = "textBoxMp";
			this.textBoxMp.ReadOnly = true;
			this.textBoxMp.Size = new System.Drawing.Size(76, 27);
			this.textBoxMp.TabIndex = 115;
			this.textBoxMp.Text = "1";
			this.textBoxMp.TextAlign = HorizontalAlignment.Right;
			this.textBoxHp.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxHp.Location = new Point(691, 235);
			this.textBoxHp.Name = "textBoxHp";
			this.textBoxHp.ReadOnly = true;
			this.textBoxHp.Size = new System.Drawing.Size(76, 27);
			this.textBoxHp.TabIndex = 114;
			this.textBoxHp.Text = "1";
			this.textBoxHp.TextAlign = HorizontalAlignment.Right;
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label22.Location = new Point(654, 271);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(33, 16);
			this.label22.TabIndex = 112;
			this.label22.Text = "MP:";
			this.label21.AutoSize = true;
			this.label21.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label21.Location = new Point(654, 238);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(31, 16);
			this.label21.TabIndex = 111;
			this.label21.Text = "HP:";
			this.label20.AutoSize = true;
			this.label20.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label20.Location = new Point(125, 108);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(24, 16);
			this.label20.TabIndex = 109;
			this.label20.Text = "秒";
			this.textBoxRefresh.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxRefresh.Location = new Point(43, 105);
			this.textBoxRefresh.Name = "textBoxRefresh";
			this.textBoxRefresh.Size = new System.Drawing.Size(76, 27);
			this.textBoxRefresh.TabIndex = 108;
			this.textBoxRefresh.Text = "0";
			this.textBoxRefresh.TextAlign = HorizontalAlignment.Right;
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label19.Location = new Point(39, 76);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(104, 16);
			this.label19.TabIndex = 107;
			this.label19.Text = "外掛偵測頻率";
			this.numericUpDown7.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown7.Location = new Point(583, 169);
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown7.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown7.TabIndex = 106;
			this.numericUpDown7.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown7.Value = new decimal(new int[] { 20, 0, 0, 0 });
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label14.Location = new Point(642, 175);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(21, 16);
			this.label14.TabIndex = 105;
			this.label14.Text = "%";
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label17.Location = new Point(411, 175);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(171, 16);
			this.label17.TabIndex = 104;
			this.label17.Text = "使用回程卷軸當HP低於";
			this.comboBoxBackToHome.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxBackToHome.FormattingEnabled = true;
			this.comboBoxBackToHome.Location = new Point(284, 175);
			this.comboBoxBackToHome.Name = "comboBoxBackToHome";
			this.comboBoxBackToHome.Size = new System.Drawing.Size(102, 20);
			this.comboBoxBackToHome.TabIndex = 103;
			this.numericUpDown8.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown8.Location = new Point(520, 132);
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown8.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown8.TabIndex = 101;
			this.numericUpDown8.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown8.Value = new decimal(new int[] { 50, 0, 0, 0 });
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label15.Location = new Point(579, 138);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(21, 16);
			this.label15.TabIndex = 100;
			this.label15.Text = "%";
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label16.Location = new Point(411, 138);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(107, 16);
			this.label16.TabIndex = 99;
			this.label16.Text = "施放當HP低於";
			this.numericUpDown6.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown6.Location = new Point(520, 92);
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown6.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown6.TabIndex = 98;
			this.numericUpDown6.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown6.Value = new decimal(new int[] { 20, 0, 0, 0 });
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label12.Location = new Point(579, 98);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(21, 16);
			this.label12.TabIndex = 97;
			this.label12.Text = "%";
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label13.Location = new Point(411, 98);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(109, 16);
			this.label13.TabIndex = 96;
			this.label13.Text = "施放當MP高於";
			this.numericUpDown3.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown3.Location = new Point(665, 54);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown3.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown3.TabIndex = 95;
			this.numericUpDown3.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown3.Value = new decimal(new int[] { 80, 0, 0, 0 });
			this.numericUpDown4.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown4.Location = new Point(520, 54);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown4.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown4.TabIndex = 94;
			this.numericUpDown4.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown4.Value = new decimal(new int[] { 30, 0, 0, 0 });
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label8.Location = new Point(722, 60);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(21, 16);
			this.label8.TabIndex = 93;
			this.label8.Text = "%";
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label9.Location = new Point(579, 60);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(88, 16);
			this.label9.TabIndex = 92;
			this.label9.Text = "%，HP低於";
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label10.Location = new Point(411, 60);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(109, 16);
			this.label10.TabIndex = 91;
			this.label10.Text = "施放當MP高於";
			this.numericUpDown2.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown2.Location = new Point(665, 21);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown2.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown2.TabIndex = 90;
			this.numericUpDown2.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown2.Value = new decimal(new int[] { 10, 0, 0, 0 });
			this.numericUpDown1.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown1.Location = new Point(520, 21);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown1.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown1.TabIndex = 89;
			this.numericUpDown1.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown1.Value = new decimal(new int[] { 50, 0, 0, 0 });
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label7.Location = new Point(722, 27);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(21, 16);
			this.label7.TabIndex = 88;
			this.label7.Text = "%";
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label5.Location = new Point(579, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 87;
			this.label5.Text = "%，HP高於";
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label6.Location = new Point(411, 27);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(109, 16);
			this.label6.TabIndex = 86;
			this.label6.Text = "施放當MP低於";
			this.comboBoxOrange.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxOrange.FormattingEnabled = true;
			this.comboBoxOrange.Location = new Point(284, 138);
			this.comboBoxOrange.Name = "comboBoxOrange";
			this.comboBoxOrange.Size = new System.Drawing.Size(102, 20);
			this.comboBoxOrange.TabIndex = 85;
			this.comboBoxArrow.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxArrow.FormattingEnabled = true;
			this.comboBoxArrow.Location = new Point(284, 99);
			this.comboBoxArrow.Name = "comboBoxArrow";
			this.comboBoxArrow.Size = new System.Drawing.Size(102, 20);
			this.comboBoxArrow.TabIndex = 83;
			this.comboBoxHealHp.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxHealHp.FormattingEnabled = true;
			this.comboBoxHealHp.Location = new Point(284, 61);
			this.comboBoxHealHp.Name = "comboBoxHealHp";
			this.comboBoxHealHp.Size = new System.Drawing.Size(102, 20);
			this.comboBoxHealHp.TabIndex = 81;
			this.comboBoxHpToMp.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxHpToMp.FormattingEnabled = true;
			this.comboBoxHpToMp.Location = new Point(284, 26);
			this.comboBoxHpToMp.Name = "comboBoxHpToMp";
			this.comboBoxHpToMp.Size = new System.Drawing.Size(102, 20);
			this.comboBoxHpToMp.TabIndex = 79;
			this.button1.Location = new Point(43, 21);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(76, 30);
			this.button1.TabIndex = 77;
			this.button1.Text = "啟動";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.tabPage2.Controls.Add(this.pictureBox1);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(990, 625);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Location = new Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(984, 619);
			this.pictureBox1.TabIndex = 111;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
			this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripStatusLabel2, this.toolStripStatusLabel3 });
			this.statusStrip1.Location = new Point(0, 629);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(998, 22);
			this.statusStrip1.TabIndex = 114;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(15, 17);
			this.toolStripStatusLabel1.Text = "X";
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(14, 17);
			this.toolStripStatusLabel2.Text = "Y";
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(38, 17);
			this.toolStripStatusLabel3.Text = "Color";
			this.timer1.Interval = 10000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(998, 651);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.tabControl1);
			base.Name = "Form1";
			this.Text = "天堂M外掛 (目前只支援雷電和夜神模擬器)";
			base.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((ISupportInitialize)this.numericUpDownLineNotifyMinute).EndInit();
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.numericUpDown7).EndInit();
			((ISupportInitialize)this.numericUpDown8).EndInit();
			((ISupportInitialize)this.numericUpDown6).EndInit();
			((ISupportInitialize)this.numericUpDown3).EndInit();
			((ISupportInitialize)this.numericUpDown4).EndInit();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private bool IsHotKeyUsed(List<string> hotKeyCheckList, ComboBox comboBox)
		{
			if (hotKeyCheckList.Any<string>((string x) => x.Equals(comboBox.Text)))
			{
				return true;
			}
			hotKeyCheckList.Add(comboBox.Text);
			return false;
		}

		private bool IsParamterValid()
		{
			bool flag;
			try
			{
				double.Parse(this.textBoxRefresh.Text);
				List<string> strs = new List<string>();
				if (this.IsHotKeyUsed(strs, this.comboBoxHpToMp))
				{
					flag = false;
				}
				else if (this.IsHotKeyUsed(strs, this.comboBoxHealHp))
				{
					flag = false;
				}
				else if (this.IsHotKeyUsed(strs, this.comboBoxArrow))
				{
					flag = false;
				}
				else if (!this.IsHotKeyUsed(strs, this.comboBoxOrange))
				{
					flag = (!this.IsHotKeyUsed(strs, this.comboBoxBackToHome) ? true : false);
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void MonitorScreenChagedNotify(Image image)
		{
			base.Invoke(new Action(() => this.pictureBox1.Image = image));
		}

		public void MonitorStateNotify(string state, Color color, string errorMsg = "")
		{
			base.Invoke(new Action(() => {
				this.labelError.Text = state;
				this.labelError.ForeColor = color;
				if (state.Equals("待機"))
				{
					this.button1.Text = "啟動";
				}
				if (!string.IsNullOrWhiteSpace(errorMsg))
				{
					this.listBox1.Items.Add(errorMsg);
					this.listBox1.TopIndex = this.listBox1.Items.Count - 1;
				}
			}));
		}

		private void numericUpDownLineNotifyMinute_ValueChanged(object sender, EventArgs e)
		{
			this.timer1.Stop();
			this.timer1.Interval = (int)((this.numericUpDownLineNotifyMinute.Value * new decimal(60)) * new decimal(1000));
			this.timer1.Start();
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (this.pictureBox1.Image != null)
				{
					Color colorAt = this.GetColorAt(e.Location);
					this.toolStripStatusLabel3.Text = colorAt.ToString();
					ToolStripStatusLabel str = this.toolStripStatusLabel1;
					int x = e.Location.X;
					str.Text = x.ToString();
					ToolStripStatusLabel toolStripStatusLabel = this.toolStripStatusLabel2;
					x = e.Location.Y;
					toolStripStatusLabel.Text = x.ToString();
				}
			}
			catch
			{
			}
		}

		public void PlayerInfoChangedNotify(PlayerInfo player)
		{
			base.Invoke(new Action(() => {
				TextBox u003cu003e4_this = this.textBoxHp;
				int hp = player.Hp;
				u003cu003e4_this.Text = hp.ToString();
				TextBox str = this.textBoxMp;
				hp = player.Mp;
				str.Text = hp.ToString();
			}));
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this._lineMessage.SendMessageToLine(this.textBoxUid.Text, this._gameMonitor.Player.GetRoleStateMessage(), this._gameMonitor.Simulator.GetImage((CaptureMode)this._config.comboBoxCaptureSettingSelectIndex));
		}
	}
}
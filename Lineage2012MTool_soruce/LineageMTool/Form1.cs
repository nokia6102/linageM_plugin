using Newtonsoft.Json;
using ScreenShotDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineageMTool
{
	public class Form1 : Form
	{
		private State _state = State.Stop;

		private string _hotKeyList = "鍵盤7,鍵盤8,鍵盤9,鍵盤0,鍵盤U,鍵盤I,鍵盤O,鍵盤P";

		private Task _monitorTask;

		private Config _config;

		private string _configPath = "Config.json";

		private int _version = 1;

		private About frmAbout;

		private IContainer components;

		private Button button1;

		private Label label1;

		private ComboBox comboBoxHpToMp;

		private ComboBox comboBoxHealHp;

		private Label label2;

		private ComboBox comboBoxArrow;

		private Label label3;

		private ComboBox comboBoxOrange;

		private Label label4;

		private Label label6;

		private Label label5;

		private Label label7;

		private NumericUpDown numericUpDown1;

		private NumericUpDown numericUpDown2;

		private NumericUpDown numericUpDown3;

		private NumericUpDown numericUpDown4;

		private Label label8;

		private Label label9;

		private Label label10;

		private NumericUpDown numericUpDown6;

		private Label label12;

		private Label label13;

		private NumericUpDown numericUpDown8;

		private Label label15;

		private Label label16;

		private NumericUpDown numericUpDown7;

		private Label label14;

		private Label label17;

		private ComboBox comboBoxBackToHome;

		private Label label18;

		private Label label19;

		private TextBox textBoxRefresh;

		private Label label20;

		private PictureBox pictureBox1;

		private Label label21;

		private Label label22;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private ToolStripStatusLabel toolStripStatusLabel3;

		private TextBox textBoxHp;

		private TextBox textBoxMp;

		private TextBox textBoxSimulatorName;

		private Label label11;

		private Label label23;

		private Label label24;

		private TextBox textBox1;

		private TextBox textBox2;

		private TextBox textBox3;

		private TextBox textBox4;

		private Label label25;

		private TextBox textBox5;

		private TextBox textBox6;

		private TextBox textBox7;

		private TextBox textBox8;

		private Label label26;

		private Label label27;

		private Label label28;

		private Label label29;

		private Label label30;

		private Label label31;

		private Label label32;

		private Label label33;

		private Label label34;

		private Label label35;

		private Label label36;

		private Label label37;

		private ListBox listBox1;

		private Label labelError;

		private Label label38;

		private Label label39;

		private Label label40;

		private Button button2;

		public Form1()
		{
			this.InitializeComponent();
			this.Initial();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this._state != State.Stop)
			{
				this._state = State.Stop;
				this.button1.Text = "啟動";
				return;
			}
			if (!this.IsSimulatorOpen())
			{
				MessageBox.Show("找不到雷電模擬器，請確認是否已經開啟，並進入天堂M遊戲");
				return;
			}
			if (!this.IsParamterValid())
			{
				MessageBox.Show("請確認輸入的參數正確後再啟動外掛");
				return;
			}
			this._state = State.Run;
			this.button1.Text = "停止";
			this._monitorTask = Task.Run(new Action(this.Monitor));
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

		private int CalculateHpPercent(Image image)
		{
			List<Color> colors = new List<Color>();
			int num = int.Parse(this.textBox1.Text);
			int num1 = int.Parse(this.textBox2.Text);
			int num2 = int.Parse(this.textBox4.Text);
			int num3 = int.Parse(this.textBox3.Text);
			using (Bitmap bitmap = new Bitmap(image))
			{
				for (int i = num; i < num1; i++)
				{
					int r = 0;
					int g = 0;
					int b = 0;
					for (int j = num2; j < num3; j++)
					{
						Color pixel = bitmap.GetPixel(i, j);
						g += pixel.G;
						b += pixel.B;
						r += pixel.R;
					}
					r = r / (num3 - num2);
					g = g / (num3 - num2);
					b = b / (num3 - num2);
					if (r > 100 && g < 50 && b < 50)
					{
						colors.Add(Color.FromArgb(r, g, b));
					}
				}
			}
			return colors.Count * 100 / (num1 - num);
		}

		private int CalculateMpPercent(Image image)
		{
			List<Color> colors = new List<Color>();
			int num = int.Parse(this.textBox8.Text);
			int num1 = int.Parse(this.textBox7.Text);
			int num2 = int.Parse(this.textBox6.Text);
			int num3 = int.Parse(this.textBox5.Text);
			using (Bitmap bitmap = new Bitmap(image))
			{
				for (int i = num; i < num1; i++)
				{
					int r = 0;
					int g = 0;
					int b = 0;
					for (int j = num2; j < num3; j++)
					{
						Color pixel = bitmap.GetPixel(i, j);
						r += pixel.R;
						g += pixel.G;
						b += pixel.B;
					}
					r = r / (num3 - num2);
					g = g / (num3 - num2);
					b = b / (num3 - num2);
					if (b > 100 && r < 20)
					{
						colors.Add(Color.FromArgb(r, g, b));
					}
				}
			}
			return colors.Count * 100 / (num1 - num);
		}

		private void CheckAction(int hp, int mp, IntPtr hwndCalc)
		{
			Action action = null;
			if (hp < this.numericUpDown8.Value)
			{
				base.Invoke(new Action(() => this.SendMessageToSimulator(this.GetComboboxText(this.comboBoxOrange), hwndCalc)));
			}
			if (hp < this.numericUpDown7.Value)
			{
				base.Invoke(new Action(() => this.SendMessageToSimulator(this.GetComboboxText(this.comboBoxBackToHome), hwndCalc)));
				this._state = State.Stop;
			}
			if (hp < this.numericUpDown3.Value && mp > this.numericUpDown4.Value)
			{
				base.Invoke(new Action(() => this.SendMessageToSimulator(this.GetComboboxText(this.comboBoxHealHp), hwndCalc)));
				return;
			}
			if (mp < this.numericUpDown1.Value && hp > this.numericUpDown2.Value)
			{
				base.Invoke(new Action(() => this.SendMessageToSimulator(this.GetComboboxText(this.comboBoxHpToMp), hwndCalc)));
				return;
			}
			if (mp > this.numericUpDown6.Value)
			{
				for (int i = 0; i < 3; i++)
				{
					Action action1 = action;
					if (action1 == null)
					{
						Action simulator = () => this.SendMessageToSimulator(this.GetComboboxText(this.comboBoxArrow), hwndCalc);
						Action action2 = simulator;
						action = simulator;
						action1 = action2;
					}
					base.Invoke(action1);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this._state = State.Stop;
				this._config.SimulatorName = this.textBoxSimulatorName.Text;
				this._config.HpToMpSelectIndex = this.comboBoxHpToMp.SelectedIndex;
				this._config.HealSelectIndex = this.comboBoxHealHp.SelectedIndex;
				this._config.ArrowSelectIndex = this.comboBoxArrow.SelectedIndex;
				this._config.OrangeSelectIndex = this.comboBoxOrange.SelectedIndex;
				this._config.BackHomeSelectIndex = this.comboBoxBackToHome.SelectedIndex;
				this._config.RefreshTime = this.textBoxRefresh.Text;
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
				this._config.Version = this._version;
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
				this._state = State.Stop;
				MessageBox.Show("請不要把模擬器縮小到工具列");
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
			this.comboBoxHpToMp.Items.AddRange(this._hotKeyList.Split(new char[] { ',' }));
			this.comboBoxHealHp.Items.AddRange(this._hotKeyList.Split(new char[] { ',' }));
			this.comboBoxArrow.Items.AddRange(this._hotKeyList.Split(new char[] { ',' }));
			this.comboBoxOrange.Items.AddRange(this._hotKeyList.Split(new char[] { ',' }));
			this.comboBoxBackToHome.Items.AddRange(this._hotKeyList.Split(new char[] { ',' }));
			if (!File.Exists(this._configPath))
			{
				this._config = new Config();
				this.comboBoxHpToMp.SelectedIndex = 0;
				this.comboBoxHealHp.SelectedIndex = 1;
				this.comboBoxArrow.SelectedIndex = 2;
				this.comboBoxOrange.SelectedIndex = 3;
				this.comboBoxBackToHome.SelectedIndex = 5;
			}
			else
			{
				string str = File.ReadAllText(this._configPath);
				this._config = JsonConvert.DeserializeObject<Config>(str);
				if (this._config.Version >= this._version)
				{
					this.textBoxSimulatorName.Text = this._config.SimulatorName;
					this.comboBoxHpToMp.SelectedIndex = this._config.HpToMpSelectIndex;
					this.comboBoxHealHp.SelectedIndex = this._config.HealSelectIndex;
					this.comboBoxArrow.SelectedIndex = this._config.ArrowSelectIndex;
					this.comboBoxOrange.SelectedIndex = this._config.OrangeSelectIndex;
					this.comboBoxBackToHome.SelectedIndex = this._config.BackHomeSelectIndex;
					this.textBoxRefresh.Text = this._config.RefreshTime;
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
				}
				else
				{
					this._config = new Config();
					this.comboBoxHpToMp.SelectedIndex = 0;
					this.comboBoxHealHp.SelectedIndex = 1;
					this.comboBoxArrow.SelectedIndex = 2;
					this.comboBoxOrange.SelectedIndex = 3;
					this.comboBoxBackToHome.SelectedIndex = 5;
				}
			}
			this.labelError.Text = "待機";
			this.labelError.ForeColor = Color.DarkBlue;
		}

		private void InitializeComponent()
		{
			this.button1 = new Button();
			this.label1 = new Label();
			this.comboBoxHpToMp = new ComboBox();
			this.comboBoxHealHp = new ComboBox();
			this.label2 = new Label();
			this.comboBoxArrow = new ComboBox();
			this.label3 = new Label();
			this.comboBoxOrange = new ComboBox();
			this.label4 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.label7 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.numericUpDown2 = new NumericUpDown();
			this.numericUpDown3 = new NumericUpDown();
			this.numericUpDown4 = new NumericUpDown();
			this.label8 = new Label();
			this.label9 = new Label();
			this.label10 = new Label();
			this.numericUpDown6 = new NumericUpDown();
			this.label12 = new Label();
			this.label13 = new Label();
			this.numericUpDown8 = new NumericUpDown();
			this.label15 = new Label();
			this.label16 = new Label();
			this.numericUpDown7 = new NumericUpDown();
			this.label14 = new Label();
			this.label17 = new Label();
			this.comboBoxBackToHome = new ComboBox();
			this.label18 = new Label();
			this.label19 = new Label();
			this.textBoxRefresh = new TextBox();
			this.label20 = new Label();
			this.pictureBox1 = new PictureBox();
			this.label21 = new Label();
			this.label22 = new Label();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new ToolStripStatusLabel();
			this.textBoxHp = new TextBox();
			this.textBoxMp = new TextBox();
			this.textBoxSimulatorName = new TextBox();
			this.label11 = new Label();
			this.label23 = new Label();
			this.label24 = new Label();
			this.textBox1 = new TextBox();
			this.textBox2 = new TextBox();
			this.textBox3 = new TextBox();
			this.textBox4 = new TextBox();
			this.label25 = new Label();
			this.textBox5 = new TextBox();
			this.textBox6 = new TextBox();
			this.textBox7 = new TextBox();
			this.textBox8 = new TextBox();
			this.label26 = new Label();
			this.label27 = new Label();
			this.label28 = new Label();
			this.label29 = new Label();
			this.label30 = new Label();
			this.label31 = new Label();
			this.label32 = new Label();
			this.label33 = new Label();
			this.label34 = new Label();
			this.label35 = new Label();
			this.label36 = new Label();
			this.label37 = new Label();
			this.listBox1 = new ListBox();
			this.labelError = new Label();
			this.label38 = new Label();
			this.label39 = new Label();
			this.label40 = new Label();
			this.button2 = new Button();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			((ISupportInitialize)this.numericUpDown3).BeginInit();
			((ISupportInitialize)this.numericUpDown4).BeginInit();
			((ISupportInitialize)this.numericUpDown6).BeginInit();
			((ISupportInitialize)this.numericUpDown8).BeginInit();
			((ISupportInitialize)this.numericUpDown7).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.button1.Location = new Point(43, 35);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(76, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "啟動";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label1.Location = new Point(195, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "魂體熱鍵";
			this.comboBoxHpToMp.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxHpToMp.FormattingEnabled = true;
			this.comboBoxHpToMp.Location = new Point(284, 40);
			this.comboBoxHpToMp.Name = "comboBoxHpToMp";
			this.comboBoxHpToMp.Size = new System.Drawing.Size(102, 20);
			this.comboBoxHpToMp.TabIndex = 4;
			this.comboBoxHealHp.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxHealHp.FormattingEnabled = true;
			this.comboBoxHealHp.Location = new Point(284, 75);
			this.comboBoxHealHp.Name = "comboBoxHealHp";
			this.comboBoxHealHp.Size = new System.Drawing.Size(102, 20);
			this.comboBoxHealHp.TabIndex = 6;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label2.Location = new Point(195, 76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "高治熱鍵";
			this.comboBoxArrow.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxArrow.FormattingEnabled = true;
			this.comboBoxArrow.Location = new Point(284, 113);
			this.comboBoxArrow.Name = "comboBoxArrow";
			this.comboBoxArrow.Size = new System.Drawing.Size(102, 20);
			this.comboBoxArrow.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label3.Location = new Point(188, 114);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "三重矢熱鍵";
			this.comboBoxOrange.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxOrange.FormattingEnabled = true;
			this.comboBoxOrange.Location = new Point(284, 152);
			this.comboBoxOrange.Name = "comboBoxOrange";
			this.comboBoxOrange.Size = new System.Drawing.Size(102, 20);
			this.comboBoxOrange.TabIndex = 10;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label4.Location = new Point(195, 153);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "橘水熱鍵";
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label6.Location = new Point(411, 41);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(109, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "施放當MP低於";
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label5.Location = new Point(579, 41);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "%，HP高於";
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label7.Location = new Point(722, 41);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(21, 16);
			this.label7.TabIndex = 14;
			this.label7.Text = "%";
			this.numericUpDown1.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown1.Location = new Point(520, 35);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown1.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown1.TabIndex = 16;
			this.numericUpDown1.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown1.Value = new decimal(new int[] { 50, 0, 0, 0 });
			this.numericUpDown2.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown2.Location = new Point(665, 35);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown2.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown2.TabIndex = 17;
			this.numericUpDown2.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown2.Value = new decimal(new int[] { 10, 0, 0, 0 });
			this.numericUpDown3.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown3.Location = new Point(665, 68);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown3.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown3.TabIndex = 22;
			this.numericUpDown3.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown3.Value = new decimal(new int[] { 80, 0, 0, 0 });
			this.numericUpDown4.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown4.Location = new Point(520, 68);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown4.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown4.TabIndex = 21;
			this.numericUpDown4.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown4.Value = new decimal(new int[] { 30, 0, 0, 0 });
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label8.Location = new Point(722, 74);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(21, 16);
			this.label8.TabIndex = 20;
			this.label8.Text = "%";
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label9.Location = new Point(579, 74);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(88, 16);
			this.label9.TabIndex = 19;
			this.label9.Text = "%，HP低於";
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label10.Location = new Point(411, 74);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(109, 16);
			this.label10.TabIndex = 18;
			this.label10.Text = "施放當MP高於";
			this.numericUpDown6.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown6.Location = new Point(520, 106);
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown6.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown6.TabIndex = 26;
			this.numericUpDown6.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown6.Value = new decimal(new int[] { 20, 0, 0, 0 });
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label12.Location = new Point(579, 112);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(21, 16);
			this.label12.TabIndex = 24;
			this.label12.Text = "%";
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label13.Location = new Point(411, 112);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(109, 16);
			this.label13.TabIndex = 23;
			this.label13.Text = "施放當MP高於";
			this.numericUpDown8.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown8.Location = new Point(520, 146);
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown8.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown8.TabIndex = 31;
			this.numericUpDown8.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown8.Value = new decimal(new int[] { 50, 0, 0, 0 });
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label15.Location = new Point(579, 152);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(21, 16);
			this.label15.TabIndex = 29;
			this.label15.Text = "%";
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label16.Location = new Point(411, 152);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(107, 16);
			this.label16.TabIndex = 28;
			this.label16.Text = "施放當HP低於";
			this.numericUpDown7.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.numericUpDown7.Location = new Point(583, 183);
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDown7.Size = new System.Drawing.Size(53, 27);
			this.numericUpDown7.TabIndex = 36;
			this.numericUpDown7.TextAlign = HorizontalAlignment.Right;
			this.numericUpDown7.Value = new decimal(new int[] { 20, 0, 0, 0 });
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label14.Location = new Point(642, 189);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(21, 16);
			this.label14.TabIndex = 35;
			this.label14.Text = "%";
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label17.Location = new Point(411, 189);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(171, 16);
			this.label17.TabIndex = 34;
			this.label17.Text = "使用回程卷軸當HP低於";
			this.comboBoxBackToHome.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxBackToHome.FormattingEnabled = true;
			this.comboBoxBackToHome.Location = new Point(284, 189);
			this.comboBoxBackToHome.Name = "comboBoxBackToHome";
			this.comboBoxBackToHome.Size = new System.Drawing.Size(102, 20);
			this.comboBoxBackToHome.TabIndex = 33;
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label18.Location = new Point(174, 189);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(104, 16);
			this.label18.TabIndex = 32;
			this.label18.Text = "回程卷軸熱鍵";
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label19.Location = new Point(39, 90);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(104, 16);
			this.label19.TabIndex = 37;
			this.label19.Text = "外掛偵測頻率";
			this.textBoxRefresh.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxRefresh.Location = new Point(43, 119);
			this.textBoxRefresh.Name = "textBoxRefresh";
			this.textBoxRefresh.Size = new System.Drawing.Size(76, 27);
			this.textBoxRefresh.TabIndex = 38;
			this.textBoxRefresh.Text = "0";
			this.textBoxRefresh.TextAlign = HorizontalAlignment.Right;
			this.textBoxRefresh.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
			this.label20.AutoSize = true;
			this.label20.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label20.Location = new Point(125, 122);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(24, 16);
			this.label20.TabIndex = 39;
			this.label20.Text = "秒";
			this.pictureBox1.Location = new Point(42, 252);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(586, 374);
			this.pictureBox1.TabIndex = 40;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
			this.label21.AutoSize = true;
			this.label21.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label21.Location = new Point(654, 252);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(31, 16);
			this.label21.TabIndex = 41;
			this.label21.Text = "HP:";
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label22.Location = new Point(654, 285);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(33, 16);
			this.label22.TabIndex = 42;
			this.label22.Text = "MP:";
			this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripStatusLabel2, this.toolStripStatusLabel3 });
			this.statusStrip1.Location = new Point(0, 629);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(998, 22);
			this.statusStrip1.TabIndex = 43;
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
			this.textBoxHp.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxHp.Location = new Point(691, 249);
			this.textBoxHp.Name = "textBoxHp";
			this.textBoxHp.ReadOnly = true;
			this.textBoxHp.Size = new System.Drawing.Size(76, 27);
			this.textBoxHp.TabIndex = 44;
			this.textBoxHp.Text = "1";
			this.textBoxHp.TextAlign = HorizontalAlignment.Right;
			this.textBoxMp.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxMp.Location = new Point(691, 282);
			this.textBoxMp.Name = "textBoxMp";
			this.textBoxMp.ReadOnly = true;
			this.textBoxMp.Size = new System.Drawing.Size(76, 27);
			this.textBoxMp.TabIndex = 45;
			this.textBoxMp.Text = "1";
			this.textBoxMp.TextAlign = HorizontalAlignment.Right;
			this.textBoxSimulatorName.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.textBoxSimulatorName.Location = new Point(30, 164);
			this.textBoxSimulatorName.Name = "textBoxSimulatorName";
			this.textBoxSimulatorName.Size = new System.Drawing.Size(119, 27);
			this.textBoxSimulatorName.TabIndex = 46;
			this.textBoxSimulatorName.Text = "雷電模擬器-1";
			this.textBoxSimulatorName.TextAlign = HorizontalAlignment.Center;
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label11.Location = new Point(773, 253);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(21, 16);
			this.label11.TabIndex = 47;
			this.label11.Text = "%";
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label23.Location = new Point(773, 285);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(21, 16);
			this.label23.TabIndex = 48;
			this.label23.Text = "%";
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label24.Location = new Point(653, 391);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(31, 16);
			this.label24.TabIndex = 49;
			this.label24.Text = "HP:";
			this.textBox1.Location = new Point(688, 387);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(44, 22);
			this.textBox1.TabIndex = 50;
			this.textBox1.Text = "78";
			this.textBox2.Location = new Point(761, 387);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(44, 22);
			this.textBox2.TabIndex = 51;
			this.textBox2.Text = "180";
			this.textBox3.Location = new Point(761, 442);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(44, 22);
			this.textBox3.TabIndex = 53;
			this.textBox3.Text = "293";
			this.textBox4.Location = new Point(688, 442);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(44, 22);
			this.textBox4.TabIndex = 52;
			this.textBox4.Text = "290";
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label25.Location = new Point(653, 497);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(33, 16);
			this.label25.TabIndex = 54;
			this.label25.Text = "MP:";
			this.textBox5.Location = new Point(761, 548);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(44, 22);
			this.textBox5.TabIndex = 58;
			this.textBox5.Text = "303";
			this.textBox6.Location = new Point(688, 548);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(44, 22);
			this.textBox6.TabIndex = 57;
			this.textBox6.Text = "302";
			this.textBox7.Location = new Point(761, 496);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(44, 22);
			this.textBox7.TabIndex = 56;
			this.textBox7.Text = "180";
			this.textBox8.Location = new Point(688, 496);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(44, 22);
			this.textBox8.TabIndex = 55;
			this.textBox8.Text = "78";
			this.label26.AutoSize = true;
			this.label26.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label26.Location = new Point(688, 361);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(40, 16);
			this.label26.TabIndex = 59;
			this.label26.Text = "左邊";
			this.label27.AutoSize = true;
			this.label27.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label27.Location = new Point(762, 361);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(40, 16);
			this.label27.TabIndex = 60;
			this.label27.Text = "右邊";
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label28.Location = new Point(814, 389);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(19, 16);
			this.label28.TabIndex = 61;
			this.label28.Text = "X";
			this.label29.AutoSize = true;
			this.label29.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label29.Location = new Point(814, 442);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(19, 16);
			this.label29.TabIndex = 62;
			this.label29.Text = "Y";
			this.label30.AutoSize = true;
			this.label30.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label30.Location = new Point(814, 547);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(19, 16);
			this.label30.TabIndex = 64;
			this.label30.Text = "Y";
			this.label31.AutoSize = true;
			this.label31.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label31.Location = new Point(814, 497);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(19, 16);
			this.label31.TabIndex = 63;
			this.label31.Text = "X";
			this.label32.AutoSize = true;
			this.label32.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label32.Location = new Point(761, 418);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(40, 16);
			this.label32.TabIndex = 66;
			this.label32.Text = "下面";
			this.label33.AutoSize = true;
			this.label33.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label33.Location = new Point(687, 418);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(40, 16);
			this.label33.TabIndex = 65;
			this.label33.Text = "上面";
			this.label34.AutoSize = true;
			this.label34.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label34.Location = new Point(765, 529);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(40, 16);
			this.label34.TabIndex = 70;
			this.label34.Text = "下面";
			this.label35.AutoSize = true;
			this.label35.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label35.Location = new Point(691, 529);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(40, 16);
			this.label35.TabIndex = 69;
			this.label35.Text = "上面";
			this.label36.AutoSize = true;
			this.label36.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label36.Location = new Point(765, 477);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(40, 16);
			this.label36.TabIndex = 68;
			this.label36.Text = "右邊";
			this.label37.AutoSize = true;
			this.label37.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label37.Location = new Point(691, 477);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(40, 16);
			this.label37.TabIndex = 67;
			this.label37.Text = "左邊";
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new Point(675, 115);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(311, 88);
			this.listBox1.TabIndex = 71;
			this.labelError.AutoSize = true;
			this.labelError.Font = new System.Drawing.Font("新細明體", 24f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.labelError.Location = new Point(828, 250);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(102, 32);
			this.labelError.TabIndex = 72;
			this.labelError.Text = "label38";
			this.label38.AutoSize = true;
			this.label38.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label38.Location = new Point(656, 219);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(76, 16);
			this.label38.TabIndex = 73;
			this.label38.Text = "偵測數據:";
			this.label39.AutoSize = true;
			this.label39.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label39.Location = new Point(662, 334);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(76, 16);
			this.label39.TabIndex = 74;
			this.label39.Text = "範圍設定:";
			this.label40.AutoSize = true;
			this.label40.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label40.Location = new Point(901, 90);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(76, 16);
			this.label40.TabIndex = 75;
			this.label40.Text = "錯誤訊息:";
			this.button2.Location = new Point(901, 27);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(76, 30);
			this.button2.TabIndex = 76;
			this.button2.Text = "關於我";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(998, 651);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.label40);
			base.Controls.Add(this.label39);
			base.Controls.Add(this.label38);
			base.Controls.Add(this.labelError);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.label34);
			base.Controls.Add(this.label35);
			base.Controls.Add(this.label36);
			base.Controls.Add(this.label37);
			base.Controls.Add(this.label32);
			base.Controls.Add(this.label33);
			base.Controls.Add(this.label30);
			base.Controls.Add(this.label31);
			base.Controls.Add(this.label29);
			base.Controls.Add(this.label28);
			base.Controls.Add(this.label27);
			base.Controls.Add(this.label26);
			base.Controls.Add(this.textBox5);
			base.Controls.Add(this.textBox6);
			base.Controls.Add(this.textBox7);
			base.Controls.Add(this.textBox8);
			base.Controls.Add(this.label25);
			base.Controls.Add(this.textBox3);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label24);
			base.Controls.Add(this.label23);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.textBoxSimulatorName);
			base.Controls.Add(this.textBoxMp);
			base.Controls.Add(this.textBoxHp);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.label22);
			base.Controls.Add(this.label21);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.label20);
			base.Controls.Add(this.textBoxRefresh);
			base.Controls.Add(this.label19);
			base.Controls.Add(this.numericUpDown7);
			base.Controls.Add(this.label14);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.comboBoxBackToHome);
			base.Controls.Add(this.label18);
			base.Controls.Add(this.numericUpDown8);
			base.Controls.Add(this.label15);
			base.Controls.Add(this.label16);
			base.Controls.Add(this.numericUpDown6);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.numericUpDown3);
			base.Controls.Add(this.numericUpDown4);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.numericUpDown2);
			base.Controls.Add(this.numericUpDown1);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.comboBoxOrange);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.comboBoxArrow);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.comboBoxHealHp);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.comboBoxHpToMp);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			base.Name = "Form1";
			this.Text = "天堂M外掛 (目前只支援雷電和夜神模擬器)";
			base.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
			((ISupportInitialize)this.numericUpDown1).EndInit();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			((ISupportInitialize)this.numericUpDown3).EndInit();
			((ISupportInitialize)this.numericUpDown4).EndInit();
			((ISupportInitialize)this.numericUpDown6).EndInit();
			((ISupportInitialize)this.numericUpDown8).EndInit();
			((ISupportInitialize)this.numericUpDown7).EndInit();
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

		private bool IsSimulatorOpen()
		{
			return WinApi.FindWindow(null, this.textBoxSimulatorName.Text) != IntPtr.Zero;
		}

		private void Monitor()
		{
			try
			{
				try
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					while (this._state == State.Run)
					{
						IntPtr intPtr = WinApi.FindWindow(null, this.textBoxSimulatorName.Text);
						ScreenCapture screenCapture = new ScreenCapture();
						try
						{
							Image image = screenCapture.CaptureWindow(intPtr);
							int num = this.CalculateHpPercent(image);
							this.textBoxHp.Invoke(new Action(() => this.textBoxHp.Text = num.ToString()));
							int num1 = this.CalculateMpPercent(image);
							this.textBoxMp.Invoke(new Action(() => this.textBoxMp.Text = num1.ToString()));
							this.pictureBox1.Image = image;
							if (num == 0 || num1 == 0)
							{
								this.listBox1.Invoke(new Action(() => {
									this.labelError.Text = "異常";
									this.labelError.ForeColor = Color.Red;
									this.listBox1.Items.Add("請確認組隊視窗是否開啟，以及XY範圍設定正確");
									this.listBox1.TopIndex = this.listBox1.Items.Count - 1;
								}));
							}
							else
							{
								this.labelError.Invoke(new Action(() => {
									this.labelError.Text = "正常";
									this.labelError.ForeColor = Color.Green;
								}));
								this.CheckAction(num, num1, intPtr);
							}
						}
						catch
						{
						}
						double num2 = (double)this.textBoxRefresh.Invoke(new Func<double>(() => double.Parse(this.textBoxRefresh.Text)));
						if (num2 > 0)
						{
							Thread.Sleep((int)(num2 * 1000));
						}
						stopwatch.Restart();
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(string.Concat("異常發生，停止外掛，錯誤原因:", exception.Message));
				}
			}
			finally
			{
				this._state = State.Stop;
				this.button1.Invoke(new Action(() => {
					this.button1.Text = "啟動";
					this.listBox1.Items.Clear();
					this.labelError.Text = "待機";
					this.labelError.ForeColor = Color.DarkBlue;
				}));
			}
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

		private void SendMessageToSimulator(string action, IntPtr hwndCalc)
		{
			string[] strArrays = this._hotKeyList.Split(new char[] { ',' });
			uint num = 256;
			uint num1 = 257;
			IntPtr zero = IntPtr.Zero;
			zero = (!this.textBoxSimulatorName.Text.Contains("雷電") ? hwndCalc : WinApi.FindWindowEx(hwndCalc, 0, "RenderWindow", null));
			if (action == strArrays[0])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.D7), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.D7), 0);
				return;
			}
			if (action == strArrays[1])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.D8), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.D8), 0);
				return;
			}
			if (action == strArrays[2])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.D9), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.D9), 0);
				return;
			}
			if (action == strArrays[3])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.D0), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.D0), 0);
				return;
			}
			if (action == strArrays[4])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.U), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.U), 0);
				return;
			}
			if (action == strArrays[5])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.I), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.I), 0);
				return;
			}
			if (action == strArrays[6])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.O), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.O), 0);
				return;
			}
			if (action == strArrays[7])
			{
				WinApi.SendMessage(zero, num, Convert.ToInt32(Keys.P), 0);
				Thread.Sleep(100);
				WinApi.SendMessage(zero, num1, Convert.ToInt32(Keys.P), 0);
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
		}
	}
}
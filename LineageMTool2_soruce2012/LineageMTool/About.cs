using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LineageMTool
{
	public class About : Form
	{
		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private Button button1;

		public About()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.button1 = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label1.Location = new Point(12, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 64);
			this.label1.TabIndex = 0;
			this.label1.Text = "此外掛只是下班時順手開發，\r\n能用就好，不要強求太多，\r\n至於什麼時候會更新，\r\n就等哪天有閒暇時間再來更新了\r\n";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label2.Location = new Point(12, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(234, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "聯絡資訊: sd016808@hotmail.com";
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("新細明體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
			this.label3.Location = new Point(12, 186);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "作者: Harris.Dai";
			this.button1.Location = new Point(197, 226);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "確認";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(284, 261);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Name = "About";
			this.Text = "About";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
namespace DBMigration
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxProjectFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxInputCmd = new System.Windows.Forms.TextBox();
            this.textBoxMlog = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxShowLog = new System.Windows.Forms.TextBox();
            this.showconsollable = new System.Windows.Forms.Label();
            this.buttonCodeGen = new System.Windows.Forms.Button();
            this.comboBoxGenCode = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 471);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxProjectFile);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxInputCmd);
            this.tabPage1.Controls.Add(this.textBoxMlog);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 445);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据迁移";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxProjectFile
            // 
            this.textBoxProjectFile.Location = new System.Drawing.Point(173, 154);
            this.textBoxProjectFile.Name = "textBoxProjectFile";
            this.textBoxProjectFile.Size = new System.Drawing.Size(334, 21);
            this.textBoxProjectFile.TabIndex = 8;
            this.textBoxProjectFile.Text = "E:\\下载代码\\Github\\DBMigrationTool";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "注意：请修改对应项目目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "输出...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "输入...";
            // 
            // textBoxInputCmd
            // 
            this.textBoxInputCmd.Location = new System.Drawing.Point(8, 29);
            this.textBoxInputCmd.Multiline = true;
            this.textBoxInputCmd.Name = "textBoxInputCmd";
            this.textBoxInputCmd.Size = new System.Drawing.Size(743, 103);
            this.textBoxInputCmd.TabIndex = 4;
            this.textBoxInputCmd.Text = resources.GetString("textBoxInputCmd.Text");
            // 
            // textBoxMlog
            // 
            this.textBoxMlog.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBoxMlog.ForeColor = System.Drawing.Color.Lime;
            this.textBoxMlog.Location = new System.Drawing.Point(8, 263);
            this.textBoxMlog.Multiline = true;
            this.textBoxMlog.Name = "textBoxMlog";
            this.textBoxMlog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMlog.Size = new System.Drawing.Size(743, 144);
            this.textBoxMlog.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(676, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 48);
            this.button2.TabIndex = 2;
            this.button2.Text = "Runner";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxShowLog);
            this.tabPage2.Controls.Add(this.showconsollable);
            this.tabPage2.Controls.Add(this.buttonCodeGen);
            this.tabPage2.Controls.Add(this.comboBoxGenCode);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "生成迁移文件";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // textBoxShowLog
            // 
            this.textBoxShowLog.BackColor = System.Drawing.SystemColors.Desktop;
            this.textBoxShowLog.ForeColor = System.Drawing.Color.Lime;
            this.textBoxShowLog.Location = new System.Drawing.Point(11, 220);
            this.textBoxShowLog.Multiline = true;
            this.textBoxShowLog.Name = "textBoxShowLog";
            this.textBoxShowLog.ReadOnly = true;
            this.textBoxShowLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxShowLog.Size = new System.Drawing.Size(775, 196);
            this.textBoxShowLog.TabIndex = 4;
            this.textBoxShowLog.TextChanged += new System.EventHandler(this.textBoxShowLog_TextChanged);
            // 
            // showconsollable
            // 
            this.showconsollable.AutoSize = true;
            this.showconsollable.Location = new System.Drawing.Point(9, 204);
            this.showconsollable.Name = "showconsollable";
            this.showconsollable.Size = new System.Drawing.Size(41, 12);
            this.showconsollable.TabIndex = 3;
            this.showconsollable.Text = "输出：";
            // 
            // buttonCodeGen
            // 
            this.buttonCodeGen.Location = new System.Drawing.Point(348, 70);
            this.buttonCodeGen.Name = "buttonCodeGen";
            this.buttonCodeGen.Size = new System.Drawing.Size(75, 23);
            this.buttonCodeGen.TabIndex = 1;
            this.buttonCodeGen.Text = "生成";
            this.buttonCodeGen.UseVisualStyleBackColor = true;
            this.buttonCodeGen.Click += new System.EventHandler(this.buttonCodeGen_Click);
            // 
            // comboBoxGenCode
            // 
            this.comboBoxGenCode.FormattingEnabled = true;
            this.comboBoxGenCode.Items.AddRange(new object[] {
            "1.生成创建表代码",
            "2.生成删除表代码"});
            this.comboBoxGenCode.Location = new System.Drawing.Point(174, 72);
            this.comboBoxGenCode.Name = "comboBoxGenCode";
            this.comboBoxGenCode.Size = new System.Drawing.Size(152, 20);
            this.comboBoxGenCode.TabIndex = 0;
            this.comboBoxGenCode.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 445);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "软件说明";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(34, 341);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参考";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(738, 76);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "1.官网https://fluentmigrator.github.io/index.html\r\n2.\r\n3.\r\n";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(34, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "命令行说明";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 20);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(738, 272);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 471);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "数据库迁移助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonCodeGen;
        private System.Windows.Forms.ComboBox comboBoxGenCode;
        private System.Windows.Forms.Label showconsollable;
        private System.Windows.Forms.TextBox textBoxShowLog;
        private System.Windows.Forms.TextBox textBoxMlog;
        private System.Windows.Forms.TextBox textBoxInputCmd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxProjectFile;
    }
}


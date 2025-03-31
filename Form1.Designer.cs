namespace jmsTools
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.EthlabelRunTime = new System.Windows.Forms.Label();
            this.EthtextBoxLog = new System.Windows.Forms.TextBox();
            this.Ethbtn_stop = new System.Windows.Forms.Button();
            this.Ethbtn_start = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BalanceInfo = new System.Windows.Forms.Label();
            this.text_transaction = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.oneaddress_cx = new System.Windows.Forms.Button();
            this.oneaddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.生成配置 = new System.Windows.Forms.GroupBox();
            this.taskNum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.stop_btn = new System.Windows.Forms.Button();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.generate_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSuffix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.xample_str = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelResults = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ygtime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.probability_lab = new System.Windows.Forms.Label();
            this.labbel10 = new System.Windows.Forms.Label();
            this.generated_lab = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.complexity_lab = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabpage5 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.analysis_Log = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_analysis = new System.Windows.Forms.Button();
            this.com_Currency = new System.Windows.Forms.ComboBox();
            this.com_Cycle = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMainNet = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_Countdown = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.生成配置.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskNum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabpage5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabpage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 440);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.EthlabelRunTime);
            this.tabPage1.Controls.Add(this.EthtextBoxLog);
            this.tabPage1.Controls.Add(this.Ethbtn_stop);
            this.tabPage1.Controls.Add(this.Ethbtn_start);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(908, 414);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "私钥碰撞";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // EthlabelRunTime
            // 
            this.EthlabelRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EthlabelRunTime.AutoSize = true;
            this.EthlabelRunTime.Location = new System.Drawing.Point(25, 366);
            this.EthlabelRunTime.Name = "EthlabelRunTime";
            this.EthlabelRunTime.Size = new System.Drawing.Size(29, 12);
            this.EthlabelRunTime.TabIndex = 2;
            this.EthlabelRunTime.Text = "日志";
            // 
            // EthtextBoxLog
            // 
            this.EthtextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EthtextBoxLog.Location = new System.Drawing.Point(3, 6);
            this.EthtextBoxLog.Multiline = true;
            this.EthtextBoxLog.Name = "EthtextBoxLog";
            this.EthtextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.EthtextBoxLog.Size = new System.Drawing.Size(896, 336);
            this.EthtextBoxLog.TabIndex = 0;
            // 
            // Ethbtn_stop
            // 
            this.Ethbtn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ethbtn_stop.Location = new System.Drawing.Point(410, 361);
            this.Ethbtn_stop.Name = "Ethbtn_stop";
            this.Ethbtn_stop.Size = new System.Drawing.Size(75, 23);
            this.Ethbtn_stop.TabIndex = 4;
            this.Ethbtn_stop.Text = "停止";
            this.Ethbtn_stop.UseVisualStyleBackColor = true;
            this.Ethbtn_stop.Click += new System.EventHandler(this.Ethbtn_stop_Click);
            // 
            // Ethbtn_start
            // 
            this.Ethbtn_start.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ethbtn_start.Location = new System.Drawing.Point(318, 361);
            this.Ethbtn_start.Name = "Ethbtn_start";
            this.Ethbtn_start.Size = new System.Drawing.Size(75, 23);
            this.Ethbtn_start.TabIndex = 3;
            this.Ethbtn_start.Text = "开始执行";
            this.Ethbtn_start.UseVisualStyleBackColor = true;
            this.Ethbtn_start.Click += new System.EventHandler(this.Ethbtn_start_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BalanceInfo);
            this.tabPage2.Controls.Add(this.text_transaction);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.oneaddress_cx);
            this.tabPage2.Controls.Add(this.oneaddress);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(908, 414);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "单地址查询";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BalanceInfo
            // 
            this.BalanceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BalanceInfo.AutoSize = true;
            this.BalanceInfo.Location = new System.Drawing.Point(88, 55);
            this.BalanceInfo.Name = "BalanceInfo";
            this.BalanceInfo.Size = new System.Drawing.Size(11, 12);
            this.BalanceInfo.TabIndex = 6;
            this.BalanceInfo.Text = "0";
            // 
            // text_transaction
            // 
            this.text_transaction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_transaction.Location = new System.Drawing.Point(17, 89);
            this.text_transaction.Multiline = true;
            this.text_transaction.Name = "text_transaction";
            this.text_transaction.Size = new System.Drawing.Size(867, 303);
            this.text_transaction.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "持仓分布：";
            // 
            // oneaddress_cx
            // 
            this.oneaddress_cx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneaddress_cx.Location = new System.Drawing.Point(540, 16);
            this.oneaddress_cx.Name = "oneaddress_cx";
            this.oneaddress_cx.Size = new System.Drawing.Size(75, 23);
            this.oneaddress_cx.TabIndex = 2;
            this.oneaddress_cx.Text = "查询";
            this.oneaddress_cx.UseVisualStyleBackColor = true;
            this.oneaddress_cx.Click += new System.EventHandler(this.oneaddress_cx_Click);
            // 
            // oneaddress
            // 
            this.oneaddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneaddress.Location = new System.Drawing.Point(182, 16);
            this.oneaddress.Name = "oneaddress";
            this.oneaddress.Size = new System.Drawing.Size(339, 21);
            this.oneaddress.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "请输入对应主网的钱包地址：";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(908, 414);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "行情监控";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 25);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(671, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "开发中.....尽请期待，这里后续应该会弄成可以微信订阅推送的那种，比如设置波动范围，达到某个范围后推送微信或者邮箱";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.生成配置);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(908, 414);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "靓号生成";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // 生成配置
            // 
            this.生成配置.Controls.Add(this.taskNum);
            this.生成配置.Controls.Add(this.label4);
            this.生成配置.Controls.Add(this.stop_btn);
            this.生成配置.Controls.Add(this.textBoxPrefix);
            this.生成配置.Controls.Add(this.generate_btn);
            this.生成配置.Controls.Add(this.label5);
            this.生成配置.Controls.Add(this.label7);
            this.生成配置.Controls.Add(this.textBoxSuffix);
            this.生成配置.Controls.Add(this.label6);
            this.生成配置.Controls.Add(this.checkBox1);
            this.生成配置.Controls.Add(this.xample_str);
            this.生成配置.Location = new System.Drawing.Point(20, 18);
            this.生成配置.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.生成配置.Name = "生成配置";
            this.生成配置.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.生成配置.Size = new System.Drawing.Size(469, 316);
            this.生成配置.TabIndex = 12;
            this.生成配置.TabStop = false;
            this.生成配置.Text = "生成配置";
            // 
            // taskNum
            // 
            this.taskNum.Location = new System.Drawing.Point(98, 100);
            this.taskNum.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.taskNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.taskNum.Name = "taskNum";
            this.taskNum.Size = new System.Drawing.Size(90, 21);
            this.taskNum.TabIndex = 7;
            this.taskNum.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.taskNum.ValueChanged += new System.EventHandler(this.taskNum_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "例如：";
            // 
            // stop_btn
            // 
            this.stop_btn.Location = new System.Drawing.Point(213, 215);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(75, 23);
            this.stop_btn.TabIndex = 10;
            this.stop_btn.Text = "停止";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(88, 28);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(100, 21);
            this.textBoxPrefix.TabIndex = 1;
            this.textBoxPrefix.TextChanged += new System.EventHandler(this.textBoxPrefix_TextChanged);
            // 
            // generate_btn
            // 
            this.generate_btn.Location = new System.Drawing.Point(98, 215);
            this.generate_btn.Name = "generate_btn";
            this.generate_btn.Size = new System.Drawing.Size(75, 23);
            this.generate_btn.TabIndex = 9;
            this.generate_btn.Text = "生成";
            this.generate_btn.UseVisualStyleBackColor = true;
            this.generate_btn.Click += new System.EventHandler(this.generate_btn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "前缀：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "线程数：";
            // 
            // textBoxSuffix
            // 
            this.textBoxSuffix.Location = new System.Drawing.Point(269, 29);
            this.textBoxSuffix.Name = "textBoxSuffix";
            this.textBoxSuffix.Size = new System.Drawing.Size(100, 21);
            this.textBoxSuffix.TabIndex = 3;
            this.textBoxSuffix.TextChanged += new System.EventHandler(this.textBoxSuffix_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "后缀：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(43, 69);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "区分大小写";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // xample_str
            // 
            this.xample_str.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xample_str.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xample_str.Location = new System.Drawing.Point(88, 139);
            this.xample_str.Name = "xample_str";
            this.xample_str.ReadOnly = true;
            this.xample_str.Size = new System.Drawing.Size(326, 24);
            this.xample_str.TabIndex = 5;
            this.xample_str.Text = "0xa83114A443dA1CecEFC50368531cACE9F37fCCcb";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelResults);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.ygtime);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.probability_lab);
            this.groupBox1.Controls.Add(this.labbel10);
            this.groupBox1.Controls.Add(this.generated_lab);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.complexity_lab);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(516, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 316);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详情";
            // 
            // labelResults
            // 
            this.labelResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelResults.Location = new System.Drawing.Point(25, 171);
            this.labelResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelResults.Multiline = true;
            this.labelResults.Name = "labelResults";
            this.labelResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.labelResults.Size = new System.Drawing.Size(307, 135);
            this.labelResults.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "输出：";
            // 
            // ygtime
            // 
            this.ygtime.AutoSize = true;
            this.ygtime.Location = new System.Drawing.Point(82, 108);
            this.ygtime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ygtime.Name = "ygtime";
            this.ygtime.Size = new System.Drawing.Size(11, 12);
            this.ygtime.TabIndex = 7;
            this.ygtime.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "预估时间：";
            // 
            // probability_lab
            // 
            this.probability_lab.AutoSize = true;
            this.probability_lab.Location = new System.Drawing.Point(82, 81);
            this.probability_lab.Name = "probability_lab";
            this.probability_lab.Size = new System.Drawing.Size(11, 12);
            this.probability_lab.TabIndex = 5;
            this.probability_lab.Text = "0";
            // 
            // labbel10
            // 
            this.labbel10.AutoSize = true;
            this.labbel10.Location = new System.Drawing.Point(23, 81);
            this.labbel10.Name = "labbel10";
            this.labbel10.Size = new System.Drawing.Size(59, 12);
            this.labbel10.TabIndex = 4;
            this.labbel10.Text = "50%概率：";
            // 
            // generated_lab
            // 
            this.generated_lab.AutoSize = true;
            this.generated_lab.Location = new System.Drawing.Point(82, 52);
            this.generated_lab.Name = "generated_lab";
            this.generated_lab.Size = new System.Drawing.Size(11, 12);
            this.generated_lab.TabIndex = 3;
            this.generated_lab.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "已生成：";
            // 
            // complexity_lab
            // 
            this.complexity_lab.AutoSize = true;
            this.complexity_lab.Location = new System.Drawing.Point(82, 27);
            this.complexity_lab.Name = "complexity_lab";
            this.complexity_lab.Size = new System.Drawing.Size(11, 12);
            this.complexity_lab.TabIndex = 1;
            this.complexity_lab.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "复杂度：";
            // 
            // tabpage5
            // 
            this.tabpage5.Controls.Add(this.groupBox3);
            this.tabpage5.Controls.Add(this.groupBox2);
            this.tabpage5.Location = new System.Drawing.Point(4, 22);
            this.tabpage5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabpage5.Name = "tabpage5";
            this.tabpage5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabpage5.Size = new System.Drawing.Size(908, 414);
            this.tabpage5.TabIndex = 4;
            this.tabpage5.Text = "合约分析工具";
            this.tabpage5.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.linkLabel2);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.analysis_Log);
            this.groupBox3.Location = new System.Drawing.Point(297, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(609, 364);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "分析日志";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(409, 16);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(77, 12);
            this.linkLabel2.TabIndex = 8;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "依据指标说明";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 16);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(353, 12);
            this.label15.TabIndex = 7;
            this.label15.Text = "该功能仅供参考，不承担经济损失责任，合约有风险，需慎重投资";
            // 
            // analysis_Log
            // 
            this.analysis_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysis_Log.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.analysis_Log.Location = new System.Drawing.Point(17, 41);
            this.analysis_Log.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.analysis_Log.Multiline = true;
            this.analysis_Log.Name = "analysis_Log";
            this.analysis_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.analysis_Log.Size = new System.Drawing.Size(589, 309);
            this.analysis_Log.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbl_Countdown);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btn_analysis);
            this.groupBox2.Controls.Add(this.com_Currency);
            this.groupBox2.Controls.Add(this.com_Cycle);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(23, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(234, 364);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合约分析工具：仅供参考";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 104);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "选择币种：";
            // 
            // btn_analysis
            // 
            this.btn_analysis.Location = new System.Drawing.Point(65, 193);
            this.btn_analysis.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_analysis.Name = "btn_analysis";
            this.btn_analysis.Size = new System.Drawing.Size(103, 29);
            this.btn_analysis.TabIndex = 4;
            this.btn_analysis.Text = "开始分析";
            this.btn_analysis.UseVisualStyleBackColor = true;
            this.btn_analysis.Click += new System.EventHandler(this.btn_analysis_Click);
            // 
            // com_Currency
            // 
            this.com_Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Currency.FormattingEnabled = true;
            this.com_Currency.Items.AddRange(new object[] {
            "BTC/USDT",
            "ETH/USDT"});
            this.com_Currency.Location = new System.Drawing.Point(99, 102);
            this.com_Currency.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.com_Currency.Name = "com_Currency";
            this.com_Currency.Size = new System.Drawing.Size(98, 20);
            this.com_Currency.TabIndex = 1;
            // 
            // com_Cycle
            // 
            this.com_Cycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Cycle.FormattingEnabled = true;
            this.com_Cycle.Items.AddRange(new object[] {
            "10分钟",
            "30分钟",
            "1小时",
            "4小时"});
            this.com_Cycle.Location = new System.Drawing.Point(99, 135);
            this.com_Cycle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.com_Cycle.Name = "com_Cycle";
            this.com_Cycle.Size = new System.Drawing.Size(98, 20);
            this.com_Cycle.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(34, 137);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "选择周期：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "请选择主网：";
            // 
            // comboBoxMainNet
            // 
            this.comboBoxMainNet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMainNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMainNet.FormattingEnabled = true;
            this.comboBoxMainNet.Items.AddRange(new object[] {
            "ETH",
            "BSC",
            "SOL"});
            this.comboBoxMainNet.Location = new System.Drawing.Point(102, 18);
            this.comboBoxMainNet.Name = "comboBoxMainNet";
            this.comboBoxMainNet.Size = new System.Drawing.Size(121, 20);
            this.comboBoxMainNet.TabIndex = 5;
            this.comboBoxMainNet.SelectedIndexChanged += new System.EventHandler(this.comboBoxMainNet_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(860, 22);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "币圈工具箱";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_Countdown
            // 
            this.lbl_Countdown.AutoSize = true;
            this.lbl_Countdown.Location = new System.Drawing.Point(65, 299);
            this.lbl_Countdown.Name = "lbl_Countdown";
            this.lbl_Countdown.Size = new System.Drawing.Size(41, 12);
            this.lbl_Countdown.TabIndex = 5;
            this.lbl_Countdown.Text = "倒计时";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 510);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMainNet);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "加密社工具集";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.生成配置.ResumeLayout(false);
            this.生成配置.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskNum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabpage5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label EthlabelRunTime;
        private System.Windows.Forms.Button Ethbtn_start;
        private System.Windows.Forms.Button Ethbtn_stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMainNet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button oneaddress_cx;
        private System.Windows.Forms.TextBox oneaddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox text_transaction;
        private System.Windows.Forms.Label BalanceInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSuffix;
        private System.Windows.Forms.RichTextBox xample_str;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown taskNum;
        private System.Windows.Forms.Button generate_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label complexity_lab;
        private System.Windows.Forms.Label probability_lab;
        private System.Windows.Forms.Label labbel10;
        private System.Windows.Forms.Label generated_lab;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label ygtime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox labelResults;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabpage5;
        private System.Windows.Forms.TextBox EthtextBoxLog;
        private System.Windows.Forms.ComboBox com_Currency;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox com_Cycle;
        private System.Windows.Forms.Button btn_analysis;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox analysis_Log;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.GroupBox 生成配置;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_Countdown;
    }
}


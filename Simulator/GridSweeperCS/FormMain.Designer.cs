namespace GridSweeperCS
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.textBoxElapsedTime = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.icon = new System.Windows.Forms.ImageList(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numTruck = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gBSettingField = new System.Windows.Forms.GroupBox();
            this.nudCellSize = new System.Windows.Forms.NumericUpDown();
            this.nudFieldLength = new System.Windows.Forms.NumericUpDown();
            this.nudFieldWidth = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.gBSettingFire = new System.Windows.Forms.GroupBox();
            this.nudNumOfFire5 = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfFire4 = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfFire3 = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfFire2 = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfFire1 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.gBSettingFireTruck = new System.Windows.Forms.GroupBox();
            this.nudTimeForHelp = new System.Windows.Forms.NumericUpDown();
            this.nudFailureProbability = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfHelpFireTruck = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfBrokenFireTruck = new System.Windows.Forms.NumericUpDown();
            this.nudNumOfNormalFireTruck = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gBSettingWind = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cBWindDirection = new System.Windows.Forms.ComboBox();
            this.nudWindLevel = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gBSettingTimer = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudTimersecond = new System.Windows.Forms.NumericUpDown();
            this.nudTimerMinute = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemScenario = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemTraining1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemTraining2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExp1 = new System.Windows.Forms.ToolStripMenuItem();
            this.シナリオToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.練習ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picBoxWindDirection = new System.Windows.Forms.PictureBox();
            this.lbWindLevel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxTimeLimit = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonWithdraw = new System.Windows.Forms.Button();
            this.btnTrack4 = new System.Windows.Forms.Button();
            this.btnTrack2 = new System.Windows.Forms.Button();
            this.btnTrack3 = new System.Windows.Forms.Button();
            this.picTrack = new System.Windows.Forms.PictureBox();
            this.btnTrack1 = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.textBoxScenarioName = new System.Windows.Forms.TextBox();
            this.lv1button = new System.Windows.Forms.Button();
            this.lv2button = new System.Windows.Forms.Button();
            this.lv3button = new System.Windows.Forms.Button();
            this.lv4button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gBSettingField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldWidth)).BeginInit();
            this.gBSettingFire.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire1)).BeginInit();
            this.gBSettingFireTruck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeForHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFailureProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfHelpFireTruck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfBrokenFireTruck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfNormalFireTruck)).BeginInit();
            this.gBSettingWind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWindLevel)).BeginInit();
            this.gBSettingTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimersecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimerMinute)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWindDirection)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowDrop = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Location = new System.Drawing.Point(3, 4);
            this.dgv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 82;
            this.dgv.RowTemplate.Height = 21;
            this.dgv.ShowCellToolTips = false;
            this.dgv.Size = new System.Drawing.Size(852, 550);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CellContentClick_1);
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            this.dgv.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.dgv.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_DragEnter);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(10, 110);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 40);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textBoxElapsedTime
            // 
            this.textBoxElapsedTime.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxElapsedTime.Location = new System.Drawing.Point(10, 68);
            this.textBoxElapsedTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxElapsedTime.Name = "textBoxElapsedTime";
            this.textBoxElapsedTime.Size = new System.Drawing.Size(151, 31);
            this.textBoxElapsedTime.TabIndex = 2;
            this.textBoxElapsedTime.Text = "00:00:00";
            this.textBoxElapsedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxElapsedTime.TextChanged += new System.EventHandler(this.textBoxElapsedTime_TextChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(89, 110);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(72, 40);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 500);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 19);
            this.label1.TabIndex = 4;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(691, 521);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 19);
            this.label2.TabIndex = 6;
            // 
            // icon
            // 
            this.icon.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.icon.ImageSize = new System.Drawing.Size(16, 16);
            this.icon.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(168, 110);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(72, 40);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "×";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // numTruck
            // 
            this.numTruck.AutoSize = true;
            this.numTruck.Location = new System.Drawing.Point(199, 104);
            this.numTruck.Name = "numTruck";
            this.numTruck.Size = new System.Drawing.Size(18, 19);
            this.numTruck.TabIndex = 14;
            this.numTruck.Text = "5";
            this.numTruck.Click += new System.EventHandler(this.numTrack_Click);
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(27, 56);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 586);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(858, 554);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "メイン画面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gBSettingField);
            this.tabPage2.Controls.Add(this.gBSettingFire);
            this.tabPage2.Controls.Add(this.gBSettingFireTruck);
            this.tabPage2.Controls.Add(this.gBSettingWind);
            this.tabPage2.Controls.Add(this.gBSettingTimer);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(858, 554);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "設定画面";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gBSettingField
            // 
            this.gBSettingField.Controls.Add(this.nudCellSize);
            this.gBSettingField.Controls.Add(this.nudFieldLength);
            this.gBSettingField.Controls.Add(this.nudFieldWidth);
            this.gBSettingField.Controls.Add(this.label23);
            this.gBSettingField.Controls.Add(this.label22);
            this.gBSettingField.Controls.Add(this.label21);
            this.gBSettingField.Location = new System.Drawing.Point(600, 26);
            this.gBSettingField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingField.Name = "gBSettingField";
            this.gBSettingField.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingField.Size = new System.Drawing.Size(226, 146);
            this.gBSettingField.TabIndex = 4;
            this.gBSettingField.TabStop = false;
            this.gBSettingField.Text = "フィールド設定";
            // 
            // nudCellSize
            // 
            this.nudCellSize.Location = new System.Drawing.Point(91, 109);
            this.nudCellSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudCellSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudCellSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCellSize.Name = "nudCellSize";
            this.nudCellSize.Size = new System.Drawing.Size(65, 27);
            this.nudCellSize.TabIndex = 10;
            this.nudCellSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudFieldLength
            // 
            this.nudFieldLength.Location = new System.Drawing.Point(91, 78);
            this.nudFieldLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudFieldLength.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudFieldLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudFieldLength.Name = "nudFieldLength";
            this.nudFieldLength.Size = new System.Drawing.Size(65, 27);
            this.nudFieldLength.TabIndex = 9;
            this.nudFieldLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudFieldWidth
            // 
            this.nudFieldWidth.Location = new System.Drawing.Point(91, 45);
            this.nudFieldWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudFieldWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudFieldWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudFieldWidth.Name = "nudFieldWidth";
            this.nudFieldWidth.Size = new System.Drawing.Size(65, 27);
            this.nudFieldWidth.TabIndex = 6;
            this.nudFieldWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 111);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(68, 19);
            this.label23.TabIndex = 8;
            this.label23.Text = "セルサイズ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(13, 80);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(34, 19);
            this.label22.TabIndex = 7;
            this.label22.Text = "高さ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 48);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(24, 19);
            this.label21.TabIndex = 6;
            this.label21.Text = "幅";
            // 
            // gBSettingFire
            // 
            this.gBSettingFire.Controls.Add(this.nudNumOfFire5);
            this.gBSettingFire.Controls.Add(this.nudNumOfFire4);
            this.gBSettingFire.Controls.Add(this.nudNumOfFire3);
            this.gBSettingFire.Controls.Add(this.nudNumOfFire2);
            this.gBSettingFire.Controls.Add(this.nudNumOfFire1);
            this.gBSettingFire.Controls.Add(this.label20);
            this.gBSettingFire.Controls.Add(this.label19);
            this.gBSettingFire.Controls.Add(this.label18);
            this.gBSettingFire.Controls.Add(this.label17);
            this.gBSettingFire.Controls.Add(this.label16);
            this.gBSettingFire.Controls.Add(this.label15);
            this.gBSettingFire.Controls.Add(this.label14);
            this.gBSettingFire.Location = new System.Drawing.Point(337, 215);
            this.gBSettingFire.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingFire.Name = "gBSettingFire";
            this.gBSettingFire.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingFire.Size = new System.Drawing.Size(474, 131);
            this.gBSettingFire.TabIndex = 3;
            this.gBSettingFire.TabStop = false;
            this.gBSettingFire.Text = "火災設定";
            // 
            // nudNumOfFire5
            // 
            this.nudNumOfFire5.Enabled = false;
            this.nudNumOfFire5.Location = new System.Drawing.Point(383, 71);
            this.nudNumOfFire5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfFire5.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudNumOfFire5.Name = "nudNumOfFire5";
            this.nudNumOfFire5.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfFire5.TabIndex = 16;
            this.nudNumOfFire5.Visible = false;
            this.nudNumOfFire5.ValueChanged += new System.EventHandler(this.nudNumOfFire5_ValueChanged);
            // 
            // nudNumOfFire4
            // 
            this.nudNumOfFire4.Enabled = false;
            this.nudNumOfFire4.Location = new System.Drawing.Point(300, 71);
            this.nudNumOfFire4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfFire4.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudNumOfFire4.Name = "nudNumOfFire4";
            this.nudNumOfFire4.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfFire4.TabIndex = 15;
            this.nudNumOfFire4.Visible = false;
            this.nudNumOfFire4.ValueChanged += new System.EventHandler(this.nudNumOfFire4_ValueChanged);
            // 
            // nudNumOfFire3
            // 
            this.nudNumOfFire3.Location = new System.Drawing.Point(225, 71);
            this.nudNumOfFire3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfFire3.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudNumOfFire3.Name = "nudNumOfFire3";
            this.nudNumOfFire3.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfFire3.TabIndex = 14;
            this.nudNumOfFire3.ValueChanged += new System.EventHandler(this.nudNumOfFire3_ValueChanged);
            // 
            // nudNumOfFire2
            // 
            this.nudNumOfFire2.Location = new System.Drawing.Point(153, 71);
            this.nudNumOfFire2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfFire2.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudNumOfFire2.Name = "nudNumOfFire2";
            this.nudNumOfFire2.Size = new System.Drawing.Size(54, 27);
            this.nudNumOfFire2.TabIndex = 13;
            this.nudNumOfFire2.ValueChanged += new System.EventHandler(this.nudNumOfFire2_ValueChanged);
            // 
            // nudNumOfFire1
            // 
            this.nudNumOfFire1.Location = new System.Drawing.Point(70, 71);
            this.nudNumOfFire1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfFire1.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudNumOfFire1.Name = "nudNumOfFire1";
            this.nudNumOfFire1.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfFire1.TabIndex = 12;
            this.nudNumOfFire1.ValueChanged += new System.EventHandler(this.nudNumOfFire1_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(9, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(24, 19);
            this.label20.TabIndex = 9;
            this.label20.Text = "数";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Enabled = false;
            this.label19.Location = new System.Drawing.Point(406, 41);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(18, 19);
            this.label19.TabIndex = 4;
            this.label19.Text = "5";
            this.label19.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Enabled = false;
            this.label18.Location = new System.Drawing.Point(328, 41);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(18, 19);
            this.label18.TabIndex = 8;
            this.label18.Text = "4";
            this.label18.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(240, 41);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(18, 19);
            this.label17.TabIndex = 4;
            this.label17.Text = "3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(174, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(18, 19);
            this.label16.TabIndex = 7;
            this.label16.Text = "2";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(99, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 19);
            this.label15.TabIndex = 3;
            this.label15.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 19);
            this.label14.TabIndex = 6;
            this.label14.Text = "火災レベル";
            // 
            // gBSettingFireTruck
            // 
            this.gBSettingFireTruck.Controls.Add(this.nudTimeForHelp);
            this.gBSettingFireTruck.Controls.Add(this.nudFailureProbability);
            this.gBSettingFireTruck.Controls.Add(this.nudNumOfHelpFireTruck);
            this.gBSettingFireTruck.Controls.Add(this.nudNumOfBrokenFireTruck);
            this.gBSettingFireTruck.Controls.Add(this.nudNumOfNormalFireTruck);
            this.gBSettingFireTruck.Controls.Add(this.label13);
            this.gBSettingFireTruck.Controls.Add(this.label12);
            this.gBSettingFireTruck.Controls.Add(this.label11);
            this.gBSettingFireTruck.Controls.Add(this.label10);
            this.gBSettingFireTruck.Controls.Add(this.label9);
            this.gBSettingFireTruck.Location = new System.Drawing.Point(23, 172);
            this.gBSettingFireTruck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingFireTruck.Name = "gBSettingFireTruck";
            this.gBSettingFireTruck.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingFireTruck.Size = new System.Drawing.Size(250, 268);
            this.gBSettingFireTruck.TabIndex = 2;
            this.gBSettingFireTruck.TabStop = false;
            this.gBSettingFireTruck.Text = "消防車設定";
            // 
            // nudTimeForHelp
            // 
            this.nudTimeForHelp.Enabled = false;
            this.nudTimeForHelp.Location = new System.Drawing.Point(159, 188);
            this.nudTimeForHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudTimeForHelp.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudTimeForHelp.Name = "nudTimeForHelp";
            this.nudTimeForHelp.Size = new System.Drawing.Size(65, 27);
            this.nudTimeForHelp.TabIndex = 11;
            // 
            // nudFailureProbability
            // 
            this.nudFailureProbability.Enabled = false;
            this.nudFailureProbability.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudFailureProbability.Location = new System.Drawing.Point(159, 114);
            this.nudFailureProbability.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudFailureProbability.Name = "nudFailureProbability";
            this.nudFailureProbability.Size = new System.Drawing.Size(65, 27);
            this.nudFailureProbability.TabIndex = 10;
            // 
            // nudNumOfHelpFireTruck
            // 
            this.nudNumOfHelpFireTruck.Enabled = false;
            this.nudNumOfHelpFireTruck.Location = new System.Drawing.Point(159, 150);
            this.nudNumOfHelpFireTruck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfHelpFireTruck.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudNumOfHelpFireTruck.Name = "nudNumOfHelpFireTruck";
            this.nudNumOfHelpFireTruck.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfHelpFireTruck.TabIndex = 4;
            // 
            // nudNumOfBrokenFireTruck
            // 
            this.nudNumOfBrokenFireTruck.Enabled = false;
            this.nudNumOfBrokenFireTruck.Location = new System.Drawing.Point(159, 75);
            this.nudNumOfBrokenFireTruck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfBrokenFireTruck.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudNumOfBrokenFireTruck.Name = "nudNumOfBrokenFireTruck";
            this.nudNumOfBrokenFireTruck.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfBrokenFireTruck.TabIndex = 9;
            // 
            // nudNumOfNormalFireTruck
            // 
            this.nudNumOfNormalFireTruck.Location = new System.Drawing.Point(159, 29);
            this.nudNumOfNormalFireTruck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudNumOfNormalFireTruck.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudNumOfNormalFireTruck.Name = "nudNumOfNormalFireTruck";
            this.nudNumOfNormalFireTruck.Size = new System.Drawing.Size(65, 27);
            this.nudNumOfNormalFireTruck.TabIndex = 3;
            this.nudNumOfNormalFireTruck.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudNumOfNormalFireTruck.ValueChanged += new System.EventHandler(this.nudNumOfNormalFireTruck_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(19, 190);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 19);
            this.label13.TabIndex = 8;
            this.label13.Text = "救援までの時間";
            this.label13.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(19, 152);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 19);
            this.label12.TabIndex = 4;
            this.label12.Text = "救援台数";
            this.label12.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(19, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 19);
            this.label11.TabIndex = 7;
            this.label11.Text = "故障確率";
            this.label11.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(19, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 19);
            this.label10.TabIndex = 4;
            this.label10.Text = "故障台数";
            this.label10.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 19);
            this.label9.TabIndex = 6;
            this.label9.Text = "消防車残台数";
            // 
            // gBSettingWind
            // 
            this.gBSettingWind.Controls.Add(this.button1);
            this.gBSettingWind.Controls.Add(this.cBWindDirection);
            this.gBSettingWind.Controls.Add(this.nudWindLevel);
            this.gBSettingWind.Controls.Add(this.label8);
            this.gBSettingWind.Controls.Add(this.label7);
            this.gBSettingWind.Location = new System.Drawing.Point(328, 25);
            this.gBSettingWind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingWind.Name = "gBSettingWind";
            this.gBSettingWind.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingWind.Size = new System.Drawing.Size(227, 161);
            this.gBSettingWind.TabIndex = 1;
            this.gBSettingWind.TabStop = false;
            this.gBSettingWind.Text = "風設定";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "Override";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cBWindDirection
            // 
            this.cBWindDirection.FormattingEnabled = true;
            this.cBWindDirection.Items.AddRange(new object[] {
            "",
            "北",
            "北東",
            "東",
            "南東",
            "南",
            "南西",
            "西",
            "北西"});
            this.cBWindDirection.Location = new System.Drawing.Point(111, 80);
            this.cBWindDirection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cBWindDirection.Name = "cBWindDirection";
            this.cBWindDirection.Size = new System.Drawing.Size(84, 27);
            this.cBWindDirection.TabIndex = 5;
            this.cBWindDirection.SelectedIndexChanged += new System.EventHandler(this.cBWindDirection_SelectedIndexChanged);
            // 
            // nudWindLevel
            // 
            this.nudWindLevel.Location = new System.Drawing.Point(111, 40);
            this.nudWindLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudWindLevel.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudWindLevel.Name = "nudWindLevel";
            this.nudWindLevel.Size = new System.Drawing.Size(65, 27);
            this.nudWindLevel.TabIndex = 3;
            this.nudWindLevel.ValueChanged += new System.EventHandler(this.nudWindLevel_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 19);
            this.label8.TabIndex = 4;
            this.label8.Text = "風向";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 19);
            this.label7.TabIndex = 3;
            this.label7.Text = "風速レベル";
            // 
            // gBSettingTimer
            // 
            this.gBSettingTimer.Controls.Add(this.label5);
            this.gBSettingTimer.Controls.Add(this.label6);
            this.gBSettingTimer.Controls.Add(this.nudTimersecond);
            this.gBSettingTimer.Controls.Add(this.nudTimerMinute);
            this.gBSettingTimer.Location = new System.Drawing.Point(17, 25);
            this.gBSettingTimer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingTimer.Name = "gBSettingTimer";
            this.gBSettingTimer.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gBSettingTimer.Size = new System.Drawing.Size(254, 96);
            this.gBSettingTimer.TabIndex = 0;
            this.gBSettingTimer.TabStop = false;
            this.gBSettingTimer.Text = "制限時間設定";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 19);
            this.label5.TabIndex = 1;
            this.label5.Text = "分";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "秒";
            // 
            // nudTimersecond
            // 
            this.nudTimersecond.Location = new System.Drawing.Point(147, 41);
            this.nudTimersecond.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudTimersecond.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudTimersecond.Name = "nudTimersecond";
            this.nudTimersecond.Size = new System.Drawing.Size(65, 27);
            this.nudTimersecond.TabIndex = 1;
            // 
            // nudTimerMinute
            // 
            this.nudTimerMinute.Location = new System.Drawing.Point(21, 41);
            this.nudTimerMinute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudTimerMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudTimerMinute.Name = "nudTimerMinute";
            this.nudTimerMinute.Size = new System.Drawing.Size(65, 27);
            this.nudTimerMinute.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemScenario});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(77, 28);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemScenario
            // 
            this.ToolStripMenuItemScenario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemTraining1,
            this.ToolStripMenuItemTraining2,
            this.ToolStripMenuItemExp1});
            this.ToolStripMenuItemScenario.Name = "ToolStripMenuItemScenario";
            this.ToolStripMenuItemScenario.Size = new System.Drawing.Size(68, 24);
            this.ToolStripMenuItemScenario.Text = "シナリオ";
            // 
            // ToolStripMenuItemTraining1
            // 
            this.ToolStripMenuItemTraining1.Name = "ToolStripMenuItemTraining1";
            this.ToolStripMenuItemTraining1.Size = new System.Drawing.Size(152, 26);
            this.ToolStripMenuItemTraining1.Text = "操作練習";
            this.ToolStripMenuItemTraining1.Click += new System.EventHandler(this.ToolStripMenuItemTraining1_Click);
            // 
            // ToolStripMenuItemTraining2
            // 
            this.ToolStripMenuItemTraining2.Name = "ToolStripMenuItemTraining2";
            this.ToolStripMenuItemTraining2.Size = new System.Drawing.Size(152, 26);
            this.ToolStripMenuItemTraining2.Text = "練習";
            this.ToolStripMenuItemTraining2.Click += new System.EventHandler(this.ToolStripMenuItemTraining2_Click);
            // 
            // ToolStripMenuItemExp1
            // 
            this.ToolStripMenuItemExp1.Name = "ToolStripMenuItemExp1";
            this.ToolStripMenuItemExp1.Size = new System.Drawing.Size(152, 26);
            this.ToolStripMenuItemExp1.Text = "実験";
            this.ToolStripMenuItemExp1.Click += new System.EventHandler(this.ToolStripMenuItemExp1_Click);
            // 
            // シナリオToolStripMenuItem
            // 
            this.シナリオToolStripMenuItem.Name = "シナリオToolStripMenuItem";
            this.シナリオToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 練習ToolStripMenuItem
            // 
            this.練習ToolStripMenuItem.Name = "練習ToolStripMenuItem";
            this.練習ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.picBoxWindDirection);
            this.groupBox1.Controls.Add(this.lbWindLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(718, 650);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(147, 136);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "風";
            // 
            // picBoxWindDirection
            // 
            this.picBoxWindDirection.Location = new System.Drawing.Point(40, 19);
            this.picBoxWindDirection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picBoxWindDirection.Name = "picBoxWindDirection";
            this.picBoxWindDirection.Size = new System.Drawing.Size(70, 75);
            this.picBoxWindDirection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxWindDirection.TabIndex = 21;
            this.picBoxWindDirection.TabStop = false;
            // 
            // lbWindLevel
            // 
            this.lbWindLevel.AutoSize = true;
            this.lbWindLevel.Location = new System.Drawing.Point(101, 102);
            this.lbWindLevel.Name = "lbWindLevel";
            this.lbWindLevel.Size = new System.Drawing.Size(18, 19);
            this.lbWindLevel.TabIndex = 12;
            this.lbWindLevel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "風速レベル";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.textBoxTimeLimit);
            this.groupBox2.Controls.Add(this.textBoxElapsedTime);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Location = new System.Drawing.Point(46, 650);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(246, 161);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "制限時間";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // textBoxTimeLimit
            // 
            this.textBoxTimeLimit.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxTimeLimit.Location = new System.Drawing.Point(10, 34);
            this.textBoxTimeLimit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTimeLimit.Name = "textBoxTimeLimit";
            this.textBoxTimeLimit.ReadOnly = true;
            this.textBoxTimeLimit.Size = new System.Drawing.Size(151, 31);
            this.textBoxTimeLimit.TabIndex = 13;
            this.textBoxTimeLimit.Text = "00:00:00";
            this.textBoxTimeLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.Controls.Add(this.buttonWithdraw);
            this.groupBox3.Controls.Add(this.btnTrack4);
            this.groupBox3.Controls.Add(this.numTruck);
            this.groupBox3.Controls.Add(this.btnTrack2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnTrack3);
            this.groupBox3.Controls.Add(this.picTrack);
            this.groupBox3.Controls.Add(this.btnTrack1);
            this.groupBox3.Location = new System.Drawing.Point(327, 639);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(365, 161);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "消火用ロボット";
            // 
            // buttonWithdraw
            // 
            this.buttonWithdraw.Enabled = false;
            this.buttonWithdraw.Location = new System.Drawing.Point(155, 125);
            this.buttonWithdraw.Name = "buttonWithdraw";
            this.buttonWithdraw.Size = new System.Drawing.Size(80, 30);
            this.buttonWithdraw.TabIndex = 26;
            this.buttonWithdraw.Text = "Withdraw";
            this.buttonWithdraw.UseVisualStyleBackColor = true;
            this.buttonWithdraw.Click += new System.EventHandler(this.buttonWithdraw_Click);
            // 
            // btnTrack4
            // 
            this.btnTrack4.BackColor = System.Drawing.Color.LightGray;
            this.btnTrack4.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTrack4.Location = new System.Drawing.Point(6, 70);
            this.btnTrack4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTrack4.Name = "btnTrack4";
            this.btnTrack4.Size = new System.Drawing.Size(35, 35);
            this.btnTrack4.TabIndex = 24;
            this.btnTrack4.Text = "◁";
            this.btnTrack4.UseVisualStyleBackColor = false;
            this.btnTrack4.Click += new System.EventHandler(this.btnTrack4_Click_1);
            // 
            // btnTrack2
            // 
            this.btnTrack2.BackColor = System.Drawing.Color.LightGray;
            this.btnTrack2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTrack2.Location = new System.Drawing.Point(80, 70);
            this.btnTrack2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTrack2.Name = "btnTrack2";
            this.btnTrack2.Size = new System.Drawing.Size(35, 35);
            this.btnTrack2.TabIndex = 23;
            this.btnTrack2.Text = "▷";
            this.btnTrack2.UseVisualStyleBackColor = false;
            this.btnTrack2.Click += new System.EventHandler(this.btnTrack2_Click_1);
            // 
            // btnTrack3
            // 
            this.btnTrack3.BackColor = System.Drawing.Color.LightGray;
            this.btnTrack3.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTrack3.Location = new System.Drawing.Point(44, 113);
            this.btnTrack3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTrack3.Name = "btnTrack3";
            this.btnTrack3.Size = new System.Drawing.Size(35, 35);
            this.btnTrack3.TabIndex = 22;
            this.btnTrack3.Text = "▽";
            this.btnTrack3.UseVisualStyleBackColor = false;
            this.btnTrack3.Click += new System.EventHandler(this.btnTrack3_Click_1);
            // 
            // picTrack
            // 
            this.picTrack.ErrorImage = null;
            this.picTrack.Location = new System.Drawing.Point(155, 23);
            this.picTrack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picTrack.Name = "picTrack";
            this.picTrack.Size = new System.Drawing.Size(76, 75);
            this.picTrack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTrack.TabIndex = 11;
            this.picTrack.TabStop = false;
            // 
            // btnTrack1
            // 
            this.btnTrack1.BackColor = System.Drawing.Color.LightGray;
            this.btnTrack1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTrack1.ImageList = this.icon;
            this.btnTrack1.Location = new System.Drawing.Point(44, 27);
            this.btnTrack1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTrack1.Name = "btnTrack1";
            this.btnTrack1.Size = new System.Drawing.Size(35, 35);
            this.btnTrack1.TabIndex = 7;
            this.btnTrack1.Text = "△";
            this.btnTrack1.UseVisualStyleBackColor = false;
            this.btnTrack1.Click += new System.EventHandler(this.btnTrack1_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNext.Location = new System.Drawing.Point(802, 35);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 25);
            this.buttonNext.TabIndex = 22;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBoxScenarioName
            // 
            this.textBoxScenarioName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxScenarioName.Location = new System.Drawing.Point(656, 37);
            this.textBoxScenarioName.Name = "textBoxScenarioName";
            this.textBoxScenarioName.ReadOnly = true;
            this.textBoxScenarioName.Size = new System.Drawing.Size(132, 27);
            this.textBoxScenarioName.TabIndex = 21;
            // 
            // lv1button
            // 
            this.lv1button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lv1button.Location = new System.Drawing.Point(185, 25);
            this.lv1button.Name = "lv1button";
            this.lv1button.Size = new System.Drawing.Size(35, 35);
            this.lv1button.TabIndex = 1;
            this.lv1button.Text = "1";
            this.lv1button.UseVisualStyleBackColor = true;
            this.lv1button.Click += new System.EventHandler(this.Lv1button_Click);
            // 
            // lv2button
            // 
            this.lv2button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lv2button.Location = new System.Drawing.Point(239, 25);
            this.lv2button.Name = "lv2button";
            this.lv2button.Size = new System.Drawing.Size(35, 35);
            this.lv2button.TabIndex = 23;
            this.lv2button.Text = "2";
            this.lv2button.UseVisualStyleBackColor = true;
            this.lv2button.Click += new System.EventHandler(this.Lv2button_Click);
            // 
            // lv3button
            // 
            this.lv3button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lv3button.Location = new System.Drawing.Point(294, 25);
            this.lv3button.Name = "lv3button";
            this.lv3button.Size = new System.Drawing.Size(35, 35);
            this.lv3button.TabIndex = 24;
            this.lv3button.Text = "3";
            this.lv3button.UseVisualStyleBackColor = true;
            this.lv3button.Click += new System.EventHandler(this.Lv3button_Click);
            // 
            // lv4button
            // 
            this.lv4button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lv4button.Location = new System.Drawing.Point(348, 25);
            this.lv4button.Name = "lv4button";
            this.lv4button.Size = new System.Drawing.Size(36, 35);
            this.lv4button.TabIndex = 25;
            this.lv4button.Text = "4";
            this.lv4button.UseVisualStyleBackColor = true;
            this.lv4button.Click += new System.EventHandler(this.Lv4button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(948, 848);
            this.Controls.Add(this.lv4button);
            this.Controls.Add(this.lv3button);
            this.Controls.Add(this.lv2button);
            this.Controls.Add(this.lv1button);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.textBoxScenarioName);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.gBSettingField.ResumeLayout(false);
            this.gBSettingField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldWidth)).EndInit();
            this.gBSettingFire.ResumeLayout(false);
            this.gBSettingFire.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFire1)).EndInit();
            this.gBSettingFireTruck.ResumeLayout(false);
            this.gBSettingFireTruck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeForHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFailureProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfHelpFireTruck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfBrokenFireTruck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfNormalFireTruck)).EndInit();
            this.gBSettingWind.ResumeLayout(false);
            this.gBSettingWind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWindLevel)).EndInit();
            this.gBSettingTimer.ResumeLayout(false);
            this.gBSettingTimer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimersecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimerMinute)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWindDirection)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBoxElapsedTime;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTrack1;
        private System.Windows.Forms.ImageList icon;
        private System.Windows.Forms.PictureBox picTrack;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label numTruck;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem シナリオToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 練習ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gBSettingWind;
        private System.Windows.Forms.ComboBox cBWindDirection;
        private System.Windows.Forms.NumericUpDown nudWindLevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gBSettingTimer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudTimersecond;
        private System.Windows.Forms.NumericUpDown nudTimerMinute;
        private System.Windows.Forms.Label lbWindLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gBSettingField;
        private System.Windows.Forms.NumericUpDown nudCellSize;
        private System.Windows.Forms.NumericUpDown nudFieldLength;
        private System.Windows.Forms.NumericUpDown nudFieldWidth;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox gBSettingFire;
        private System.Windows.Forms.NumericUpDown nudNumOfFire5;
        private System.Windows.Forms.NumericUpDown nudNumOfFire4;
        private System.Windows.Forms.NumericUpDown nudNumOfFire3;
        private System.Windows.Forms.NumericUpDown nudNumOfFire2;
        private System.Windows.Forms.NumericUpDown nudNumOfFire1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox gBSettingFireTruck;
        private System.Windows.Forms.NumericUpDown nudTimeForHelp;
        private System.Windows.Forms.NumericUpDown nudFailureProbability;
        private System.Windows.Forms.NumericUpDown nudNumOfHelpFireTruck;
        private System.Windows.Forms.NumericUpDown nudNumOfBrokenFireTruck;
        private System.Windows.Forms.NumericUpDown nudNumOfNormalFireTruck;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTrack4;
        private System.Windows.Forms.Button btnTrack2;
        private System.Windows.Forms.Button btnTrack3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemScenario;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTraining1;
        private System.Windows.Forms.PictureBox picBoxWindDirection;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTraining2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExp1;
        private System.Windows.Forms.TextBox textBoxTimeLimit;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.TextBox textBoxScenarioName;
        private System.Windows.Forms.Button buttonWithdraw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button lv1button;
        private System.Windows.Forms.Button lv2button;
        private System.Windows.Forms.Button lv3button;
        private System.Windows.Forms.Button lv4button;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}


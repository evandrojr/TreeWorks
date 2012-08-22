namespace Spartacus {
    partial class FrmOption {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOption));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btConnect = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.cbSampleUsingTheSameSeed = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tbPresentValueCoefficient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTreeMinNumberOfCasesPerNode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTreeLevelsMax = new System.Windows.Forms.TextBox();
            this.lbLearningSampleValue = new System.Windows.Forms.Label();
            this.tbLearningSample = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tabClassification = new System.Windows.Forms.TabPage();
            this.tbClfMaxNumberOfValues = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabRegression = new System.Windows.Forms.TabPage();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.tbSampleSizeError = new System.Windows.Forms.TextBox();
            this.lbConfidenceInterval = new System.Windows.Forms.Label();
            this.tbSampleSizeCI = new System.Windows.Forms.TextBox();
            this.btClose = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.sldOptimisationLevelForCategoricalSearch = new System.Windows.Forms.TrackBar();
            this.label26 = new System.Windows.Forms.Label();
            this.tbOptimisationLevelForCategoricalSearch = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabDatabase.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLearningSample)).BeginInit();
            this.tabClassification.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldOptimisationLevelForCategoricalSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDatabase);
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabClassification);
            this.tabControl.Controls.Add(this.tabRegression);
            this.tabControl.Controls.Add(this.tabStatistics);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(410, 305);
            this.tabControl.TabIndex = 0;
            // 
            // tabDatabase
            // 
            this.tabDatabase.BackColor = System.Drawing.Color.Transparent;
            this.tabDatabase.Controls.Add(this.tbPort);
            this.tabDatabase.Controls.Add(this.label10);
            this.tabDatabase.Controls.Add(this.tbServer);
            this.tabDatabase.Controls.Add(this.label9);
            this.tabDatabase.Controls.Add(this.label8);
            this.tabDatabase.Controls.Add(this.btConnect);
            this.tabDatabase.Controls.Add(this.label7);
            this.tabDatabase.Controls.Add(this.tbDatabase);
            this.tabDatabase.Controls.Add(this.label6);
            this.tabDatabase.Controls.Add(this.tbUser);
            this.tabDatabase.Controls.Add(this.tbPassword);
            this.tabDatabase.Location = new System.Drawing.Point(4, 22);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatabase.Size = new System.Drawing.Size(402, 279);
            this.tabDatabase.TabIndex = 2;
            this.tabDatabase.Text = "PostgreSQL Database";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(192, 106);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(201, 20);
            this.tbPort.TabIndex = 4;
            this.tbPort.Validated += new System.EventHandler(this.tbPort_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Port";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(192, 81);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(201, 20);
            this.tbServer.TabIndex = 3;
            this.tbServer.Validated += new System.EventHandler(this.tbServer_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Server";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Database";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(309, 239);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 5;
            this.btConnect.Text = "Connect";
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Password";
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(192, 56);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(201, 20);
            this.tbDatabase.TabIndex = 2;
            this.tbDatabase.Validated += new System.EventHandler(this.tbDatabase_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "User";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(192, 6);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(201, 20);
            this.tbUser.TabIndex = 0;
            this.tbUser.Validated += new System.EventHandler(this.tbUser_Validated);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(192, 31);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '●';
            this.tbPassword.Size = new System.Drawing.Size(201, 20);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.Validated += new System.EventHandler(this.tbPassword_Validated);
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.Transparent;
            this.tabGeneral.Controls.Add(this.cbSampleUsingTheSameSeed);
            this.tabGeneral.Controls.Add(this.label25);
            this.tabGeneral.Controls.Add(this.label24);
            this.tabGeneral.Controls.Add(this.tbPresentValueCoefficient);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.tbTreeMinNumberOfCasesPerNode);
            this.tabGeneral.Controls.Add(this.label5);
            this.tabGeneral.Controls.Add(this.tbTreeLevelsMax);
            this.tabGeneral.Controls.Add(this.lbLearningSampleValue);
            this.tabGeneral.Controls.Add(this.tbLearningSample);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(402, 279);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // cbSampleUsingTheSameSeed
            // 
            this.cbSampleUsingTheSameSeed.AutoSize = true;
            this.cbSampleUsingTheSameSeed.Location = new System.Drawing.Point(312, 244);
            this.cbSampleUsingTheSameSeed.Name = "cbSampleUsingTheSameSeed";
            this.cbSampleUsingTheSameSeed.Size = new System.Drawing.Size(44, 17);
            this.cbSampleUsingTheSameSeed.TabIndex = 10;
            this.cbSampleUsingTheSameSeed.Text = "Yes";
            this.cbSampleUsingTheSameSeed.UseVisualStyleBackColor = true;
            this.cbSampleUsingTheSameSeed.CheckedChanged += new System.EventHandler(this.cbSampleUsingTheSameSeed_CheckedChanged);
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(5, 239);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(264, 37);
            this.label25.TabIndex = 9;
            this.label25.Text = "Reproduce the same training and validation sets when using the same data set and " +
                "same percentage for the sample";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 81);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(234, 13);
            this.label24.TabIndex = 8;
            this.label24.Text = "Value for coefficient of the presente values (MV)";
            // 
            // tbPresentValueCoefficient
            // 
            this.tbPresentValueCoefficient.Location = new System.Drawing.Point(308, 81);
            this.tbPresentValueCoefficient.Name = "tbPresentValueCoefficient";
            this.tbPresentValueCoefficient.Size = new System.Drawing.Size(86, 20);
            this.tbPresentValueCoefficient.TabIndex = 7;
            this.tbPresentValueCoefficient.Validated += new System.EventHandler(this.tbPresentValueCoefficient_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Minimum number of cases per node";
            // 
            // tbTreeMinNumberOfCasesPerNode
            // 
            this.tbTreeMinNumberOfCasesPerNode.Location = new System.Drawing.Point(308, 57);
            this.tbTreeMinNumberOfCasesPerNode.Name = "tbTreeMinNumberOfCasesPerNode";
            this.tbTreeMinNumberOfCasesPerNode.Size = new System.Drawing.Size(86, 20);
            this.tbTreeMinNumberOfCasesPerNode.TabIndex = 5;
            this.tbTreeMinNumberOfCasesPerNode.Validated += new System.EventHandler(this.tbTreeMinNumberOfCasesPerNode_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Maximun number of levels";
            // 
            // tbTreeLevelsMax
            // 
            this.tbTreeLevelsMax.Location = new System.Drawing.Point(308, 32);
            this.tbTreeLevelsMax.Name = "tbTreeLevelsMax";
            this.tbTreeLevelsMax.Size = new System.Drawing.Size(86, 20);
            this.tbTreeLevelsMax.TabIndex = 1;
            this.tbTreeLevelsMax.Validated += new System.EventHandler(this.tbTreeLevelsMax_Validated);
            // 
            // lbLearningSampleValue
            // 
            this.lbLearningSampleValue.AutoSize = true;
            this.lbLearningSampleValue.Location = new System.Drawing.Point(258, 8);
            this.lbLearningSampleValue.Name = "lbLearningSampleValue";
            this.lbLearningSampleValue.Size = new System.Drawing.Size(21, 13);
            this.lbLearningSampleValue.TabIndex = 2;
            this.lbLearningSampleValue.Text = "0%";
            // 
            // tbLearningSample
            // 
            this.tbLearningSample.Location = new System.Drawing.Point(299, 4);
            this.tbLearningSample.Maximum = 100;
            this.tbLearningSample.Minimum = 5;
            this.tbLearningSample.Name = "tbLearningSample";
            this.tbLearningSample.Size = new System.Drawing.Size(104, 45);
            this.tbLearningSample.TabIndex = 0;
            this.tbLearningSample.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLearningSample.Value = 5;
            this.tbLearningSample.Scroll += new System.EventHandler(this.tBLearningSample_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Default % of data used for the learning sample";
            // 
            // tabClassification
            // 
            this.tabClassification.BackColor = System.Drawing.Color.Transparent;
            this.tabClassification.Controls.Add(this.tbOptimisationLevelForCategoricalSearch);
            this.tabClassification.Controls.Add(this.label26);
            this.tabClassification.Controls.Add(this.sldOptimisationLevelForCategoricalSearch);
            this.tabClassification.Controls.Add(this.tbClfMaxNumberOfValues);
            this.tabClassification.Controls.Add(this.label4);
            this.tabClassification.Location = new System.Drawing.Point(4, 22);
            this.tabClassification.Name = "tabClassification";
            this.tabClassification.Padding = new System.Windows.Forms.Padding(3);
            this.tabClassification.Size = new System.Drawing.Size(402, 279);
            this.tabClassification.TabIndex = 1;
            this.tabClassification.Text = "Classification";
            // 
            // tbClfMaxNumberOfValues
            // 
            this.tbClfMaxNumberOfValues.Location = new System.Drawing.Point(308, 12);
            this.tbClfMaxNumberOfValues.Name = "tbClfMaxNumberOfValues";
            this.tbClfMaxNumberOfValues.Size = new System.Drawing.Size(86, 20);
            this.tbClfMaxNumberOfValues.TabIndex = 2;
            this.tbClfMaxNumberOfValues.Validated += new System.EventHandler(this.tbClfMaxNumberOfValues_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maximun number of values per variable for full search";
            // 
            // tabRegression
            // 
            this.tabRegression.BackColor = System.Drawing.Color.Transparent;
            this.tabRegression.Location = new System.Drawing.Point(4, 22);
            this.tabRegression.Name = "tabRegression";
            this.tabRegression.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegression.Size = new System.Drawing.Size(402, 279);
            this.tabRegression.TabIndex = 3;
            this.tabRegression.Text = "Regression";
            // 
            // tabStatistics
            // 
            this.tabStatistics.BackColor = System.Drawing.Color.Transparent;
            this.tabStatistics.Controls.Add(this.label2);
            this.tabStatistics.Controls.Add(this.lbError);
            this.tabStatistics.Controls.Add(this.tbSampleSizeError);
            this.tabStatistics.Controls.Add(this.lbConfidenceInterval);
            this.tabStatistics.Controls.Add(this.tbSampleSizeCI);
            this.tabStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(402, 279);
            this.tabStatistics.TabIndex = 4;
            this.tabStatistics.Text = "Statistics";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Minimum sample size estimation";
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Location = new System.Drawing.Point(4, 55);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(46, 13);
            this.lbError.TabIndex = 11;
            this.lbError.Text = "Error (%)";
            // 
            // tbSampleSizeError
            // 
            this.tbSampleSizeError.Location = new System.Drawing.Point(308, 59);
            this.tbSampleSizeError.Name = "tbSampleSizeError";
            this.tbSampleSizeError.Size = new System.Drawing.Size(86, 20);
            this.tbSampleSizeError.TabIndex = 10;
            this.tbSampleSizeError.Validated += new System.EventHandler(this.tbSampleSizeError_Validated);
            // 
            // lbConfidenceInterval
            // 
            this.lbConfidenceInterval.AutoSize = true;
            this.lbConfidenceInterval.Location = new System.Drawing.Point(4, 35);
            this.lbConfidenceInterval.Name = "lbConfidenceInterval";
            this.lbConfidenceInterval.Size = new System.Drawing.Size(116, 13);
            this.lbConfidenceInterval.TabIndex = 9;
            this.lbConfidenceInterval.Text = "Confidence Interval (%)";
            // 
            // tbSampleSizeCI
            // 
            this.tbSampleSizeCI.Location = new System.Drawing.Point(308, 34);
            this.tbSampleSizeCI.Name = "tbSampleSizeCI";
            this.tbSampleSizeCI.Size = new System.Drawing.Size(86, 20);
            this.tbSampleSizeCI.TabIndex = 8;
            this.tbSampleSizeCI.Validated += new System.EventHandler(this.tbSampleSizeCI_Validated);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(331, 311);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(308, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(174, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Minimum number of cases per node";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.textBox4);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.textBox5);
            this.tabPage1.Controls.Add(this.textBox6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(402, 279);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "PostgreSQL Database";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(192, 106);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(201, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Port";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(192, 81);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(201, 20);
            this.textBox3.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Server";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Database";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Connect";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Password";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(192, 56);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(201, 20);
            this.textBox4.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "User";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(192, 6);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(201, 20);
            this.textBox5.TabIndex = 0;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(192, 31);
            this.textBox6.Name = "textBox6";
            this.textBox6.PasswordChar = '●';
            this.textBox6.Size = new System.Drawing.Size(201, 20);
            this.textBox6.TabIndex = 1;
            this.textBox6.UseSystemPasswordChar = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.trackBar1);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(402, 279);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "General";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(129, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Maximun number of levels";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(308, 32);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(86, 20);
            this.textBox7.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(258, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(21, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "0%";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(299, 4);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 5;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 5;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(223, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Default % of data used for the learning sample";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.textBox8);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(402, 279);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Classification";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(308, 12);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(86, 20);
            this.textBox8.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(246, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "Maximun number of values per categorical variable";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(402, 279);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Regression";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Transparent;
            this.tabPage5.Controls.Add(this.label21);
            this.tabPage5.Controls.Add(this.label22);
            this.tabPage5.Controls.Add(this.textBox9);
            this.tabPage5.Controls.Add(this.label23);
            this.tabPage5.Controls.Add(this.textBox10);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(402, 279);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Statistics";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(118, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(155, 13);
            this.label21.TabIndex = 12;
            this.label21.Text = "Minimum sample size estimation";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(4, 55);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(46, 13);
            this.label22.TabIndex = 11;
            this.label22.Text = "Error (%)";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(308, 59);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(86, 20);
            this.textBox9.TabIndex = 10;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(4, 35);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(116, 13);
            this.label23.TabIndex = 9;
            this.label23.Text = "Confidence Interval (%)";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(308, 34);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(86, 20);
            this.textBox10.TabIndex = 8;
            // 
            // sldOptimisationLevelForCategoricalSearch
            // 
            this.sldOptimisationLevelForCategoricalSearch.Location = new System.Drawing.Point(220, 50);
            this.sldOptimisationLevelForCategoricalSearch.Maximum = 2;
            this.sldOptimisationLevelForCategoricalSearch.Name = "sldOptimisationLevelForCategoricalSearch";
            this.sldOptimisationLevelForCategoricalSearch.Size = new System.Drawing.Size(57, 45);
            this.sldOptimisationLevelForCategoricalSearch.TabIndex = 5;
            this.sldOptimisationLevelForCategoricalSearch.Scroll += new System.EventHandler(this.sldOptimisationLevelForCategoricalSearch_Scroll);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 50);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(194, 13);
            this.label26.TabIndex = 6;
            this.label26.Text = "Optimisation level for categorical search";
            // 
            // tbOptimisationLevelForCategoricalSearch
            // 
            this.tbOptimisationLevelForCategoricalSearch.Location = new System.Drawing.Point(308, 50);
            this.tbOptimisationLevelForCategoricalSearch.Name = "tbOptimisationLevelForCategoricalSearch";
            this.tbOptimisationLevelForCategoricalSearch.ReadOnly = true;
            this.tbOptimisationLevelForCategoricalSearch.Size = new System.Drawing.Size(86, 20);
            this.tbOptimisationLevelForCategoricalSearch.TabIndex = 7;
            // 
            // FrmOption
            // 
            this.AcceptButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(410, 346);
            this.ControlBox = false;
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmOption";
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOption_FormClosing);
            this.Load += new System.EventHandler(this.FrmOption_Load);
            this.tabControl.ResumeLayout(false);
            this.tabDatabase.ResumeLayout(false);
            this.tabDatabase.PerformLayout();
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLearningSample)).EndInit();
            this.tabClassification.ResumeLayout(false);
            this.tabClassification.PerformLayout();
            this.tabStatistics.ResumeLayout(false);
            this.tabStatistics.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldOptimisationLevelForCategoricalSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabClassification;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TrackBar tbLearningSample;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbLearningSampleValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbClfMaxNumberOfValues;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTreeLevelsMax;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabRegression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTreeMinNumberOfCasesPerNode;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.TextBox tbSampleSizeError;
        private System.Windows.Forms.Label lbConfidenceInterval;
        private System.Windows.Forms.TextBox tbSampleSizeCI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbPresentValueCoefficient;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.CheckBox cbSampleUsingTheSameSeed;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TrackBar sldOptimisationLevelForCategoricalSearch;
        private System.Windows.Forms.TextBox tbOptimisationLevelForCategoricalSearch;
    }
}
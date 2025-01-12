namespace UniversityAcademicManagementSystem
{
    partial class FormTeacher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.课程信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改课程信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.授课信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.学生信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加学生ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改学生信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除学生ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.成绩管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置课程成绩ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反馈到管理员ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_Tno = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_IsLeader = new System.Windows.Forms.Label();
            this.label_COname = new System.Windows.Forms.Label();
            this.label_Age = new System.Windows.Forms.Label();
            this.label_Sex = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.课程信息管理ToolStripMenuItem,
            this.学生信息管理ToolStripMenuItem,
            this.成绩管理ToolStripMenuItem,
            this.反馈到管理员ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(942, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 课程信息管理ToolStripMenuItem
            // 
            this.课程信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加课程ToolStripMenuItem,
            this.修改课程信息ToolStripMenuItem,
            this.删除课程ToolStripMenuItem,
            this.授课信息ToolStripMenuItem});
            this.课程信息管理ToolStripMenuItem.Name = "课程信息管理ToolStripMenuItem";
            this.课程信息管理ToolStripMenuItem.Size = new System.Drawing.Size(156, 34);
            this.课程信息管理ToolStripMenuItem.Text = "课程信息管理";
            // 
            // 添加课程ToolStripMenuItem
            // 
            this.添加课程ToolStripMenuItem.Name = "添加课程ToolStripMenuItem";
            this.添加课程ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.添加课程ToolStripMenuItem.Text = "添加课程";
            this.添加课程ToolStripMenuItem.Click += new System.EventHandler(this.添加课程ToolStripMenuItem_Click);
            // 
            // 修改课程信息ToolStripMenuItem
            // 
            this.修改课程信息ToolStripMenuItem.Name = "修改课程信息ToolStripMenuItem";
            this.修改课程信息ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.修改课程信息ToolStripMenuItem.Text = "修改课程信息";
            this.修改课程信息ToolStripMenuItem.Click += new System.EventHandler(this.修改课程信息ToolStripMenuItem_Click);
            // 
            // 删除课程ToolStripMenuItem
            // 
            this.删除课程ToolStripMenuItem.Name = "删除课程ToolStripMenuItem";
            this.删除课程ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.删除课程ToolStripMenuItem.Text = "删除课程";
            this.删除课程ToolStripMenuItem.Click += new System.EventHandler(this.删除课程ToolStripMenuItem_Click);
            // 
            // 授课信息ToolStripMenuItem
            // 
            this.授课信息ToolStripMenuItem.Name = "授课信息ToolStripMenuItem";
            this.授课信息ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.授课信息ToolStripMenuItem.Text = "授课信息";
            this.授课信息ToolStripMenuItem.Click += new System.EventHandler(this.授课信息ToolStripMenuItem_Click);
            // 
            // 学生信息管理ToolStripMenuItem
            // 
            this.学生信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加学生ToolStripMenuItem,
            this.修改学生信息ToolStripMenuItem,
            this.删除学生ToolStripMenuItem});
            this.学生信息管理ToolStripMenuItem.Name = "学生信息管理ToolStripMenuItem";
            this.学生信息管理ToolStripMenuItem.Size = new System.Drawing.Size(156, 34);
            this.学生信息管理ToolStripMenuItem.Text = "学生信息管理";
            // 
            // 添加学生ToolStripMenuItem
            // 
            this.添加学生ToolStripMenuItem.Name = "添加学生ToolStripMenuItem";
            this.添加学生ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.添加学生ToolStripMenuItem.Text = "添加学生";
            this.添加学生ToolStripMenuItem.Click += new System.EventHandler(this.添加学生ToolStripMenuItem_Click);
            // 
            // 修改学生信息ToolStripMenuItem
            // 
            this.修改学生信息ToolStripMenuItem.Name = "修改学生信息ToolStripMenuItem";
            this.修改学生信息ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.修改学生信息ToolStripMenuItem.Text = "修改学生信息";
            this.修改学生信息ToolStripMenuItem.Click += new System.EventHandler(this.修改学生信息ToolStripMenuItem_Click);
            // 
            // 删除学生ToolStripMenuItem
            // 
            this.删除学生ToolStripMenuItem.Name = "删除学生ToolStripMenuItem";
            this.删除学生ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.删除学生ToolStripMenuItem.Text = "删除学生";
            this.删除学生ToolStripMenuItem.Click += new System.EventHandler(this.删除学生ToolStripMenuItem_Click);
            // 
            // 成绩管理ToolStripMenuItem
            // 
            this.成绩管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置课程成绩ToolStripMenuItem});
            this.成绩管理ToolStripMenuItem.Name = "成绩管理ToolStripMenuItem";
            this.成绩管理ToolStripMenuItem.Size = new System.Drawing.Size(114, 34);
            this.成绩管理ToolStripMenuItem.Text = "成绩管理";
            // 
            // 设置课程成绩ToolStripMenuItem
            // 
            this.设置课程成绩ToolStripMenuItem.Name = "设置课程成绩ToolStripMenuItem";
            this.设置课程成绩ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.设置课程成绩ToolStripMenuItem.Text = "设置课程成绩";
            this.设置课程成绩ToolStripMenuItem.Click += new System.EventHandler(this.设置课程成绩ToolStripMenuItem_Click);
            // 
            // 反馈到管理员ToolStripMenuItem
            // 
            this.反馈到管理员ToolStripMenuItem.Name = "反馈到管理员ToolStripMenuItem";
            this.反馈到管理员ToolStripMenuItem.Size = new System.Drawing.Size(156, 34);
            this.反馈到管理员ToolStripMenuItem.Text = "反馈到管理员";
            this.反馈到管理员ToolStripMenuItem.Click += new System.EventHandler(this.反馈到管理员ToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(829, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 51);
            this.button1.TabIndex = 1;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "教师";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button2.Location = new System.Drawing.Point(687, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 41);
            this.button2.TabIndex = 3;
            this.button2.Text = "添加专业";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.DodgerBlue;
            this.dataGridView1.Location = new System.Drawing.Point(17, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(622, 333);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "教师";
            this.Column1.MinimumWidth = 9;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "课程名";
            this.Column3.MinimumWidth = 9;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "学时";
            this.Column4.MinimumWidth = 9;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "课程简介";
            this.Column5.MinimumWidth = 9;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "选课人数";
            this.Column6.MinimumWidth = 9;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // label_Tno
            // 
            this.label_Tno.Location = new System.Drawing.Point(0, 47);
            this.label_Tno.Name = "label_Tno";
            this.label_Tno.Size = new System.Drawing.Size(291, 26);
            this.label_Tno.TabIndex = 5;
            this.label_Tno.Text = "2222106013001";
            this.label_Tno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label_IsLeader);
            this.groupBox1.Controls.Add(this.label_COname);
            this.groupBox1.Controls.Add(this.label_Age);
            this.groupBox1.Controls.Add(this.label_Sex);
            this.groupBox1.Controls.Add(this.label_Name);
            this.groupBox1.Controls.Add(this.label_Password);
            this.groupBox1.Controls.Add(this.label_ID);
            this.groupBox1.Controls.Add(this.label_Tno);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(645, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 408);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "教师信息";
            // 
            // label_IsLeader
            // 
            this.label_IsLeader.Location = new System.Drawing.Point(0, 343);
            this.label_IsLeader.Name = "label_IsLeader";
            this.label_IsLeader.Size = new System.Drawing.Size(291, 21);
            this.label_IsLeader.TabIndex = 19;
            this.label_IsLeader.Text = "否";
            this.label_IsLeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_COname
            // 
            this.label_COname.Location = new System.Drawing.Point(0, 308);
            this.label_COname.Name = "label_COname";
            this.label_COname.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_COname.Size = new System.Drawing.Size(297, 23);
            this.label_COname.TabIndex = 17;
            this.label_COname.Text = "生命科学与技术学院";
            this.label_COname.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_COname.UseCompatibleTextRendering = true;
            // 
            // label_Age
            // 
            this.label_Age.Location = new System.Drawing.Point(0, 258);
            this.label_Age.Name = "label_Age";
            this.label_Age.Size = new System.Drawing.Size(291, 21);
            this.label_Age.TabIndex = 15;
            this.label_Age.Text = "28";
            this.label_Age.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Sex
            // 
            this.label_Sex.Location = new System.Drawing.Point(0, 220);
            this.label_Sex.Name = "label_Sex";
            this.label_Sex.Size = new System.Drawing.Size(291, 21);
            this.label_Sex.TabIndex = 13;
            this.label_Sex.Text = "女";
            this.label_Sex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Name
            // 
            this.label_Name.Location = new System.Drawing.Point(0, 180);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(291, 21);
            this.label_Name.TabIndex = 11;
            this.label_Name.Text = "李玉";
            this.label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Password
            // 
            this.label_Password.Location = new System.Drawing.Point(0, 123);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(292, 21);
            this.label_Password.TabIndex = 9;
            this.label_Password.Text = "3001001";
            this.label_Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ID
            // 
            this.label_ID.Location = new System.Drawing.Point(0, 87);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(291, 21);
            this.label_ID.TabIndex = 7;
            this.label_ID.Text = "16";
            this.label_ID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormTeacher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UniversityAcademicManagementSystem.Properties.Resources._4;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(942, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormTeacher";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "教师";
            this.Load += new System.EventHandler(this.FormTeacher_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 课程信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改课程信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 学生信息管理ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 添加学生ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改学生信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除学生ToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem 授课信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反馈到管理员ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Label label_Tno;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label_Age;
        private System.Windows.Forms.Label label_Sex;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_IsLeader;
        private System.Windows.Forms.Label label_COname;
        private System.Windows.Forms.ToolStripMenuItem 成绩管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置课程成绩ToolStripMenuItem;
    }
}
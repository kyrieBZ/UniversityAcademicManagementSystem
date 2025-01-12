using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace UniversityAcademicManagementSystem
{
    public partial class FormStudent : Form
    {
        public FormStudent()
        {
            InitializeComponent();

            //提示学生是否达到毕业要求
            double creditRequest = FormAdministrator.getCreditRequest();//毕业学分要求
            double creditSum = getStudentCredit(Form1.Sno);//当前学生已修学分

            //Console.WriteLine("creditRequest:" + creditRequest);
            //Console.WriteLine("creditSum:" + creditSum);

            double GPARequest = FormAdministrator.getGPA_Request(); // 获得学生的GPA要求
            double GPA = getStudentGPA(Form1.Sno); // 获得学生的GPA

            //Console.WriteLine("GPA:" + GPA);

            label_Credit.Text = "当前学分：" + creditSum.ToString();
            label_GPA.Text = "当前GPA：" + GPA.ToString("F3");

            label_CreditRequest.Text = "学分要求：" + creditRequest.ToString();
            label_GPARequest.Text = "GPA要求：" + GPARequest.ToString();

            tableLayoutPanel1.Visible = false; // 隐藏表格

        }

        //获取学生GPA
        private double getStudentGPA(string Sno)
        {
            // 获取学生学号
            string studentId = Form1.Sno;
            double gpa = 0.0;

            if (string.IsNullOrEmpty(studentId))
            {
                MessageBox.Show("学号不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            // 查询学生的课程成绩和学分
            string sql = @"
                SELECT 
                    course.Credit, 
                    sc.grade 
                FROM 
                    sc 
                INNER JOIN 
                    course ON sc.Cno = course.Cno 
                WHERE 
                    sc.Sno = @Sno AND
                    sc.grade IS NOT NULL";

            Dao dao = new Dao(); // 创建 Dao 对象
            try
            {
                dao.Connection(); // 打开数据库连接

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@Sno", studentId); // 设置参数

                // 使用 MySqlDataReader 获取查询结果
                MySqlDataReader reader = command.ExecuteReader();

                double totalCredits = 0; // 总学分
                double totalGradePoints = 0; // 总绩点

                while (reader.Read()) // 逐行读取数据
                {
                    double credit = Convert.ToDouble(reader["Credit"]); // 课程学分
                    double score = Convert.ToDouble(reader["grade"]); // 课程成绩

                    // 计算绩点（假设成绩满分为 100 分，绩点 = 成绩 / 10 - 5）
                    double gradePoint = score / 10 - 5;
                    if (gradePoint < 1) gradePoint = 0; // 绩点最低为 0

                    totalCredits += credit; // 累加总学分
                    totalGradePoints += gradePoint * credit; // 累加总绩点
                }

                reader.Close(); // 关闭读取器

                if (totalCredits == 0)
                {
                    MessageBox.Show("未找到该学生的课程信息！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                // 计算平均学分绩点
                gpa = totalGradePoints / totalCredits;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose(); // 确保关闭连接
            }

            return gpa;
        }

        //获取总学分
        private double getStudentCredit(string Sno)
        {
            double totalCredits = 0.0;

            Dao dao = new Dao();
            try
            {
                // 建立数据库连接
                dao.Connection();

                // SQL 查询语句，连接 sc 和 course 表，计算学分的总和
                string sql = $@"
                SELECT SUM(c.Credit) AS TotalCredits
                FROM sc s
                JOIN course c ON s.Cno = c.Cno
                WHERE s.Sno = @Sno AND 
                      s.Grade >= 60 AND 
                      s.Grade IS NOT NULL"; // 增加条件：成绩必须大于等于60且不为空

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@Sno", Sno); // 参数化查询，防止 SQL 注入

                    // 使用 MySqlDataReader 读取学分总和
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // 读取数据
                        {
                            // 获取学分总和
                            totalCredits = reader.IsDBNull(0) ? 0 : reader.GetDouble("TotalCredits");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose(); // 确保关闭连接
            }

            return totalCredits; // 返回学分总和
        }



        //窗口加载时欢迎学生登录
        private void FormStudent_Load(object sender, EventArgs e)
        {
            label1.Text = "欢迎学生：" + Form1.name + " 登录教务系统！";
            // 设置列属性
            listView1.Columns.Add("课程名", 100);
            listView1.Columns.Add("教师名", 100);
            listView1.Columns.Add("成绩", 80);
            listView1.Columns.Add("GPA", 80);
            listView1.Columns.Add("成绩状态", 80);

            listView1.View = View.Details; // 设置视图为详细模式

            LoadCourseInfo();
            LoadBasicInfo();

            listView1.Visible = false;
        }

        //加载学生基本信息
        private void LoadBasicInfo()
        {
            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT Sno, Sname, ID, PassWord, Sex, Sage FROM student WHERE Sno = @studentId";

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@studentId", Form1.Sno); // 使用当前学生的学号作为参数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // 确保读取到数据
                        {
                            // 设置标签文本
                            label_Sno.Text = "学号：" + reader.GetString("Sno");
                            label_Name.Text = "姓名：" + reader.GetString("Sname");
                            label_ID.Text = "账号：" + reader.GetInt32("ID").ToString();
                            label_Password.Text = "密码：" + reader.GetString("PassWord");
                            label_Sex.Text = "性别：" + reader.GetString("Sex");
                            label_Age.Text = "年龄：" + reader.GetInt32("Sage").ToString();
                        }
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //加载学生选课课程信息
        private void LoadCourseInfo()
        {
            // 清空现有项
            listView1.Items.Clear();

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询视图
                string sql = "SELECT CourseName, TeacherName, Grade, GPA, Status FROM StudentCourseInfo WHERE Sno = @studentId AND Grade IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@studentId", Form1.Sno); // 使用学生学号作为参数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 创建 ListViewItem
                            ListViewItem item = new ListViewItem(reader.GetString("CourseName"));
                            item.SubItems.Add(reader.GetString("TeacherName"));
                            item.SubItems.Add(reader.GetInt32("Grade").ToString());
                            item.SubItems.Add(reader.GetDouble("GPA").ToString("F1")); // 保留一位小数
                            item.SubItems.Add(reader.GetString("Status"));

                            // 添加到 ListView
                            listView1.Items.Add(item);
                        }
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //将系统使用感受反馈给管理员
        private void 反馈到管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建一个新的窗口
            Form feedbackForm = new Form();
            feedbackForm.Text = "反馈信息";
            feedbackForm.Width = 400;
            feedbackForm.Height = 300;
            feedbackForm.StartPosition = FormStartPosition.CenterScreen; // 设置显示位置为屏幕中心


            // 创建标签
            Label label = new Label();
            label.Text = "请输入您的反馈:";
            label.Top = 20;
            label.Left = 20;

            // 创建文本框
            TextBox textBox = new TextBox();
            textBox.Multiline = true; // 允许多行输入
            textBox.Top = 50;
            textBox.Left = 20;
            textBox.Width = 350;
            textBox.Height = 150;

            // 创建确认按钮
            Button submitButton = new Button();
            submitButton.Text = "提交";
            submitButton.Top = 220;
            submitButton.Left = 20;
            submitButton.Click += (s, args) => {
                string feedback = textBox.Text;
                MessageBox.Show("您提交的反馈是: " + feedback, "反馈确认", MessageBoxButtons.OK, MessageBoxIcon.Information);
                feedbackForm.Close(); // 关闭反馈窗口
            };

            // 将控件添加到反馈窗口
            feedbackForm.Controls.Add(label);
            feedbackForm.Controls.Add(textBox);
            feedbackForm.Controls.Add(submitButton);

            // 以对话框的形式打开反馈窗口
            feedbackForm.ShowDialog();

            if (textBox.Text == "")
            {
                MessageBox.Show("反馈信息不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int max_id = 0;
            string UserType = "学生";

            string sqlMaxid = "SELECT MAX(FID) FROM feedback";
            Dao dao = new Dao();
            dao.Connection();
            MySqlDataReader reader = dao.read(sqlMaxid);

            if (reader.Read()) // 读取查询结果
            {
                max_id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // 若无记录则 max_id 为 0
            }
            reader.Close(); // 关闭读取器

            string sql = $"INSERT INTO feedback (FID, UID, Uname, Fdate, FInfo, Ftype) " +
              $"values({max_id + 1}, @UserId, @UserName, @FeedbackDate, @FeedbackInfo,'{UserType}')";


            dao.AddParameter("@UserId", Form1.id);
            dao.AddParameter("@UserName", Form1.name);
            dao.AddParameter("@FeedbackDate", DateTime.Now);
            dao.AddParameter("@FeedbackInfo", textBox.Text);

            if (dao.Excute(sql) > 0)
            {
                MessageBox.Show("反馈成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("反馈失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dao.DaoClose();
        }

        //退出
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 学生所在学院
        private void 学院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取学生的学号
            string studentSno = Form1.Sno;

            if (!string.IsNullOrEmpty(studentSno))
            {
                // SQL 查询
                string sql = @"
                   SELECT college.COname 
                        FROM student 
                        INNER JOIN major ON student.Mno = major.Mno
                        INNER JOIN college ON major.Mcono = college.COno 
                        WHERE student.Sno = @StudentSno";

                // 创建数据库连接
                Dao dao = new Dao();
                try
                {
                    dao.Connection();

                    // 创建命令对象
                    MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                    command.Parameters.AddWithValue("@StudentSno", studentSno);

                    // 使用 MySqlDataReader 获取查询结果
                    string collegeName = string.Empty;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // 读取学院名称
                        {
                            collegeName = reader["COname"].ToString();
                        }
                    }

                    // 检查结果并显示
                    if (!string.IsNullOrEmpty(collegeName))
                    {
                        MessageBox.Show($"{Form1.name} 同学所在学院: {collegeName}", "学院", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("未找到您的学院信息。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dao.DaoClose(); // 确保关闭连接
                }
            }
            else
            {
                MessageBox.Show("学号无效或未输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //学生所学专业
        private void 专业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取学生的学号
            string studentSno = Form1.Sno;

            if (!string.IsNullOrEmpty(studentSno))
            {
                // SQL 查询
                string sql = @"
                   SELECT major.Mname 
                        FROM student 
                        INNER JOIN major ON student.Mno = major.Mno
                        WHERE student.Sno = @StudentSno";

                // 创建数据库连接
                Dao dao = new Dao();
                try
                {
                    dao.Connection();

                    // 创建命令对象
                    MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                    command.Parameters.AddWithValue("@StudentSno", studentSno);

                    // 使用 MySqlDataReader 获取查询结果
                    string majorName = string.Empty;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // 读取专业名称
                        {
                            majorName = reader["Mname"].ToString();
                        }
                    }

                    // 检查结果并显示
                    if (!string.IsNullOrEmpty(majorName))
                    {
                        MessageBox.Show($"{Form1.name} 同学所学专业: {majorName}", "专业", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("未找到该您的专业信息。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dao.DaoClose(); // 确保关闭连接
                }
            }
            else
            {
                MessageBox.Show("学号无效或未输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //学生学籍信息
        private void 学籍信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取学生的学号
            string studentSno = Form1.Sno;

            if (!string.IsNullOrEmpty(studentSno))
            {
                // SQL 查询
                string sql = @"
                   SELECT * 
                        FROM student 
                        INNER JOIN major ON student.Mno = major.Mno
                        INNER JOIN college ON major.Mcono = college.COno
                        WHERE student.Sno = @StudentSno";

                // 创建数据库连接
                Dao dao = new Dao();
                try
                {
                    dao.Connection();

                    // 创建命令对象
                    MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                    command.Parameters.AddWithValue("@StudentSno", studentSno);

                    // 使用 MySqlDataReader 获取查询结果
                    string majorName = string.Empty;
                    string collegeName = string.Empty;
                    string sex = string.Empty;
                    int age = -1;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // 读取专业名称
                        {
                            collegeName = reader["COname"].ToString();
                            majorName = reader["Mname"].ToString();
                            sex = reader["Sex"].ToString();
                            age = int.Parse(reader["Sage"].ToString());
                        }
                    }

                    // 检查结果并显示
                    if (!string.IsNullOrEmpty(majorName))
                    {
                        string message = $"学号: {studentSno}\n" +
                                  $"姓名：{Form1.name}\n" +
                                  $"性别: {sex}\n" +
                                  $"年龄: {age}\n" +
                                  $"专业: {majorName}\n" +
                                  $"学院: {collegeName}\n";

                        MessageBox.Show(message, "学籍信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("未找到该您的学籍信息。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dao.DaoClose(); // 确保关闭连接
                }
            }
            else
            {
                MessageBox.Show("学号无效或未输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        // 查看所有选修的课程的成绩
        private void 查看选修课程成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Visible = true;

            FormGrade formGrade = new FormGrade();
            formGrade.Show();
        }

        // 平均学分绩点
        private void 平均学分绩点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string studentId = Form1.Sno;
            double gpa = getStudentGPA(studentId);

            // 显示结果
            MessageBox.Show($"学生学号: {studentId}\n平均学分绩点: {gpa:F2}", "平均学分绩点", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //选课/退课功能
        private void 选课退课ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelect_QuitCourse formSelect_QuitCourse = new FormSelect_QuitCourse();
            formSelect_QuitCourse.Show();
        }

        //提示
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提示：学生可以使用的功能一共有5种" +
                "（选课/退课，学生成绩，学生信息，学院专业，反馈）" +
                "学生成绩可以查询单科选修课程成绩以及平均学分绩点。查看选修课程成绩时界面左上角也会显示成绩。" +
                "学生信息可以查看自己的学籍信息" +
                "学院专业可以查看自己的所在学院和专业" +
                "反馈可以向管理员反馈系统使用问题！" +
                "注：点击“学院和专业”在界面右下角显示学院和专业信息。点击学院简介，专业简介可以查看具体的简介信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        //毕业要求
        private void label2_Click(object sender, EventArgs e)
        {
            double creditRequest = FormAdministrator.getCreditRequest();//毕业学分要求
            double creditSum = getStudentCredit(Form1.Sno);//当前学生已修学分
            if (creditSum >= creditRequest)
            {
                MessageBox.Show($"恭喜您，您所修课程学分 <{creditSum}> 已达到毕业要求！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("您当前课程的学分总和为：" + creditSum + "，还差 " + (creditRequest - creditSum) + " 学分就可毕业！继续加油哦！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //当前已修
        private void label3_Click(object sender, EventArgs e)
        {
            double GPARequest = FormAdministrator.getGPA_Request(); // 获得学生的GPA要求
            double GPA = getStudentGPA(Form1.Sno); // 获得学生的GPA
            if (GPA >= GPARequest)
            {
                MessageBox.Show($"恭喜您，您的GPA  <{GPA}> 已达到毕业要求！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("您当前的GPA为：" + GPA + "，还差 " + (GPARequest - GPA) + " 才能毕业！继续努力！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //点击学院和专业后显示学院信息和专业信息
        private void 学院和专业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            getCollege_MajorInfo(Form1.Sno);
        }

        private string collegeName;
        private string leaderName;
        private string collegeIntroduce;
        private string majorName;
        private int majorFee;
        private string majorIntroduce;


        //获取学院信息，专业信息
        public void getCollege_MajorInfo(string studentId)
        {
            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询语句
                string sql = @"
                    SELECT 
                        c.COname, 
                        c.COleader_name, 
                        c.COintroduce, 
                        m.Mname, 
                        m.Mfee, 
                        m.Mintroduce 
                    FROM 
                        student s
                    JOIN 
                        major m ON s.Mno = m.Mno
                    JOIN 
                        college c ON m.Mcono = c.COno
                    WHERE 
                        s.Sno = @studentId"; // 使用参数化查询

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@studentId", studentId); // 添加参数

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // 读取查询结果
                        {
                            collegeName = reader.GetString("COname");
                            leaderName = reader.GetString("COleader_name");
                            collegeIntroduce = reader.GetString("COintroduce");
                            majorName = reader.GetString("Mname");
                            majorFee = reader.GetInt32("Mfee");
                            majorIntroduce = reader.GetString("Mintroduce");

                            label_COname.Text = "学院："+collegeName;
                            label_COleader.Text = "负责人："+leaderName;
                            

                            label_Mname.Text ="专业："+ majorName;
                            label_Mfee.Text = "专业学费："+majorFee.ToString();

                            label_COintroduce.Text ="学院简介："+ TruncateTextWithEllipsis(label_COintroduce, collegeIntroduce);
                            label_Mintroduce.Text = "专业简介："+TruncateTextWithEllipsis(label_Mintroduce, majorIntroduce);


                        }
                        else
                        {
                            MessageBox.Show("未找到相关信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 定义一个函数，根据 Label 控件的宽度截断文本并添加省略号
        string TruncateTextWithEllipsis(Label label, string text)
        {
            // 获取 Label 控件的宽度
            int labelWidth = label.Width;

            // 获取 Label 控件的字体
            Font labelFont = label.Font;

            // 使用 Graphics 对象测量文本的宽度
            using (Graphics g = label.CreateGraphics())
            {
                // 计算文本的宽度
                SizeF textSize = g.MeasureString(text, labelFont);

                // 如果文本宽度小于 Label 宽度，直接返回原文本
                if (textSize.Width <= labelWidth)
                {
                    return text;
                }

                // 如果文本宽度超过 Label 宽度，逐步截断文本并添加省略号
                string ellipsis = "...";
                int maxLength = text.Length;
                int minLength = 0;

                // 使用二分查找法找到合适的截断长度
                while (maxLength - minLength > 1)
                {
                    int midLength = (maxLength + minLength) / 2;
                    string truncatedText = text.Substring(0, midLength) + ellipsis;
                    SizeF truncatedSize = g.MeasureString(truncatedText, labelFont);

                    if (truncatedSize.Width <= labelWidth)
                    {
                        minLength = midLength;
                    }
                    else
                    {
                        maxLength = midLength;
                    }
                }

                // 返回截断后的文本
                return text.Substring(0, minLength) + ellipsis;
            }
        }

        //弹窗显示学院简介
        private void label_COintroduce_Click(object sender, EventArgs e)
        {
            MessageBox.Show(collegeIntroduce, "学院简介", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //弹窗显示专业简介
        private void label_Mintroduce_Click(object sender, EventArgs e)
        {
            MessageBox.Show(majorIntroduce, "专业简介", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

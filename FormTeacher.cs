using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using MySql.Data.MySqlClient;

namespace UniversityAcademicManagementSystem
{
    public partial class FormTeacher : Form
    {
        public FormTeacher()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void 添加学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddStudent form= new FormAddStudent();
            form.ShowDialog();
        }

        public static string operation;

        //添加课程
        private void 添加课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.Teacher == "CollegeLeader")
            {//该教师为学院负责人
                operation = "添加";
                FormCourse form = new FormCourse();
                form.ShowDialog();
            }
            else
            {//该教师为普通教师
                MessageBox.Show("您没有添加课程的权限！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {//添加专业
            if (Form1.Teacher == "CollegeLeader")
            {//该教师是学院负责人
                MessageBox.Show("注意：机械工程学院设置了机械设计制造及其自动化，机械电子工程以及过程装备与控制工程三个专业，计算机学院设置了计算机科学与技术，信息安全，物联网工程，人工智能，软件工程五个专业，生命科学与技术学院设置了生物技术，生物科学两个专业，理学院设置了数学与应用数学，物理学两个专业。请各个学院的负责人按自身学院设置的专业进行添加！","消息",MessageBoxButtons.OK,MessageBoxIcon.Information);
                FormAddMajor form= new FormAddMajor();
                form.ShowDialog();
            }
            else
            {//该教师为普通教师
                MessageBox.Show("您没有添加专业的权限！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 删除课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Form1.Teacher == "CollegeLeader")
            {//该教师为学院负责人
                operation = "删除";
                FormCourse form = new FormCourse();
                form.ShowDialog();
            }
            else
            {//该教师为普通教师
                MessageBox.Show("您没有删除课程的权限！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //将对系统的看法反馈到管理员
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
            string UserType = "教师";

            string sqlMaxid="SELECT MAX(FID) FROM feedback";
            Dao dao = new Dao();
            dao.Connection();
            MySqlDataReader reader = dao.read(sqlMaxid);

            if (reader.Read()) // 读取查询结果
            {
                max_id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // 若无记录则 max_id 为 0
            }
            reader.Close(); // 关闭读取器

            string sql = $"INSERT INTO feedback (FID, UID, Uname, Fdate, FInfo, Ftype) " +
              $"values({max_id+1}, @UserId, @UserName, @FeedbackDate, @FeedbackInfo,'{UserType}')";

            
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

        //获取教师所属学院
        private string getCollege(string tno)
        {
            string academyName = string.Empty; // 定义用于存储学院名称的变量
            Dao dao = new Dao();
            try
            {

                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT c.COname FROM college c " +
                                "JOIN teacher t ON c.COno = t.COno " +
                                "WHERE t.Tno = @TeacherID";

                using (MySqlCommand command = new MySqlCommand(sql, dao.Connection()))
                {
                    command.Parameters.AddWithValue("@TeacherID", tno); // 设置参数

                    // 执行查询
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) // 如果有结果
                    {
                        academyName = reader["COname"].ToString(); // 获取学院名称并赋值
                    }
                    reader.Close();
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

            return academyName;
        }

        //窗体加载时欢迎教师登录
        private void FormTeacher_Load(object sender, EventArgs e)
        {
            label1.Text = "欢迎教师：" + Form1.name + " 登录教务系统！";

            //显示教师基本信息
            string tno = Form1.Tno;
            string college = getCollege(tno);

            string sql = @"
                SELECT Tno, ID, PassWord,Tname, Tsex, Tage, COno, IsLeader 
                FROM teacher 
                WHERE Tno = @Tno"; // 使用参数化查询

            Dao dao = new Dao(); // 创建 Dao 对象
            try
            {
                dao.Connection(); // 打开数据库连接

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@Tno", tno); // 设置参数

                // 使用 MySqlDataReader 获取查询结果
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // 获取教师信息
                    string tno_test = reader["Tno"].ToString();
                    string id = reader["ID"].ToString();
                    string password = reader["PassWord"].ToString();
                    string name = reader["Tname"].ToString();
                    string sex = reader["Tsex"].ToString();
                    string age = reader["Tage"].ToString();
                    bool isLeader = Convert.ToBoolean(reader["IsLeader"]); // 转换为布尔值

                    // 显示教师信息
                    MessageBox.Show($"教师编号: {tno}\n" +
                                    $"ID: {id}\n" +
                                    $"姓名: {name}\n" +
                                    $"性别: {sex}\n" +
                                    $"年龄: {age}\n" +
                                    $"学院: {college}\n" +
                                    $"是否为学院负责人: {(isLeader ? "是" : "否")}",
                                    "教师信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //设置教师信息到界面
                    label_Tno.Text = "教师编号："+tno_test;
                    label_ID.Text = "账号："+id;
                    label_Password.Text = "密码："+password;
                    label_Name.Text = "姓名："+name;
                    label_Sex.Text = "性别："+sex;
                    label_Age.Text = "年龄："+age;
                    label_COname.Text = "学院："+college;
                    label_IsLeader.Text = "学院负责人："+$"{(isLeader ? "是" : "否")}";
                }
                else
                {
                    MessageBox.Show("未找到该教师的信息。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                reader.Close(); // 关闭读取器
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

        //查询对应教师的教师编号
        private string GetTeacherTnoById(string teacherName)
        {
            string tno = string.Empty; // 用于接收查询结果

            // 创建数据库连接
            Dao dao = new Dao();
            try
            {
                dao.Connection();

                // 使用参数化查询
                string sql = "SELECT Tno FROM teacher WHERE Tname = @name";

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@name", teacherName);

                // 执行查询并获取结果
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) // 检查是否有结果
                    {
                        tno = reader["Tno"].ToString(); // 获取 Tno 并赋值
                    }
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

            return tno; // 返回查询结果
        }


        //展示该教师的授课信息
        private void 授课信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // 清空数据

            // 获取教师的 Tno
            string tno = GetTeacherTnoById(Form1.name);
            //Console.WriteLine(tno);

            if (!string.IsNullOrEmpty(tno)) // 确保 tno 不是空字符串
            {
                // 使用参数化查询
                string sql = @"
                    SELECT
                        course.Cteacher_name,
                        course.Cno,     
                        course.Cname,  
                        course.Chour,
                        course.Cintroduce,
                        sk.Snum
                    FROM 
                        sk 
                    INNER JOIN 
                        course ON sk.Cno = course.Cno 
                    INNER JOIN 
                        teacher ON sk.Tno = teacher.Tno 
                    WHERE 
                        sk.Tno = @Tno";

                // 创建数据库连接
                Dao dao = new Dao();
                try
                {
                    dao.Connection();

                    // 创建命令对象
                    MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                    command.Parameters.AddWithValue("@Tno", tno);

                    // 使用 MySqlDataReader 获取查询结果
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) // 逐行读取数据
                        {
                            dataGridView1.Rows.Add(reader[0].ToString(), reader[2].ToString()
                                , reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                        }
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
                MessageBox.Show("未找到教师的信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public static string StudentInfoType;
        //修改学生信息
        private void 修改学生信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentInfoType = "修改";
            FormUpdate_DeleteStudent formUpdate_DeleteStudent = new FormUpdate_DeleteStudent();
            formUpdate_DeleteStudent.Show();
        }

        //删除学生
        private void 删除学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentInfoType = "删除";
            FormUpdate_DeleteStudent formUpdate_DeleteStudent = new FormUpdate_DeleteStudent();
            formUpdate_DeleteStudent.Show();
        }

        //修改课程信息
        private void 修改课程信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.Teacher == "CollegeLeader")
            {//该教师为学院负责人
                operation = "修改";
                FormCourse form = new FormCourse();
                form.ShowDialog();
            }
            else
            {//该教师为普通教师
                MessageBox.Show("您没有修改课程信息的权限！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //点击单元格内容触发
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        //单击单元格时触发
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value == null)
                {
                    MessageBox.Show("选中了无效数据！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    // 获取点击单元格所在行的数据
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    object cellValue = row.Cells[e.ColumnIndex].Value;
                    MessageBox.Show("授课教师：" + cellValue.ToString());
                }
                if (e.ColumnIndex == 1)
                {
                    // 获取点击单元格所在行的数据
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    object cellValue = row.Cells[e.ColumnIndex].Value;
                    // 在这里可以根据获取到的值进行各种操作
                    MessageBox.Show("课程：" + cellValue.ToString());
                }

                if (e.ColumnIndex == 2)
                {
                    // 获取点击单元格所在行的数据
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    object cellValue = row.Cells[e.ColumnIndex].Value;
                    // 在这里可以根据获取到的值进行各种操作
                    MessageBox.Show("学时：" + cellValue.ToString());
                }
                if (e.ColumnIndex == 3)
                {
                    // 获取点击单元格所在行的数据
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    object cellValue = row.Cells[e.ColumnIndex].Value;
                    // 在这里可以根据获取到的值进行各种操作
                    MessageBox.Show("课程简介：" + cellValue.ToString());
                }
                if (e.ColumnIndex == 4)
                {
                    // 获取点击单元格所在行的数据
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    object cellValue = row.Cells[e.ColumnIndex].Value;
                    // 在这里可以根据获取到的值进行各种操作
                    MessageBox.Show("选课人数：" + cellValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //设置学生课程考试成绩
        private void 设置课程成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetGrade formSetGrade = new FormSetGrade();
            formSetGrade.Show();
        }
    }
}

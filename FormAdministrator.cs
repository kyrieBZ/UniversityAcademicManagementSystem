using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace UniversityAcademicManagementSystem
{
   
    public partial class FormAdministrator : Form
    {
        public FormAdministrator()
        {
            InitializeComponent();

            //只有1号管理员有设置学生毕业要求的权限
            if (Form1.id != 1)
            {
                // 禁用与毕业要求相关的菜单项
                学分要求ToolStripMenuItem.Enabled = false;
                绩点要求ToolStripMenuItem.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 添加教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddTeacher form = new FormAddTeacher();
            form.ShowDialog();
        }

        private void 添加学院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddCollege form = new FormAddCollege();
            form.ShowDialog();
        }

        private void 注销账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            
            string sql = $"delete from administrator where ID='{Form1.id}'";

            try
            {
                dao.Connection();
                if (MessageBox.Show("确认要注销您的账号？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dao.Excute(sql) > 0)
                    {
                        MessageBox.Show("注销成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("注销失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dao.DaoClose();
            }
        }

        //修改管理员账户的密码
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            try
            {
                dao.Connection();
                // 创建一个自定义的输入框
                Form inputForm = new Form();
                inputForm.Text = "修改密码";
                inputForm.Size = new System.Drawing.Size(300, 150);

                Label label = new Label();
                label.Text = "请输入新密码:";
                label.Location = new System.Drawing.Point(10, 20);
                label.AutoSize = true;

                TextBox textBox = new TextBox();
                textBox.Location = new System.Drawing.Point(10, 50);
                textBox.Size = new System.Drawing.Size(260, 20);

                Button okButton = new Button();
                okButton.Text = "确定";
                okButton.Location = new System.Drawing.Point(100, 80);
                okButton.DialogResult = DialogResult.OK;

                inputForm.Controls.Add(label);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(okButton);

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string newPassword = textBox.Text;
                    string sql = $"update administrator set Password = '{newPassword}' where ID='{Form1.id}'";
                    if (dao.Excute(sql) > 0)
                    {
                        MessageBox.Show("修改密码成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("修改密码失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                dao.DaoClose();
            }
        }

        //提示功能
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提示:管理员可以管理自身的账户状态，" +
                "管理教师信息（添加，修改，删除），" +
                "管理学院信息（添加，修改，删除），管理反馈信息（教师反馈，学生反馈）" +
                "，设置学生毕业要求（学分数，GPA） 注：该功能仅有特定权限管理员才可以使用。" +
                "点击对应的功能选项即可进行操作。"
                , "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string teacherInfoType;//记录当前操作的教师信息类型

        //修改教师信息
        private void 修改教师信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            teacherInfoType = "修改";
            FormUpdate_DeleteTeacher formUpdate_DeleteTeacher 
                = new FormUpdate_DeleteTeacher();
            formUpdate_DeleteTeacher.Show();
        }
        
        //删除教师信息
        private void 删除教师ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            teacherInfoType = "删除";
            FormUpdate_DeleteTeacher formUpdate_DeleteTeacher
                = new FormUpdate_DeleteTeacher();
            formUpdate_DeleteTeacher.Show();
        }

        public static string collegeInfoType;

        //修改学院信息
        private void 修改学院信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            collegeInfoType = "修改";
            FormUpdate_DeleteCollege formUpdate_DeleteCollege= new FormUpdate_DeleteCollege();
            formUpdate_DeleteCollege.Show();
        }
        

        //删除学院
        private void 删除学院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            collegeInfoType = "删除";
            FormUpdate_DeleteCollege formUpdate_DeleteCollege = new FormUpdate_DeleteCollege();
            formUpdate_DeleteCollege.Show();
        }

        //窗体加载时欢迎管理员登录
        private void FormAdministrator_Load(object sender, EventArgs e)
        {
            label1.Text = "欢迎管理员：" + Form1.name+" 登录系统！";

            // 设置 ListView 列名
            listView1.Columns.Clear();
            listView1.Items.Clear();

            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("姓名", 100);
            listView1.Columns.Add("密码", 100);
            listView1.Columns.Add("性别", 50);
            listView1.Columns.Add("年龄", 50);

            LoadAdminInfo();

        }

        private void LoadAdminInfo()
        {
            // SQL 查询语句
            Dao dao = new Dao();
            string sql = @"SELECT ID, Name, PassWord, Sex, Age FROM  administrator WHERE Name = @adminName";
            try
            {
                // 建立数据库连接
                dao.Connection();

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@adminName", Form1.name); // 使用当前管理员的名字作为参数
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 创建 ListViewItem
                            ListViewItem item = new ListViewItem(reader.GetInt32("ID").ToString());
                            item.SubItems.Add(reader.GetString("Name"));
                            item.SubItems.Add(reader.GetString("PassWord"));
                            item.SubItems.Add(reader.GetString("Sex"));
                            item.SubItems.Add(reader.GetInt32("Age").ToString());
                            
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

        //处理教师反馈信息
        private void 教师反馈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建一个新的窗体
            Form feedbackForm = new Form
            {
                Text = "教师反馈信息", // 窗体标题
                Size = new System.Drawing.Size(400, 300), // 设置窗体大小
                StartPosition = FormStartPosition.CenterScreen // 窗体居中显示
            };

            // 创建 ListBox 控件用来显示反馈信息
            ListBox feedbackListBox = new ListBox
            {
                Dock = DockStyle.Fill // 填满整个窗体
            };

            //数据库读取反馈信息
            List<string> feedbacks = GetFeedbacksFromDatabase(); 
            foreach (var feedback in feedbacks)
            {
                feedbackListBox.Items.Add(feedback);
            }

            // 将 ListBox 控件添加到窗体
            feedbackForm.Controls.Add(feedbackListBox);

            // 显示反馈窗口
            feedbackForm.ShowDialog();
        }

        // 数据库中读取反馈信息
        private List<string> GetFeedbacksFromDatabase()
        {
            List<string> feedbacks = new List<string>();

            Dao dao = new Dao();
            try
            {
                // 建立数据库连接
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT FID, Uname, Fdate, FInfo, Ftype FROM feedback WHERE Ftype = '教师'";

                // 使用 MySqlDataReader 读取反馈信息
                using (MySqlDataReader reader = dao.read(sql))
                {
                    while (reader.Read()) // 逐行读取数据
                    {
                        // 获取反馈信息的各个字段
                        int fid = reader.GetInt32("FID");
                        string uname = reader.GetString("Uname");
                        DateTime fdate = reader.GetDateTime("Fdate");
                        string finfo = reader.GetString("FInfo");
                        string ftype = reader.GetString("Ftype");

                        // 将反馈信息格式化为两行
                        string line1 = $"[{fid}] {finfo}"; // 第一行包含 FID 和反馈信息
                        string line2 = $"{uname} ({fdate.ToString("yyyy-MM-dd")}) [{ftype}]"; // 第二行包含姓名、日期和类型

                        // 将两行信息添加到列表中
                        feedbacks.Add(line1);
                        feedbacks.Add(line2);
                        feedbacks.Add(""); // 添加空行作为间隔
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            finally
            {
                dao.DaoClose(); // 确保关闭连接
            }

            return feedbacks; // 返回所有反馈信息
        }

        // 处理学生反馈信息
        private void 学生反馈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建一个新的窗体
            Form feedbackForm = new Form
            {
                Text = "学生反馈信息", // 窗体标题
                Size = new System.Drawing.Size(400, 300), // 设置窗体大小
                StartPosition = FormStartPosition.CenterScreen // 窗体居中显示
            };

            // 创建 ListBox 控件用来显示反馈信息
            ListBox feedbackListBox = new ListBox
            {
                Dock = DockStyle.Fill // 填满整个窗体
            };

            // 数据库读取反馈信息
            List<string> feedbacks = GetStudentFeedbacksFromDatabase();
            foreach (var feedback in feedbacks)
            {
                feedbackListBox.Items.Add(feedback);
            }

            // 将 ListBox 控件添加到窗体
            feedbackForm.Controls.Add(feedbackListBox);

            // 显示反馈窗口
            feedbackForm.ShowDialog();
        }

        // 数据库中读取学生反馈信息
        private List<string> GetStudentFeedbacksFromDatabase()
        {
            List<string> feedbacks = new List<string>();

            Dao dao = new Dao();
            try
            {
                // 建立数据库连接
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT FID, Uname, Fdate, FInfo, Ftype FROM feedback WHERE Ftype = '学生'";

                // 使用 MySqlDataReader 读取反馈信息
                using (MySqlDataReader reader = dao.read(sql))
                {
                    while (reader.Read()) // 逐行读取数据
                    {
                        // 获取反馈信息的各个字段
                        int fid = reader.GetInt32("FID");
                        string uname = reader.GetString("Uname");
                        DateTime fdate = reader.GetDateTime("Fdate");
                        string finfo = reader.GetString("FInfo");
                        string ftype = reader.GetString("Ftype");

                        // 将反馈信息格式化为两行
                        string line1 = $"[{fid}] {finfo}"; // 第一行包含 FID 和反馈信息
                        string line2 = $"{uname} ({fdate.ToString("yyyy-MM-dd")}) [{ftype}]"; // 第二行包含姓名、日期和类型

                        // 将两行信息添加到列表中
                        feedbacks.Add(line1);
                        feedbacks.Add(line2);
                        feedbacks.Add(""); // 添加空行作为间隔
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            finally
            {
                dao.DaoClose(); // 确保关闭连接
            }

            return feedbacks; // 返回所有反馈信息
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private static double creditRequest;//学分要求
        private static double GPA_Request;//GPA要求

        public static double getCreditRequest()
        {
            double creditRequest = 0.0; // 初始化变量

            // 建立数据库连接
            Dao dao = new Dao();

            try
            {
                dao.Connection();

                // SQL 查询获取学分要求
                string sql = "SELECT CreditRequest FROM graduate_request ORDER BY ID DESC LIMIT 1"; // 获取最大的 ID 对应的 CreditRequest

                // 执行查询并获取结果
                using (MySqlDataReader reader = dao.read(sql))
                {
                    if (reader.Read()) // 读取查询结果
                    {
                        // 获取学分要求
                        creditRequest = reader.IsDBNull(0) ? 0 : reader.GetDouble(0); // 使用 GetDouble 以获取 double 类型
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 关闭数据库连接
                dao.DaoClose();
            }

            return creditRequest; // 返回学分要求
        }




        public static double getGPA_Request()
        {
            double gpaRequest = 0.0; // 初始化变量

            // 建立数据库连接
            Dao dao = new Dao();

            try
            {
                dao.Connection();

                // SQL 查询获取绩点要求
                string sql = "SELECT GPA_Request FROM graduate_request ORDER BY ID DESC LIMIT 1"; // 获取最大的 ID 对应的 GPA_Request

                // 执行查询并获取结果
                using (MySqlDataReader reader = dao.read(sql))
                {
                    if (reader.Read()) // 读取查询结果
                    {
                        // 获取绩点要求
                        gpaRequest = reader.IsDBNull(0) ? 0 : reader.GetDouble(0); // 使用 GetDouble 以获取 double 类型
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 关闭数据库连接
                dao.DaoClose();
            }

            return gpaRequest; // 返回绩点要求
        }

        private int getMaxID()
        {
            // 定义一个变量来接收最大 ID 值
            int maxId = 0;

            // 建立数据库连接
            Dao dao = new Dao();

            try
            {
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT MAX(ID) FROM graduate_request";

                // 执行查询并获取结果
                using (MySqlDataReader reader = dao.read(sql))
                {
                    if (reader.Read()) // 读取查询结果
                    {
                        // 获取最大 ID 值
                        maxId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // 如果没有记录，设置为0
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 关闭数据库连接
                dao.DaoClose();
            }

           return maxId;

        }

        //毕业的学分要求
        private void 学分要求ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建新的窗口
            Form creditRequirementForm = new Form
            {
                Text = "设置学分要求",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent // 窗口居中
            };

            // 创建提示标签
            Label label = new Label
            {
                Text = "请输入学分要求（学分数 >= 0.0）：",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true
            };

            // 创建文本框以输入学分要求
            TextBox textBox = new TextBox
            {
                Dock = DockStyle.Top,
                TextAlign = HorizontalAlignment.Center
            };

            // 创建确认按钮
            Button confirmButton = new Button
            {
                Text = "确认",
                Dock = DockStyle.Bottom
            };

            int max_id=getMaxID();

            // 按钮点击事件
            confirmButton.Click += (s, args) => {
                if (double.TryParse(textBox.Text, out double credits) && credits >= 0)
                {
                    MessageBox.Show("学分要求已设置为：" + credits, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    creditRequest = credits;

                    // 将学分要求写入数据库
                    try
                    {
                        // 假设你已经有一个 Dao 类来处理数据库连接
                        Dao dao = new Dao();
                        dao.Connection(); // 建立数据库连接

                        // SQL 插入语句，将学分要求插入到 graduate_request 表中
                        string sql = $"INSERT INTO graduate_request (ID,CreditRequest) VALUES ({max_id + 1}, {credits})";

                        // 执行 SQL 语句
                        if (dao.Excute(sql) > 0)
                        {
                            MessageBox.Show("学分要求设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("学分要求设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        dao.DaoClose(); // 关闭数据库连接
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"写入数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                    Console.WriteLine("学分要求：" + creditRequest);
                    creditRequirementForm.Close();
                }
                else
                {
                    MessageBox.Show("请输入有效的学分数（>= 0.0）", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // 将控件添加到窗口
            creditRequirementForm.Controls.Add(confirmButton);
            creditRequirementForm.Controls.Add(textBox);
            creditRequirementForm.Controls.Add(label);

            // 显示窗口
            creditRequirementForm.ShowDialog();
        }

        //毕业GPA要求
        private void 绩点要求ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建新的窗口
            Form gpaRequirementForm = new Form
            {
                Text = "设置绩点要求",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent // 窗口居中
            };

            // 创建提示标签
            Label label = new Label
            {
                Text = "请输入绩点要求（GPA >= 0.0）：",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true
            };

            // 创建文本框以输入绩点要求
            TextBox textBox = new TextBox
            {
                Dock = DockStyle.Top,
                TextAlign = HorizontalAlignment.Center
            };

            // 创建确认按钮
            Button confirmButton = new Button
            {
                Text = "确认",
                Dock = DockStyle.Bottom
            };

            // 按钮点击事件
            confirmButton.Click += (s, args) => {
                if (double.TryParse(textBox.Text, out double gpa) && gpa >= 2.0)
                {
                    MessageBox.Show("绩点要求已设置为：" + gpa, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int max_id=getMaxID();
                    try
                    {
                        // 建立数据库连接
                        Dao dao = new Dao();
                        dao.Connection();

                        // SQL 插入语句，将绩点要求插入到 graduate_request 表中
                        string sql = $"update graduate_request set GPA_Request = {gpa} where ID = {max_id}";

                        // 执行 SQL 语句
                        if (dao.Excute(sql) > 0)
                        {
                            MessageBox.Show("绩点要求设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("绩点要求设置失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        dao.DaoClose(); // 关闭数据库连接
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"写入数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("请输入有效的绩点（>= 0.0）", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // 将控件添加到窗口
            gpaRequirementForm.Controls.Add(confirmButton);
            gpaRequirementForm.Controls.Add(textBox);
            gpaRequirementForm.Controls.Add(label);

            // 显示窗口
            gpaRequirementForm.ShowDialog();
        }

    }
}

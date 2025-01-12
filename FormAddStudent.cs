using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UniversityAcademicManagementSystem
{
    public partial class FormAddStudent : Form
    {
        public FormAddStudent()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormAddStudent_Load(object sender, EventArgs e)
        {
            textBox2.Text=(getMaxID()+1).ToString();
            textBox2.Enabled = false;

            string teacherId = Form1.Tno; 

            // 获取专业名列表
            List<string> majors = GetMajorsByTeacherId(teacherId);

            // 将专业名添加到 comboBox2
            comboBox2.Items.Clear(); // 清空之前的项
            comboBox2.Items.AddRange(majors.ToArray()); // 将专业名列表添加到 ComboBox
        }

        //获取教师所在学院
        public string GetCollegeNameByTeacherId(string teacherId)
        {
            string collegeName = string.Empty; // 定义变量来接收学院名

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询语句
                string sql = @"
                    SELECT c.COname 
                    FROM teacher t
                    JOIN college c ON t.COno = c.COno 
                    WHERE t.Tno = @teacherId"; // 使用参数化查询

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@teacherId", teacherId); // 添加参数

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // 读取查询结果
                        {
                            collegeName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0); // 如果没有记录，设置为空
                        }
                    }
                }

                // 关闭数据库连接
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return collegeName; // 返回学院名
        }

        //获取教师所在学院设置的所有专业
        public List<string> GetMajorsByTeacherId(string teacherId)
        {
            var majorsList = new List<string>(); // 存储专业名的列表

            // 建立数据库连接
            Dao dao = new Dao();
            try
            { 
                // SQL 查询语句
                string sql = @"
                    SELECT 
                        m.Mname 
                    FROM 
                        teacher t
                    JOIN 
                        major m ON t.COno = m.Mcono
                    WHERE 
                        t.Tno = @teacherId"; // 使用参数化查询

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@teacherId", teacherId); // 添加参数

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            majorsList.Add(reader.GetString("Mname")); // 将专业名添加到列表
                        }
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return majorsList; // 返回专业名列表
        }


        private int getMaxID()
        {
            int maxId = 0; // 定义变量来接收最大 ID 值

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT MAX(ID) FROM student"; // 查询 student 表中的最大 ID

                // 执行查询并获取结果
                using (MySqlDataReader reader = dao.read(sql))
                {
                    if (reader.Read()) // 读取查询结果
                    {
                        maxId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // 如果没有记录，设置为0
                    }
                }

                // Optional: 显示最大 ID 值（用于调试或验证）
                Console.WriteLine("Student 表中最大 ID: " + maxId);

                // 关闭数据库连接
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return maxId; // 返回最大 ID
        }


        private void button2_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" ||
                textBox4.Text == "" || textBox5.Text == "" || comboBox2.Text == "" ||
                comboBox1.Text == "")
            {
                MessageBox.Show("学生信息存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dao dao = new Dao();
            try
            {
                // 根据专业名查询对应的专业编号
                int majorId = GetMajorIdByName(comboBox2.Text, dao);

                if (majorId == -1)
                {
                    MessageBox.Show("未找到对应的专业编号！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 使用查询到的专业编号构造插入 SQL 语句
                string sql = $"INSERT INTO student VALUES('{textBox1.Text}', '{int.Parse(textBox2.Text)}', '{textBox3.Text}', '{textBox4.Text}', '{comboBox1.Text}', '{int.Parse(textBox5.Text)}', '{majorId}')";

                if (dao.Excute(sql) > 0)
                {
                    MessageBox.Show("添加成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
            }
        }

        private int GetMajorIdByName(string majorName, Dao dao)
        {
            int majorId = -1;

            try
            {
                // SQL 查询语句
                string sql = "SELECT Mno FROM major WHERE Mname = @Mname"; // 根据专业名查询专业编号

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@Mname", majorName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // 读取查询结果
                        {
                            majorId = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return majorId; // 返回专业编号
        }

    }
}

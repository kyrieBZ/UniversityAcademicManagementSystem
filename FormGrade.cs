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
    public partial class FormGrade : Form
    {
        public FormGrade()
        {
            InitializeComponent();
        }

        //退出
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //加载该学生的所有选修课程的成绩
        private void FormGrade_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // 清空数据

            // 学生的学号
            string studentId = Form1.Sno; 

            // 使用参数化查询
            string sql = @"
                SELECT 
                    course.Cno,     
                    course.Cname,  
                    sc.grade
                FROM 
                    sc 
                INNER JOIN 
                    course ON sc.Cno = course.Cno 
                WHERE 
                    sc.Sno = @Sno"; // 使用参数化查询

            Dao dao = new Dao(); // 创建 Dao 对象
            try
            {
                dao.Connection(); // 打开数据库连接

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@Sno", studentId); // 设置参数

                // 使用 MySqlDataReader 获取查询结果
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) // 逐行读取数据
                    {
                        // 将读取到的数据添加到 DataGridView
                        dataGridView1.Rows.Add(reader["Cno"].ToString(), reader["Cname"].ToString(), reader["grade"].ToString());
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

        //查询课程成绩
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // 清空DataGridView

            // 从 TextBox 中获取输入的课程名
            string courseName = textBox1.Text.Trim();
            string studentId = Form1.Sno; 

            // 使用参数化查询
            string sql = @"
        SELECT 
            course.Cno,     
            course.Cname,  
            sc.grade
        FROM 
            sc 
        INNER JOIN 
            course ON sc.Cno = course.Cno 
        WHERE 
            sc.Sno = @StudentId AND 
            course.Cname LIKE @CourseName"; // 使用学生学号和模糊查询

            Dao dao = new Dao(); // 创建 Dao 对象
            try
            {
                dao.Connection(); // 打开数据库连接

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@StudentId", studentId); // 学生学号参数
                command.Parameters.AddWithValue("@CourseName", "%" + courseName + "%"); // 模糊查询参数

                // 使用 MySqlDataReader 获取查询结果
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) // 逐行读取数据
                    {
                        // 将读取到的数据添加到 DataGridView
                        dataGridView1.Rows.Add(reader["Cno"].ToString(), reader["Cname"].ToString(), reader["grade"].ToString());
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

    }
}

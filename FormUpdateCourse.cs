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
    public partial class FormUpdateCourse : Form
    {
        public FormUpdateCourse()
        {
            InitializeComponent();
            textBox1.Text = FormCourse.courseOrder;
            textBox1.Enabled = false;
            textBox2.Text = FormCourse.courseName;
            textBox3.Text = FormCourse.courseTeacherID;
            textBox4.Text = FormCourse.courseTeacherName;
            textBox6.Text = FormCourse.courseCredit.ToString();
            textBox7.Text = FormCourse.courseHour;
            textBox5.Text = FormCourse.courseIntroduce;
        }

        //退出
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string status;

        // 修改信息
        private void button1_Click(object sender, EventArgs e)
        {
            // 创建数据库连接
            Dao dao = new Dao();
            try
            {
                dao.Connection();

                // 使用参数化查询
                string sql = "UPDATE course SET Cname = @Cname, Cteacher_id = @TeacherID, Cteacher_name = @TeacherName, Credit= @Credit,Chour = @Hour, Cintroduce = @Introduce WHERE Cno = @CourseOrder";

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());

                // 添加参数
                command.Parameters.AddWithValue("@Cname", textBox2.Text);
                command.Parameters.AddWithValue("@TeacherID", textBox3.Text);
                command.Parameters.AddWithValue("@TeacherName", textBox4.Text);
                command.Parameters.AddWithValue("@Credit", textBox6.Text);
                command.Parameters.AddWithValue("@Hour", textBox7.Text);
                command.Parameters.AddWithValue("@Introduce", textBox5.Text);
                command.Parameters.AddWithValue("@CourseOrder", textBox1.Text);

                // 执行更新操作
                int rowsAffected = command.ExecuteNonQuery();

                // 检查更新结果
                if (rowsAffected > 0)
                {
                    MessageBox.Show("课程信息修改成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    status = "成功";
                }
                else
                {
                    MessageBox.Show("修改失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    status = "失败";
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

        private void FormUpdateCourse_Load(object sender, EventArgs e)
        {

        }
    }
}

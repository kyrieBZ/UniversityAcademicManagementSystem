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
    public partial class FormUpdateStudent : Form
    {
        public FormUpdateStudent()
        {
            InitializeComponent();
            textBox1.Text = FormUpdate_DeleteStudent.studentOrder;
            textBox1.Enabled = false;
            textBox2.Text = FormUpdate_DeleteStudent.studentID.ToString();
            textBox3.Text = FormUpdate_DeleteStudent.studentPassword;
            textBox4.Text = FormUpdate_DeleteStudent.studentName;
            comboBox1.Text = FormUpdate_DeleteStudent.studentGender;
            textBox5.Text = FormUpdate_DeleteStudent.studentAge;
            textBox6.Text = FormUpdate_DeleteStudent.studentMajorID;
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
                string sql = "UPDATE student SET ID = @ID, PassWord = @Password, Sname = @Name, Sex = @Sex, Sage = @Age, Mno = @MajorID WHERE Sno = @Sno";

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());

                // 添加参数
                command.Parameters.AddWithValue("@ID", textBox2.Text);
                command.Parameters.AddWithValue("@Password", textBox3.Text);
                command.Parameters.AddWithValue("@Name", textBox4.Text);
                command.Parameters.AddWithValue("@Sex", comboBox1.Text);
                command.Parameters.AddWithValue("@Age", textBox5.Text);
                command.Parameters.AddWithValue("@MajorID", textBox6.Text);
                command.Parameters.AddWithValue("@Sno", textBox1.Text);

                // 执行更新操作
                int rowsAffected = command.ExecuteNonQuery();

                // 检查更新结果
                if (rowsAffected > 0)
                {
                    MessageBox.Show("学生信息修改成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}

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
    public partial class FormAddTeacher : Form
    {
        public FormAddTeacher()
        {
            InitializeComponent();
            textBox2.Text=(getMaxID()+1).ToString();
            textBox2.Enabled = false;
        }

        private int getMaxID()
        {
            // 定义一个变量来接收最大 ID 值
            int maxId = 0;

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT MAX(ID) FROM teacher"; // 查询最大 ID

                // 执行查询并获取结果
                using (MySqlDataReader reader = dao.read(sql))
                {
                    if (reader.Read()) // 读取查询结果
                    {
                        // 获取最大 ID 值
                        maxId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // 如果没有记录，设置为0
                    }
                }

                // Optional: 显示最大 ID 值（用于调试或验证）
                Console.WriteLine("Teacher 表中最大 ID: " + maxId);

                // 关闭数据库连接
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return maxId; // 返回最大 ID
        }


        private void button1_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {//添加教师
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("教师信息存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string state="";//标识教师为学院负责人或普通教师的字符串
            if (radioButton1.Checked)
            {//该教师为学院负责人
                state = "true";
            }
            if(radioButton2.Checked)
            {
                state = "false";
            }

            Dao dao = new Dao();
            
            string sql = $"insert into teacher values('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{comboBox1.Text}','{textBox5.Text}','{textBox6.Text}','{state}')";
            try
            {
                dao.Connection();
                if (dao.Excute(sql) > 0)
                {
                    MessageBox.Show("添加成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
            }
        }
    }
}

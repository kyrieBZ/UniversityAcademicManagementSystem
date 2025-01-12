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
    public partial class FormAddMajor : Form
    {
        public FormAddMajor()
        {
            InitializeComponent();
        }

        private void FormAddMajor_Load(object sender, EventArgs e)
        {
            textBox1.Text = (getMaxMajorNo()+1).ToString();
            textBox1.Enabled = false;
        }

        private int getMaxMajorNo() // 修改返回值类型为 int
        {
            int maxMno = 0; // 定义变量来接收最大 Mno 值

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT MAX(Mno) FROM major"; // 查询最大 Mno

                // 执行查询并获取结果
                using (MySqlDataReader reader = dao.read(sql))
                {
                    if (reader.Read()) // 读取查询结果
                    {
                        maxMno = reader.IsDBNull(0) ? 0 : reader.GetInt32(0); // 如果没有记录，设置为0
                    }
                }

                // Optional: 显示最大 Mno 值（用于调试或验证）
                Console.WriteLine("Major 表中最大 Mno: " + maxMno);

                // 关闭数据库连接
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return maxMno; // 返回最大 Mno
        }




        private void button2_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//添加专业
            if(textBox1.Text==""||textBox2.Text==""||textBox3.Text==""||
                textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("专业信息存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dao dao = new Dao();

            try
            {
                dao.Connection();
                string sql = $"insert into major values('{int.Parse(textBox1.Text)}','{textBox2.Text}','{int.Parse(textBox3.Text)}','{textBox5.Text}','{textBox4.Text}')";
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
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
            }
        }   
    }
}

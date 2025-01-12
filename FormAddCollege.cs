using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversityAcademicManagementSystem
{
    public partial class FormAddCollege : Form
    {
        public FormAddCollege()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//添加学院
            if(textBox1.Text==""||textBox2.Text==""||textBox3.Text==""||
                textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("学院信息存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dao dao= new Dao();
           
            string sql = $"insert into college values('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}')";

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
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
            }
        }
    }
}

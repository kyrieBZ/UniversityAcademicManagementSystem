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
using Mysqlx.Crud;

namespace UniversityAcademicManagementSystem
{
    public partial class FormAdministratorRegister : Form
    {
        public FormAdministratorRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//注册
            if (textBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("账户信息存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox4.Text.Trim() != textBox5.Text.Trim())
            {
                MessageBox.Show("确认密码与密码不一致！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dao dao= new Dao();
            dao.Connection();

            string sql0 = "select Max(ID) from Administrator";
            MySqlDataReader reader=dao.read(sql0);
            reader.Read();
            int id = int.Parse(reader[0].ToString());//注册账户的账号
            reader.Close();

            if (id >= 5)
            {//限制系统管理员用户最多为5个
                MessageBox.Show("系统管理员人数已到达上限，注册失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = $"insert into Administrator values('{++id}','{textBox1.Text}','{textBox4.Text}','{comboBox1.Text}','{textBox3.Text}','{textBox2.Text}')";
            if (dao.Excute(sql) > 0)
            {
                MessageBox.Show("注册成功！您的账户的账号为：" + $"{id}", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //清空文本框内容
                textBox1.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox2.Clear();
                comboBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("注册失败！","消息",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dao.DaoClose();
        }

        private void button2_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }
    }
}

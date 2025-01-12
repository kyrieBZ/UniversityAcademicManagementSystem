using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UniversityAcademicManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        public static string Teacher;//教师职位
        public static int id;//用户账号
        public static string name;//用户姓名
        public static string Sno;//学生学号
        public static string Tno;//教师编号
        

        private void button3_Click(object sender, EventArgs e)
        {//提示
            MessageBox.Show("登录界面，需要输入账号密码进行登录，登录时选择对应的用户进行登录。" +
                "账号密码：管理员用户可以通过注册功能注册，教师用户可以联系管理员获取，" +
                "学生用户可以联系教师获取！ 注意：管理员注册账户时需要先选中‘管理员’单选框才能进行注册，而教师和学生则没有权限使用注册功能！", "消息",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {//管理员注册
            if (radioButton1.Checked == true)
            {
                FormAdministratorRegister form = new FormAdministratorRegister();
                form.ShowDialog();
            }
            else if (radioButton2.Checked == true||radioButton3.Checked==true) 
            { 
                MessageBox.Show("您没有注册权限！","消息",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AdministratorLogin()//管理员登录方法
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("账号或密码信息存在空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dao dao = new Dao();
            
            string sql = $"select ID,PassWord,Name from Administrator where ID='{int.Parse(textBox1.Text)}'and PassWord='{textBox2.Text}'";
            MySqlDataReader reader=dao.read(sql);

            try
            {
                dao.Connection();
                if (reader.Read())
                {
                    MessageBox.Show("登录成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    id = int.Parse(reader[0].ToString());
                    name = reader[2].ToString();
                    FormAdministrator form = new FormAdministrator();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登录失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader.Close();
                dao.DaoClose();
            }
        }

        private void TeacherLogin()//教师登录方法
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("账号或密码存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dao dao=new Dao();
            
            string sql = $"select ID,PassWord,IsLeader,Tname,Tno from teacher where ID='{int.Parse(textBox1.Text)}' and PassWord='{textBox2.Text}'";
            MySqlDataReader reader= dao.read(sql);

            try
            {
                dao.Connection();
                if (reader.Read())
                {
                    if (reader[2].ToString() == "true")
                    {
                        Teacher = "CollegeLeader";
                    }
                    else
                    {
                        Teacher = "CommonTeacher";
                    }
                    MessageBox.Show("登录成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    id = int.Parse(reader[0].ToString());
                    name = reader[3].ToString();
                    Tno = reader[4].ToString();

                    FormTeacher form = new FormTeacher();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登陆失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader.Close();
                dao.DaoClose();
            }
        }

        private void StudentLogin()//学生登录方法
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("账号或密码存在空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dao dao= new Dao();
            
            string sql = $"select ID,PassWord,Sname,Sno from student where ID='{textBox1.Text}' and PassWord='{textBox2.Text}'";
            MySqlDataReader reader= dao.read(sql);

            try
            {
                dao.Connection();
                if (reader.Read())
                {
                    MessageBox.Show("登录成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    id = int.Parse(reader[0].ToString());
                    name = reader[2].ToString();
                    Sno = reader[3].ToString();
                    FormStudent form = new FormStudent();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登录失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader.Close();
                dao.DaoClose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//登录功能
            if(radioButton1.Checked )
            {
                AdministratorLogin();//管理员登录
            }
            if(radioButton2.Checked )
            {
                TeacherLogin();//教师登录
            }
            if( radioButton3.Checked )
            {
                StudentLogin();//学生登录
            }
            
        }
    }
}

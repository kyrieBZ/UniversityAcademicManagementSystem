using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UniversityAcademicManagementSystem
{
    public partial class FormUpdateTeacher : Form
    {
        public FormUpdateTeacher()
        {
            InitializeComponent();
            textBox1.Text = FormUpdate_DeleteTeacher.Tno;
            textBox1.Enabled=false;
            textBox2.Text = FormUpdate_DeleteTeacher.ID.ToString();
            textBox3.Text = FormUpdate_DeleteTeacher.PassWord;
            textBox4.Text = FormUpdate_DeleteTeacher.Tname;
            comboBox1.Text = FormUpdate_DeleteTeacher.TSex;
            textBox5.Text = FormUpdate_DeleteTeacher.Tage.ToString();
            textBox6.Text = FormUpdate_DeleteTeacher.COno;
            textBox6.Enabled = false;

            if (FormUpdate_DeleteTeacher.IsLeader == "true")
            {
                radioButton1.Checked = true;
            }
            else if(FormUpdate_DeleteTeacher.IsLeader == "false")
            {
                radioButton2.Checked = true;
            }
        }

        //退出
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string Tno;
        public static int ID;
        public static string PassWord;
        public static string TName;
        public static string TSex;
        public static int Tage;
        public static string COno;
        public static string IsLeader;

        public static string status;

        //修改信息
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Tno = textBox1.Text;
                ID = int.Parse(textBox2.Text);
                PassWord = textBox3.Text;
                TName = textBox4.Text;
                TSex = comboBox1.Text;
                Tage = int.Parse(textBox5.Text);
                COno = textBox6.Text;
                if (radioButton1.Checked)
                {
                    IsLeader = "true";
                }
                else if (radioButton2.Checked)
                {
                    IsLeader = "false";
                }

                Dao dao = new Dao();
                dao.Connection();
                string sql = $"update teacher set ID='{ID}',PassWord='{PassWord}',Tname='{TName}',Tsex='{TSex}',Tage='{Tage}',COno='{COno}',IsLeader='{IsLeader}' where Tno='{Tno}'";
                if (MessageBox.Show("确认修改该教师的信息吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dao.Excute(sql) > 0)
                    {
                        MessageBox.Show("修改成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        status = "成功";
                    }
                    else
                    {
                        MessageBox.Show("修改失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        status = "失败";
                    }
                }
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}

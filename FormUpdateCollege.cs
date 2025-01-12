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
    public partial class FormUpdateCollege : Form
    {
        public FormUpdateCollege()
        {
            InitializeComponent();
            textBox1.Text = FormUpdate_DeleteCollege.collegeOrder;
            textBox1.Enabled = false;
            textBox2.Text = FormUpdate_DeleteCollege.collegeName;
            textBox3.Text = FormUpdate_DeleteCollege.collegeLeaderID;
            textBox4.Text = FormUpdate_DeleteCollege.collegeLeaderName;
            textBox5.Text = FormUpdate_DeleteCollege.collegeIntroduction;
        }

        //退出
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string status;
        //修改信息
        private void button1_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            dao.Connection();
            string sql = $"update college set COname='{textBox2.Text}'" +
                $",COleader_id='{textBox3.Text}'" +
                $",COleader_name='{textBox4.Text}'" +
                $",COintroduce='{textBox5.Text}' where COno='{FormUpdate_DeleteCollege.collegeOrder}'";
            if(MessageBox.Show("是否确认修改该学院的信息？", "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
    }
}

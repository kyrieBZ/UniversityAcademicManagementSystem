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
using Org.BouncyCastle.Asn1;

namespace UniversityAcademicManagementSystem
{
    public partial class FormUpdate_DeleteCollege : Form
    {
        private System.Windows.Forms.Timer statusCheckTimer;
        public FormUpdate_DeleteCollege()
        {
            InitializeComponent();
            if (FormAdministrator.collegeInfoType == "修改")
            {
                button2.Enabled = false;
            }
            else if (FormAdministrator.collegeInfoType == "删除")
            {
                button1.Enabled = false;
            }

            // 初始化定时器
            statusCheckTimer = new System.Windows.Forms.Timer();
            statusCheckTimer.Interval = 1000; // 每 1 秒检查一次
            statusCheckTimer.Tick += StatusCheckTimer_Tick;
            statusCheckTimer.Start();
        }

        // 定时器事件处理程序
        private void StatusCheckTimer_Tick(object sender, EventArgs e)
        {
            if (FormUpdateCollege.status == "成功")
            {
                LoadCollege();
                //statusCheckTimer.Stop();
                FormUpdateCollege.status = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //退出
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //学院简介
        private void button6_Click(object sender, EventArgs e)
        {
            string collegeInfo = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            MessageBox.Show("学院简介："+collegeInfo, "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //加载学院信息
        private void LoadCollege()
        {
            Dao dao = new Dao();
            
            string sql = "select * from college";
            MySqlDataReader reader = dao.read(sql);

            try
            {
                dao.Connection();
                dataGridView1.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                reader.Close();
                dao.DaoClose();
            }
        }

        //窗体加载时显示学院信息
        private void FormUpdate_DeleteCollege_Load(object sender, EventArgs e)
        {
            LoadCollege();
        }

        //单击单元格时的事件
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选中了无效数据！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                if(FormAdministrator.collegeInfoType == "修改")
                {
                    button1.Enabled = true;
                }
                else if(FormAdministrator.collegeInfoType == "删除")
                {
                    button2.Enabled = true;
                }
            }
            
        }

        //提示信息
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提示：可以根据学院名称来查询学院，选中学院后可以修改这一学院的信息或删除学院。", "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        //查询功能
        private void button4_Click(object sender, EventArgs e)
        {
            string collegeName = textBox1.Text;
            string sql="select * from college where COname like '%"+collegeName+"%'";
            Dao dao = new Dao();
            dao.Connection();
            MySqlDataReader reader = dao.read(sql);
            dataGridView1.Rows.Clear();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            dao.DaoClose();
            reader.Close();
        }

        //删除学院
        private void button2_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            dao.Connection();
            string sql="delete from college where COno="+dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if(MessageBox.Show("确认删除该学院吗？", "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (dao.Excute(sql) > 0)
                {
                    MessageBox.Show("删除成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCollege();
                }
                else
                {
                    MessageBox.Show("删除失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            dao.DaoClose();
        }

        public static string collegeOrder;
        public static string collegeName;
        public static string collegeLeaderID;
        public static string collegeLeaderName;
        public static string collegeIntroduction;

        private void InitCollegeInfo()
        {
            collegeOrder = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            collegeName = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            collegeLeaderID = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            collegeLeaderName = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            collegeIntroduction = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        

        //修改学院信息
        private void button1_Click(object sender, EventArgs e)
        {
            InitCollegeInfo();
            FormUpdateCollege formUpdateCollege = new FormUpdateCollege();
            formUpdateCollege.Show();
        }
    }
}

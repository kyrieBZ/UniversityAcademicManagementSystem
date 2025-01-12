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
using System.Threading; // 确保引用了这个命名空间

namespace UniversityAcademicManagementSystem
{
    public partial class FormUpdate_DeleteTeacher : Form
    {
        private System.Windows.Forms.Timer statusCheckTimer; // 声明 statusCheckTimer 变量
        public FormUpdate_DeleteTeacher()
        {
            InitializeComponent();
            if (FormAdministrator.teacherInfoType == "修改")
            {
                button2.Enabled = false;
            }
            else if (FormAdministrator.teacherInfoType == "删除")
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
            if (FormUpdateTeacher.status == "成功")
            {
                LoadTeacher();
                //statusCheckTimer.Stop();
                FormUpdateTeacher.status = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void LoadTeacher()
        {
            Dao dao = new Dao();
            dao.Connection();
            string sql = $"select * from teacher";
            MySqlDataReader reader = dao.read(sql);
            dataGridView1.Rows.Clear();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString(),
                    reader[6].ToString(), reader[7].ToString());
            }
            reader.Close();
            dao.DaoClose();
        }

        private void FormUpdate_DeleteTeacher_Load(object sender, EventArgs e)
        {//修改/删除教师窗体加载事件
            LoadTeacher();//将所有教师信息加载到网格中
        }


        //提示功能
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提示：可以根据教师是否是学院负责人来选择需要修改信息或删除的教师", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //根据教师是否是学院负责人来查询教师
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "是")
            {
                Dao dao = new Dao();
                dao.Connection();
                string sql = $"select * from teacher where IsLeader='true'";
                MySqlDataReader reader = dao.read(sql);
                dataGridView1.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString(), reader[5].ToString(),
                        reader[6].ToString(), reader[7].ToString());
                }
                reader.Close();
                dao.DaoClose();
            }
            else if (comboBox1.Text == "否")
            {
                Dao dao = new Dao();
                dao.Connection();
                string sql = $"select * from teacher where IsLeader='false'";
                MySqlDataReader reader = dao.read(sql);
                dataGridView1.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString(), reader[5].ToString(),
                        reader[6].ToString(), reader[7].ToString());
                }
                reader.Close();
                dao.DaoClose();
            }
            else
            {
                MessageBox.Show("请选择是否是学院负责人！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //删除教师
        private void button2_Click(object sender, EventArgs e)
        {
            Dao dao = new Dao();
            try
            {
                dao.Connection();
                string sql = $"delete from teacher where Tno=" +
                    $"'{dataGridView1.CurrentRow.Cells[0].Value.ToString()}'";
                if (MessageBox.Show("确定要删除选中的教师？", "消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (dao.Excute(sql) > 0)
                    {
                        MessageBox.Show("删除成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTeacher();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
            }
        }

        public static string Tno;
        public static int ID;
        public static string PassWord;
        public static string Tname;
        public static string TSex;
        public static int Tage;
        public static string COno;
        public static string IsLeader;

        private void InitializeData()
        {
            Tno = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ID=int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            PassWord = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Tname = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TSex = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            Tage = int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            COno = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            IsLeader = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        //修改教师信息
        private void button1_Click(object sender, EventArgs e)
        {
            InitializeData();
            FormUpdateTeacher formUpdateTeacher = new FormUpdateTeacher();
            formUpdateTeacher.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //选择了无效数据则禁止用户使用删除/修改功能
            if (dataGridView1.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选中了无效数据！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                if (FormAdministrator.teacherInfoType == "修改")
                {
                    button1.Enabled = true;
                }
                else if (FormAdministrator.teacherInfoType == "删除")
                {
                    button2.Enabled = true;
                }
            }
        }
    }
}
           

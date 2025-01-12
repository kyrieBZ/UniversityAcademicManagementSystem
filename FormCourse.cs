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
    public partial class FormCourse : Form
    {
        private System.Windows.Forms.Timer statusCheckTimer;
        public FormCourse()
        {
            InitializeComponent();
            // 初始化定时器
            statusCheckTimer = new System.Windows.Forms.Timer();
            statusCheckTimer.Interval = 1000; // 每 1 秒检查一次
            statusCheckTimer.Tick += StatusCheckTimer_Tick;
            statusCheckTimer.Start();

            if (FormTeacher.operation == "添加")
            {
                button2.Enabled = false;
                button8.Enabled = false;
            }
            else if(FormTeacher.operation == "修改")
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else if(FormTeacher.operation == "删除")
            {
                button1.Enabled = false;
                button8.Enabled = false;
            }
        }

        // 定时器事件处理程序
        private void StatusCheckTimer_Tick(object sender, EventArgs e)
        {
            if (FormUpdateCourse.status == "成功")
            {
                LoadCourseTable();
                //statusCheckTimer.Stop();
                FormUpdateCourse.status = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        //加载学校开设的所有课程信息
        private void LoadCourse()
        {
            try
            {
                dataGridView1.Rows.Clear();
                Dao dao = new Dao();
                dao.Connection();
                string sql = $"select Cno,Cname,Cteacher_id,Cteacher_name,Credit,Chour,Cintroduce from all_course ";
                MySqlDataReader reader = dao.read(sql);
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString(), reader[4].ToString(),
                        reader[5].ToString(), reader[6].ToString());
                }
                reader.Close();
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //加载课程表信息
        private void LoadCourseTable()
        {
            dataGridView1.Rows.Clear();
            string sql="select * from course";
            Dao dao = new Dao();
            MySqlDataReader reader = dao.read(sql);
            try
            {
                dao.Connection();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
                reader.Close();
            }
            
        }

        private void FormCourse_Load(object sender, EventArgs e)
        {//添加/删除窗体加载事件
            LoadCourse();//将学校所有课程的信息加载到网格中
        }

        private void button5_Click(object sender, EventArgs e)
        {//查看课程简介
            try
            {
                MessageBox.Show(dataGridView1.CurrentRow.Cells[6].Value.ToString(), "课程简介", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {//查询课程信息
            if (textBox1.Text == "")
            {
                LoadCourse();
                return;
            }

            try
            {
                Dao dao = new Dao();
                dao.Connection();
                string sql = $"select Cno,Cname,Cteacher_id,Cteacher_name,Credit,Chour,Cintroduce from all_course where College='{textBox1.Text}'";
                MySqlDataReader reader = dao.read(sql);
                dataGridView1.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                }
                reader.Close();
                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value==null)
            {
                MessageBox.Show("选中了无效数据！","消息",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button1.Enabled = false;
                button2.Enabled = false;
                button8.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                if (FormTeacher.operation == "添加")
                {
                    button1.Enabled = true;
                }
                else if (FormTeacher.operation == "修改")
                {
                    button8.Enabled = true;
                }
                else if (FormTeacher.operation == "删除")
                {
                    button2.Enabled = true;
                }
                button5.Enabled = true;
            }
        }

        //获取教师所属学院
        private string getCollege(string tno)
        {
            string academyName = string.Empty; // 定义用于存储学院名称的变量
            Dao dao= new Dao();
            try
            {
                
                dao.Connection();

                // SQL 查询语句
                string sql = "SELECT c.COname FROM college c " +
                                "JOIN teacher t ON c.COno = t.COno " +
                                "WHERE t.Tno = @TeacherID"; 

                using (MySqlCommand command = new MySqlCommand(sql, dao.Connection()))
                {
                    command.Parameters.AddWithValue("@TeacherID",tno ); // 设置参数

                    // 执行查询
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) // 如果有结果
                    {
                        academyName = reader["COname"].ToString(); // 获取学院名称并赋值
                    }
                    reader.Close();
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

            return academyName;
        }

        private int getMaxid()
        {
            int maxId = 0; // 初始化最大ID

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                // SQL 查询获取 TeachID 的最大值
                string sql = "SELECT MAX(TeachID) FROM sk"; // 假设表名是 sk

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    // 执行查询并获取结果
                    object result = cmd.ExecuteScalar(); // 获取第一行第一列的值

                    // 检查结果是否为 null，然后赋值
                    if (result != DBNull.Value)
                    {
                        maxId = Convert.ToInt32(result); // 转换为整数
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return maxId; // 返回最大ID
        }


        private void saveTeachInfo()
        {
            int max_id=getMaxid();
            string sql = "insert into sk values(@TeachID, @Cno, @Cname, @Tno, @Tname, @introduce,@sum)";
            Dao dao = new Dao();
            try
            {
                dao.Connection();

                MySqlCommand command = new MySqlCommand(sql,dao.Connection());
                command.Parameters.AddWithValue("@TeachID", max_id + 1);
                command.Parameters.AddWithValue("@Cno", courseOrder);
                command.Parameters.AddWithValue("@Cname", courseName);
                command.Parameters.AddWithValue("@Tno", courseTeacherID);
                command.Parameters.AddWithValue("@Tname", courseTeacherName);
                command.Parameters.AddWithValue("@introduce", courseIntroduce);
                command.Parameters.AddWithValue("@sum", 0);

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("授课信息也已成功添加！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("授课信息添加失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // 添加课程
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请您先选择课程所属学院！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string college = getCollege(Form1.Tno);

            if(textBox1.Text != college)
            {
                MessageBox.Show($"您只能添加 {college} 的课程！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dao dao = new Dao();
            try
            {
                dao.Connection();

                // 使用参数化查询
                string sql = "INSERT INTO course (Cno, Cname, Cteacher_id, Cteacher_name,Credit, Chour, Cintroduce) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6,@Value7)";

                // 创建命令对象
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());

                // 为参数赋值
                command.Parameters.AddWithValue("@Value1", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                command.Parameters.AddWithValue("@Value2", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                command.Parameters.AddWithValue("@Value3", dataGridView1.CurrentRow.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@Value4", dataGridView1.CurrentRow.Cells[3].Value.ToString());
                command.Parameters.AddWithValue("@Value5", dataGridView1.CurrentRow.Cells[4].Value.ToString());
                command.Parameters.AddWithValue("@Value6", dataGridView1.CurrentRow.Cells[5].Value.ToString());
                command.Parameters.AddWithValue("@Value7", dataGridView1.CurrentRow.Cells[6].Value.ToString());

                // 执行插入操作
                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("添加成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitCourseInfo();
                    LoadCourseTable();
                    saveTeachInfo();
                }
                else
                {
                    MessageBox.Show("添加失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void button2_Click(object sender, EventArgs e)
        {//删除课程
            if (textBox1.Text == "")
            {
                MessageBox.Show("请您先选择课程所属学院！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(textBox1.Text != getCollege(Form1.Tno))
            {
                MessageBox.Show($"您只能删除 {getCollege(Form1.Tno)} 的课程！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Dao dao = new Dao();
                dao.Connection();
                string sql = $"delete from course where Cno='{dataGridView1.CurrentRow.Cells[0].Value.ToString()}'";

                if (MessageBox.Show("确定要删除这门课程吗？", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dao.Excute(sql) > 0)
                    {
                        MessageBox.Show("删除成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCourseTable();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                dao.DaoClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {//显示课程表信息
            string sql = $"select * from course";
            Dao dao = new Dao();
            dao.Connection();
            MySqlDataReader reader = dao.read(sql);
            dataGridView1.Rows.Clear();
            while (reader.Read())
            {
               dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                   reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }
            reader.Close();
            dao.DaoClose();
        }

        private void button7_Click(object sender, EventArgs e)
        {//提示
            MessageBox.Show("教师可以通过网格中显示的课程来添加课程到课程表中，查询功能可以根据课程所属学院来查询相关课程，学校设置有：计算机学院，机械学院，理学院，生命科学与技术学院  " +
                "注意：输入所属学院时必须输入学院的全名！ 课程表按钮可以显示课程表中的所有课程，请教师在查看课程表中的课程后再执行删除操作！ " +
                "课程简介按钮可以显示当前所选课程的课程简介.", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string courseOrder;
        public static string courseName;
        public static string courseTeacherID;
        public static string courseTeacherName;
        public static double courseCredit;
        public static string courseHour;
        public static string courseIntroduce;

        private string flag;//异常处理标志

        private void InitCourseInfo()
        {
            try
            {
                courseOrder = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                courseName = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                courseTeacherID = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                courseTeacherName = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                courseCredit = double.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                courseHour = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                courseIntroduce = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag="异常"; 
            }
            
        }

        //修改课程信息
        private void button8_Click(object sender, EventArgs e)
        {
            InitCourseInfo();
            if (flag == "异常")
            {
                return;
            }

            if(textBox1.Text != getCollege(Form1.Tno))
            {
                MessageBox.Show($"您只能修改 {getCollege(Form1.Tno)} 的课程！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormUpdateCourse formUpdateCourse = new FormUpdateCourse();
            formUpdateCourse.Show();
        }
    }
}

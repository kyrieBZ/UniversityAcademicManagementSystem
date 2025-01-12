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
    public partial class FormUpdate_DeleteStudent : Form
    {
        private System.Windows.Forms.Timer statusCheckTimer;

        private string tno = Form1.Tno;
        public FormUpdate_DeleteStudent()
        {
            InitializeComponent();
            if (FormTeacher.StudentInfoType == "修改")
            {
                button2.Enabled = false;
            }
            else if(FormTeacher.StudentInfoType == "删除")
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
            if (FormUpdateStudent.status == "成功")
            {
                LoadCollegeStudent(tno);
                //statusCheckTimer.Stop();
                FormUpdateStudent.status = "";
            }
        }

        //退出
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //提示
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提示：可以通过学生姓名来查询对应的学生来进行相关操作（修改，删除)","消息",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        //删除学生
        private void button2_Click(object sender, EventArgs e)
        {
                // 获取选中行的学号（第一列）
                string sno = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                // 删除确认提示
                DialogResult result = MessageBox.Show($"确认要删除学号为 {sno} 的学生吗？",
                    "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Dao dao = new Dao();
                    try
                    {
                        dao.Connection();

                        // SQL 删除语句
                        string sql = $"DELETE FROM student WHERE Sno = '{sno}'";

                        // 执行删除操作
                        if (dao.Excute(sql) > 0)
                        {
                            MessageBox.Show("删除成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCollegeStudent(tno);
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

     

        private void LoadCollegeStudent(string teacherNo)
        {
            Dao dao = new Dao();
            dao.Connection();

            try
            {
                // 1. 根据教师编号 (Tno) 查找学院编号 (COno)
                string getCollegeIdSql = $"SELECT COno FROM teacher WHERE Tno = @Tno";
                string collegeId = null;

                using (var command = new MySqlCommand(getCollegeIdSql, dao.Connection()))
                {
                    command.Parameters.AddWithValue("@Tno", teacherNo);
                    collegeId = command.ExecuteScalar()?.ToString();
                }

                // 2. 根据学院编号 (COno) 查找所有专业编号 (Mno)
                if (collegeId != null)
                {
                    string getMajorIdsSql = $"SELECT Mno FROM major WHERE Mcono = @COno";
                    List<string> majorIds = new List<string>();

                    using (var command = new MySqlCommand(getMajorIdsSql, dao.Connection()))
                    {
                        command.Parameters.AddWithValue("@COno", collegeId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                majorIds.Add(reader["Mno"].ToString());
                            }
                        }
                    }

                    // 3. 根据专业编号 (Mno) 查找学生信息
                    if (majorIds.Count > 0)
                    {
                        // 生成查询字符串，使用 IN 子句
                        string majorIdsStr = string.Join(",", majorIds.Select(m => $"'{m}'"));
                        string getStudentsSql = $"SELECT * FROM student WHERE Mno IN ({majorIdsStr})";

                        MySqlDataReader studentReader = dao.read(getStudentsSql);
                        dataGridView1.Rows.Clear();
                        while (studentReader.Read())
                        {
                            dataGridView1.Rows.Add(studentReader[0].ToString(), studentReader[1].ToString(), studentReader[2].ToString(),
                                studentReader[3].ToString(), studentReader[4].ToString(), studentReader[5].ToString(),
                                studentReader[6].ToString());
                        }
                        studentReader.Close();
                    }
                }
            }
            catch(Exception ex)
            { 
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dao.DaoClose();
            }
        }


        private void FormUpdate_DeleteStudent_Load(object sender, EventArgs e)
        {
            //LoadStudent();
            LoadCollegeStudent(tno);
        }

        //点击单元格触发事件
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("选择了无效数据！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                if (FormTeacher.StudentInfoType == "修改")
                {
                    button1.Enabled = true;
                }
                if (FormTeacher.StudentInfoType == "删除")
                {
                    button2.Enabled = true;
                }
            }
        }

        public static string studentOrder;
        public static int studentID;
        public static string studentPassword;
        public static string studentName;
        public static string studentGender;
        public static string studentAge;
        public static string studentMajorID;

        //获取学生信息
        private void InitStudentInfo()
        {
            try
            {
                studentOrder = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                studentID = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                studentPassword = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                studentName = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                studentGender = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                studentAge = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                studentMajorID = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //修改学生信息
        private void button1_Click(object sender, EventArgs e)
        {
            InitStudentInfo();
            FormUpdateStudent formUpdateStudent = new FormUpdateStudent();
            formUpdateStudent.Show();
        }

        //根据学生姓名查询学生信息
        private void button4_Click(object sender, EventArgs e)
        {
            // 创建数据库连接
            Dao dao = new Dao();
            try
            {
                dao.Connection();

                // 1. 根据教师编号 (Tno) 查找学院编号 (COno)
                string collegeId = null;
                string getCollegeIdSql = "SELECT COno FROM teacher WHERE Tno = @Tno";

                using (var command = new MySqlCommand(getCollegeIdSql, dao.Connection()))
                {
                    command.Parameters.AddWithValue("@Tno", tno);
                    collegeId = command.ExecuteScalar()?.ToString();
                }

                // 2. 根据学院编号 (COno) 查找所有专业编号 (Mno)
                List<string> majorIds = new List<string>();
                if (collegeId != null)
                {
                    string getMajorIdsSql = "SELECT Mno FROM major WHERE Mcono = @COno";

                    using (var command = new MySqlCommand(getMajorIdsSql, dao.Connection()))
                    {
                        command.Parameters.AddWithValue("@COno", collegeId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                majorIds.Add(reader["Mno"].ToString());
                            }
                        }
                    }
                }

                // 3. 根据专业编号 (Mno) 和姓名进行查询
                if (majorIds.Count > 0)
                {
                    // 生成查询字符串，使用 IN 子句
                    string majorIdsStr = string.Join(",", majorIds.Select(m => $"'{m}'"));
                    string sql = $"SELECT * FROM student WHERE Mno IN ({majorIdsStr}) AND Sname LIKE @Name";

                    using (MySqlCommand command = new MySqlCommand(sql, dao.Connection()))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{textBox1.Text}%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // 清空 DataGridView 内容
                            dataGridView1.Rows.Clear();
                            while (reader.Read()) // 逐行读取数据
                            {
                                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                                    reader[3].ToString(), reader[4].ToString(), reader[5].ToString(),
                                    reader[6].ToString());
                            }
                        }
                    }
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

    }
}

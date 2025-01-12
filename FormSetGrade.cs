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
    public partial class FormSetGrade : Form
    {
        public FormSetGrade()
        {
            InitializeComponent();
        }

        //退出
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //窗体加载时将所有需要打分的课程成绩信息显示在表格中
        private void FormSetGrade_Load(object sender, EventArgs e)
        {
            // 清空已有的内容
            dataGridView1.Rows.Clear();

            string teacherId = Form1.Tno; // 获取教师编号

            // SQL 查询语句，增加了grade字段
            string sql = @"
        SELECT 
            cou.Cname AS 课程名,
            s.Sno AS 学号,
            stu.Sname AS 姓名,
            s.grade AS 成绩
        FROM 
            sc s
        JOIN 
            student stu ON s.Sno = stu.Sno
        JOIN 
            sk cou ON s.Cno = cou.Cno
        WHERE 
            cou.Tno = @teacherId"; // 根据教师编号进行查询

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@teacherId", teacherId); // 使用参数化查询

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 获取成绩的值，如果为空则显示课程名、学号和姓名
                            var grade = reader["成绩"] != DBNull.Value ? reader["成绩"] : null;

                            if (grade == null) // 只有当成绩为空时才添加记录
                            {
                                // 创建新的行
                                int rowIndex = dataGridView1.Rows.Add();

                                // 获取数据并填充到数据行
                                dataGridView1.Rows[rowIndex].Cells[0].Value = reader["课程名"].ToString(); // 转换为字符串
                                dataGridView1.Rows[rowIndex].Cells[1].Value = reader["学号"].ToString(); // 转换为字符串
                                dataGridView1.Rows[rowIndex].Cells[2].Value = reader["姓名"].ToString(); // 转换为字符串
                            }
                        }
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //按照课程名来查询记录
        private void button2_Click(object sender, EventArgs e)
        {
            // 清空现有行
            dataGridView1.Rows.Clear();

            // 获取输入的课程名和教师编号
            string courseName = textBox1.Text.Trim(); // 假设有一个文本框用于输入课程名
            string teacherId = Form1.Tno;

            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(teacherId))
            {
                MessageBox.Show("课程名不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL 查询语句，增加了grade字段
            string sql = @"
                SELECT 
                    cou.Cname AS 课程名,
                    s.Sno AS 学号,
                    stu.Sname AS 姓名,
                    s.grade AS 成绩
                FROM 
                    sc s
                JOIN 
                    student stu ON s.Sno = stu.Sno
                JOIN 
                    course cou ON s.Cno = cou.Cno
                JOIN 
                    sk k ON cou.Cno = k.Cno
                WHERE 
                    cou.Cname LIKE @courseName AND k.Tno = @teacherId"; // 根据课程名和教师编号进行查询

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                using (MySqlCommand cmd = new MySqlCommand(sql, dao.Connection()))
                {
                    cmd.Parameters.AddWithValue("@courseName", "%" + courseName + "%"); // 模糊匹配课程名
                    cmd.Parameters.AddWithValue("@teacherId", teacherId); // 精确匹配教师编号

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 获取成绩的值，如果为空则显示课程名、学号和姓名
                            var grade = reader["成绩"] != DBNull.Value ? reader["成绩"] : null;

                            if (grade == null) // 只有当成绩为空时才添加记录
                            {
                                // 创建新的行并填充数据
                                int rowIndex = dataGridView1.Rows.Add();
                                dataGridView1.Rows[rowIndex].Cells[0].Value = reader["课程名"].ToString();
                                dataGridView1.Rows[rowIndex].Cells[1].Value = reader["学号"].ToString();
                                dataGridView1.Rows[rowIndex].Cells[2].Value = reader["姓名"].ToString();
                            }
                        }
                    }
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //设置成绩
        private void button3_Click(object sender, EventArgs e)
        {
            // SQL 查询语句用于获取课程号
            string courseIdSql = "SELECT Cno FROM course WHERE Cname = @CourseName";

            // SQL 更新语句
            string updateSql = "UPDATE sc SET grade = @Grade WHERE Sno = @Sno AND Cno = @Cno";

            try
            {
                // 建立数据库连接
                Dao dao = new Dao();
                dao.Connection();

                using (var transaction = dao.Connection().BeginTransaction())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // 检查行是否有效，避免空行
                        if (row.IsNewRow) continue;

                        // 获取课程名、学号和成绩
                        string courseName = row.Cells[0].Value?.ToString(); // 课程名
                        string studentId = row.Cells[1].Value?.ToString(); // 学号
                        string gradeStr = row.Cells[3].Value?.ToString(); // 成绩

                        // 检查输入有效性
                        if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(gradeStr))
                        {
                            MessageBox.Show("课程名、学号或成绩不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        char last_name = Form1.name[0];

                        if (int.Parse(gradeStr) < 60)
                        {
                            MessageBox.Show($"学生想要过个好假期，老师就尽量捞捞吧，谢谢老师. {last_name} 老师您最好了！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // 查询课程编号
                        int courseId;
                        using (MySqlCommand courseCmd = new MySqlCommand(courseIdSql, dao.Connection(), transaction)) // 使用事务
                        {
                            courseCmd.Parameters.AddWithValue("@CourseName", courseName);
                            var courseNumber = courseCmd.ExecuteScalar();

                            if (courseNumber != null) // 如果课程存在
                            {
                                courseId = Convert.ToInt32(courseNumber);

                                // 更新成绩到 sc 表
                                using (MySqlCommand updateCmd = new MySqlCommand(updateSql, dao.Connection(), transaction)) // 使用事务
                                {
                                    updateCmd.Parameters.AddWithValue("@Sno", studentId);
                                    updateCmd.Parameters.AddWithValue("@Cno", courseId);
                                    updateCmd.Parameters.AddWithValue("@Grade", Convert.ToDecimal(gradeStr));

                                    updateCmd.ExecuteNonQuery(); // 执行更新
                                }
                            }
                            else
                            {
                                MessageBox.Show($"课程 '{courseName}' 不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    transaction.Commit(); // 提交事务
                }

                dao.DaoClose(); // 关闭数据库连接
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据库时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

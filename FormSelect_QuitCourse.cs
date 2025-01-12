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
    public partial class FormSelect_QuitCourse : Form
    {
        public FormSelect_QuitCourse()
        {
            InitializeComponent();
            InitializeListView();
        }

        private void InitializeListView()
        {
            // 启用绘制模式
            listView1.OwnerDraw = true;

            // 添加事件处理程序
            listView1.DrawColumnHeader += ListView1_DrawColumnHeader;
            listView1.DrawItem += ListView1_DrawItem;
            listView1.DrawSubItem += ListView1_DrawSubItem;

            listView1.View = View.Details;
            listView1.Columns.Add("课程号", 100);
            listView1.Columns.Add("课程名", 100);
            listView1.Columns.Add("授课教师", 100);
            listView1.Columns.Add("学分", 100);
            listView1.Columns.Add("学时", 100);

            
        }

        // 列标题绘制
        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true; // 使用默认绘制
        }

        // 行绘制
        private void ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false; // 行使用默认绘制
        }

        // 子项绘制
        
        private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground(); // 绘制背景

            // 设置水平和垂直居中对齐
            Rectangle bounds = e.Bounds;

            // 计算文本绘制位置
            string text = e.SubItem.Text;
            SizeF textSize = e.Graphics.MeasureString(text, e.SubItem.Font);

            // 检查当前文本是否超出 bounds 的宽度
            if (textSize.Width > bounds.Width)
            {
                // 计算使用省略号后的文本
                string ellipsis = "...";
                float ellipsisWidth = e.Graphics.MeasureString(ellipsis, e.SubItem.Font).Width;

                // 从文本中减去省略号占用的宽度
                float availableWidth = bounds.Width - ellipsisWidth;

                // 循环减少文本的长度直到适合
                while (textSize.Width > availableWidth && text.Length > 0)
                {
                    text = text.Remove(text.Length - 1); // 移除最后一个字符
                    textSize = e.Graphics.MeasureString(text, e.SubItem.Font); // 重新测量
                }

                // 添加省略号
                text += ellipsis;
            }

            // 计算最终文本绘制位置(水平以及垂直居中)
            float textX = bounds.X + (bounds.Width - textSize.Width) / 2; // 水平居中
            float textY = bounds.Y + (bounds.Height - textSize.Height) / 2; // 垂直居中

            e.Graphics.DrawString(text, e.SubItem.Font, Brushes.Black, textX, textY);

            e.DrawFocusRectangle(bounds); // 绘制焦点矩形


        }



        private void LoadCourses()
        {
            listView1.Items.Clear(); // 清空 ListView

            if (comboBox1.Text == "选课")
            {
                string sql = "SELECT Cno, Cname, Cteacher_name,Credit, Chour FROM course"; // 查询课程的 SQL

                Dao dao = new Dao();
                try
                {
                    dao.Connection();
                    MySqlCommand command = new MySqlCommand(sql, dao.Connection());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Cno"].ToString());
                            item.SubItems.Add(reader["Cname"].ToString());
                            item.SubItems.Add(reader["Cteacher_name"].ToString());
                            item.SubItems.Add(reader["Credit"].ToString());
                            item.SubItems.Add(reader["Chour"].ToString());
                            listView1.Items.Add(item);
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
            else if(comboBox1.Text == "退课")
            { 
                // 获取学生的学号
                string studentSno = Form1.Sno; // 假设该学号在 Form1 中设置

                if (!string.IsNullOrEmpty(studentSno))
                {
                    // SQL 查询
                    string sql = @"
                        SELECT course.Cno, course.Cname, course.Cteacher_name,course.Credit ,course.Chour, course.Cintroduce
                        FROM sc
                        INNER JOIN course ON sc.Cno = course.Cno
                        WHERE sc.Sno = @StudentSno";

                    // 创建数据库连接
                    Dao dao = new Dao();
                    try
                    {
                        dao.Connection();

                        // 创建命令对象
                        MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                        command.Parameters.AddWithValue("@StudentSno", studentSno);

                        // 使用 MySqlDataReader 获取查询结果
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                               
                            listView1.Items.Clear(); // 清空 ListView
                            // 逐行读取数据
                            while (reader.Read())
                            {
                                // 获取课程信息
                                string courseId = reader["Cno"].ToString();
                                string courseName = reader["Cname"].ToString();
                                string teacherName = reader["Cteacher_name"].ToString();
                                string credit = reader["Credit"].ToString();
                                string classHours = reader["Chour"].ToString();
                                string courseIntro = reader["Cintroduce"].ToString();

                                listView1.Items.Add(courseId); // 添加课程号
                                listView1.Items[listView1.Items.Count - 1].SubItems.Add(courseName); // 添加课程名
                                listView1.Items[listView1.Items.Count - 1].SubItems.Add(teacherName); // 添加授课教师
                                listView1.Items[listView1.Items.Count - 1].SubItems.Add(credit); // 添加学分
                                listView1.Items[listView1.Items.Count - 1].SubItems.Add(classHours); // 添加学时
                                listView1.Items[listView1.Items.Count - 1].ToolTipText = courseIntro; // 添加课程介绍
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
                    else
                    {
                        MessageBox.Show("学号无效或未输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


            }

            //退出
            private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int max_id;
        private int getMaxID()
        {
            string sql = "SELECT MAX(SCid) FROM sc";
            Dao dao = new Dao();
            dao.Connection();
            try
            {
                MySqlDataReader reader = dao.read(sql);
                if(reader.Read())
                {
                    max_id = reader.IsDBNull(0) ? 0 :reader.GetInt32(0);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return max_id;
        }

        //选课
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一门课程！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(comboBox1.Text == "退课")
            {
                MessageBox.Show("请先选择选课操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string courseId = listView1.SelectedItems[0].SubItems[0].Text; // 获取选中课程的ID
            string studentId = Form1.Sno; // 获取学生的学号

            int max_id=getMaxID();

            string sql = $"INSERT INTO sc (SCid,Sno, Cno) VALUES ('{max_id+1}',@Sno, @Cno)";

            Dao dao = new Dao();
            try
            {
                dao.Connection();
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@Sno", studentId);
                command.Parameters.AddWithValue("@Cno", courseId);

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("选课成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("选课失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //退课
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一门课程！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(comboBox1.Text == "选课")
            {
                MessageBox.Show("请先选择退课操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string courseId = listView1.SelectedItems[0].SubItems[0].Text; // 获取选中课程的ID
            string studentId = Form1.Sno; // 获取学生的学号

            string sql = "DELETE FROM sc WHERE Sno = @Sno AND Cno = @Cno";

            Dao dao = new Dao();
            try
            {
                dao.Connection();
                MySqlCommand command = new MySqlCommand(sql, dao.Connection());
                command.Parameters.AddWithValue("@Sno", studentId);
                command.Parameters.AddWithValue("@Cno", courseId);

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("退课成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCourses();
                }
                else
                {
                    MessageBox.Show("退课失败！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //下拉列表内容变化时触发
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            LoadCourses(); // 加载课程
        }

        //提示
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("提示：可以选择下拉列表中的“选课”或“退课”" +
                "，选课则listview中显示所有可选课程，退课则listview中显示自身已选课程。" +
                "选择课程后点击按钮即可执行相应的操作。Listview可以通过滑动下方的滚轮查看更多的课程信息。" +
                "另外，可以将鼠标放在两列标题中间移动来加大列宽查看单元格中没完全显示的信息","消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

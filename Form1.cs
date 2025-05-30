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
        private string generatedCode; // 生成的验证码
        public Form1()
        {
            InitializeComponent();
            GenerateVerificationCode(); // 初始化验证码
        }

        // 生成验证码方法
        private void GenerateVerificationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            generatedCode = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            label4.Text = generatedCode;
            label4.ForeColor = Color.Blue;
            label4.Font = new Font("Arial", 16, FontStyle.Bold);
        }

        //检测验证码是否正确
        private bool CheckVerificationCode()
        {
            return textBox3.Text.Trim().Equals(generatedCode, StringComparison.OrdinalIgnoreCase);
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
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                MessageBox.Show("请选择用户权限！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

        // 类级成员变量，用于记录失败次数和锁定状态（若需持久化需改用数据库）
        private Dictionary<string, int> failedAttempts = new Dictionary<string, int>();
        private Dictionary<string, DateTime> lockedAccounts = new Dictionary<string, DateTime>();

        private void AdministratorLogin()
        {
            string account = textBox1.Text.Trim();

            // 1. 检查账号是否被锁定
            if (lockedAccounts.ContainsKey(account))
            {
                TimeSpan lockDuration = DateTime.Now - lockedAccounts[account];
                if (lockDuration.TotalMinutes < 5)
                {
                    MessageBox.Show($"账号已锁定，剩余时间：{5 - (int)lockDuration.TotalMinutes}分钟", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lockedAccounts.Remove(account);
                    failedAttempts.Remove(account);
                }
            }

            // 2. 输入非空校验
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("账号或密码存在空项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. 密码位数校验
            if (textBox2.Text.Length < 6)
            {
                MessageBox.Show("密码位数不足6位！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. 验证码校验
            if (!CheckVerificationCode())
            {
                MessageBox.Show("验证码错误！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerateVerificationCode();
                return;
            }

            Dao dao = new Dao();
            MySqlDataReader reader = null;

            try
            {
                // 5. 第一步：查询账号是否存在
                string sqlCheckAccount = "SELECT ID, PassWord, Name FROM Administrator WHERE ID=@ID";
                MySqlCommand cmdCheckAccount = new MySqlCommand(sqlCheckAccount, dao.Connection());
                cmdCheckAccount.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                reader = cmdCheckAccount.ExecuteReader();

                if (reader.Read())
                {
                    // 6. 账号存在，验证密码
                    string storedPassword = reader["PassWord"].ToString();
                    reader.Close(); // 关闭第一次查询的Reader

                    if (textBox2.Text == storedPassword)
                    {
                        // 7. 密码正确，登录成功
                        if (failedAttempts.ContainsKey(account))
                        {
                            failedAttempts.Remove(account);
                        }

                        MessageBox.Show("登录成功！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        id = int.Parse(reader["ID"].ToString());
                        name = reader["Name"].ToString();
                        FormAdministrator form = new FormAdministrator();
                        form.ShowDialog();
                    }
                    else
                    {
                        // 8. 密码错误
                        HandleFailedLogin(account, "密码错误");
                    }
                }
                else
                {
                    // 9. 账号不存在
                    HandleFailedLogin(account, "账号错误");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                dao.DaoClose();
            }
        }

        // 处理登录失败的公共方法
        private void HandleFailedLogin(string account, string errorType)
        {
            // 记录失败次数
            if (!failedAttempts.ContainsKey(account))
            {
                failedAttempts.Add(account, 1);
            }
            else
            {
                failedAttempts[account]++;
            }

            // 提示具体错误类型
            if (errorType == "账号错误")
            {
                MessageBox.Show("账号不存在！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (errorType == "密码错误")
            {
                int remainingAttempts = 5 - failedAttempts[account];
                MessageBox.Show($"密码错误！剩余尝试次数：{remainingAttempts}", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 达到5次失败则锁定账号
            if (failedAttempts[account] >= 5)
            {
                lockedAccounts.Add(account, DateTime.Now);
                MessageBox.Show("账号已锁定，请5分钟后重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TeacherLogin()//教师登录方法
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("账号或密码存在空白项！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckVerificationCode())
            {
                MessageBox.Show("验证码错误！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerateVerificationCode();
                return;
            }

            Dao dao = new Dao();
            string sql="";

            try
            {
                sql = $"select ID,PassWord,IsLeader,Tname,Tno from teacher where ID='{int.Parse(textBox1.Text)}' and PassWord='{textBox2.Text}'";
            }
            catch(Exception ex)
            {
                MessageBox.Show($"发生了错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MySqlDataReader reader = dao.read(sql);
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

            if (!CheckVerificationCode())
            {
                MessageBox.Show("验证码错误！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerateVerificationCode();
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
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("登录信息存在空项，请检查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if(radioButton1.Checked==false && radioButton2.Checked==false && radioButton3.Checked == false)
            {
                MessageBox.Show("请选择用户权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

        //点击验证码刷新
        private void label4_Click(object sender, EventArgs e)
        {
            GenerateVerificationCode(); // 点击验证码刷新
        }
    }
}

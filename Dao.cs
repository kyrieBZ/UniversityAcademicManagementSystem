using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace UniversityAcademicManagementSystem
{
    class Dao
    {
        private string sqlConnect = "server=localhost;database=university_academic_management_system;user=root;password=23456789";
        MySqlConnection sc;//数据库连接对象
        private Dictionary<string, object> parameters = new Dictionary<string, object>(); // 存储参数


        public MySqlConnection Connection()
        {
            try
            {
                if (sc == null || sc.State != ConnectionState.Open)
                {//数据库对象为空或者数据库连接已关闭
                    sc=new MySqlConnection(sqlConnect);
                    sc.Open();//打开数据库连接
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine("数据库连接失败！"+ex.Message);
                throw;
            }
            return sc;//返回数据库连接对象
        }

        public MySqlCommand Command(string sql)//执行传入的SQL语句
        {
            MySqlCommand com= new MySqlCommand(sql,Connection());
            // 添加参数到命令对象
            foreach (var param in parameters)
            {
                com.Parameters.AddWithValue(param.Key, param.Value);
            }
            return com;
        }

        public void AddParameter(string parameterName, object value) // 添加参数
        {
            if (parameters.ContainsKey(parameterName))
            {
                parameters[parameterName] = value; // 更新已有参数的值
            }
            else
            {
                parameters.Add(parameterName, value); // 添加新参数
            }
        }

        public int Excute(string sql)//用于更新操作，返回受影响的记录的条数
        {
            return Command(sql).ExecuteNonQuery();
        }

        public MySqlDataReader read(string sql)//读取数据
        {
            return Command(sql).ExecuteReader();
        }

        public void ClearParameters() // 可选择添加一个方法来清空参数
        {
            parameters.Clear();
        }

        public void DaoClose()
        {
            sc.Close();//关闭数据库连接
        }

    }
}

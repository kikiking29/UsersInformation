using MySql.Data.MySqlClient;
using System.Data;
using userInformation.Model;

namespace userInformation.ConnecDb
{
    public class connecDb
    {
        string connectionstring = "Server=localhost;Database=userinformation;Uid=root;Pwd=1234;";
        public string connectDb() {
            string connectionString = "Server=localhost;Database=userinformation;Uid=root;Pwd=1234;";
            return connectionString;
        }

        public String Setdata(string Sql)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            connection.Open();
            MySqlCommand comm = new MySqlCommand(Sql,connection);
            String result = Convert.ToString(comm.ExecuteNonQuery());
            connection.Close();
            return result;
        }

        public DataSet Selectdata(string Sql)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();
            connection.Open();
            MySqlDataAdapter dap = new MySqlDataAdapter(Sql,connection);
            dap.Fill(ds);
            connection.Close();
            return ds;
        }

        public PasswordModels ChackPassword(PasswordModels data)
        {
            PasswordModels passwrd = new PasswordModels(); 
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();
            connection.Open();
            string Sql = "SELECT usersId FROM users where username='"+data.username+"'AND passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1('"+data.old_password+"')))));";
            MySqlDataAdapter dap = new MySqlDataAdapter(Sql, connection);
            dap.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                passwrd = new PasswordModels()
                {
                    usersId = int.Parse(dr["usersId"].ToString()),
                };
            }
            connection.Close();
            return passwrd;
        }

        public RoleModle Getrole (int id)
        {
            PrivileageModels pau = new PrivileageModels();
            RoleModle status = new RoleModle();
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();
            connection.Open();
            string Sql = "SELECT * FROM privileage WHERE usersId ='" +id+"';";
            MySqlDataAdapter dap = new MySqlDataAdapter(Sql, connection);
            dap.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                pau = new PrivileageModels()
                {
                    privileageId = int.Parse(dr["privileageId"].ToString()),
                    usersId = int.Parse(dr["usersId"].ToString()),
                    canread = dr["canread"].ToString(),
                    caninsert = dr["caninsert"].ToString(),
                    canupdate = dr["canupdate"].ToString(),
                    candelete = dr["candelete"].ToString(),
                    candrop = dr["candrop"].ToString()
                };
            }
            if (pau.candrop != "0" )
            {
                status.status = "Admin";
            }
            else
            {
                status.status = "User";
            }

            connection.Close();
            return status;
        }
    }
}

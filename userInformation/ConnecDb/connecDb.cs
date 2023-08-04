using MySql.Data.MySqlClient;
using System.Data;
using userInformation.Entities;
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

        public DataSet Selectitem(string Sql)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();
            connection.Open();
            if (Sql != null)
            {
                var db = "SELECT * FROM users WHERE TRUE";
                MySqlDataAdapter dap = new MySqlDataAdapter(db + Sql+ ";", connection);
                dap.Fill(ds);
            }
            connection.Close();
            return ds;
        }


        public int CheckIduser(UserDto data)
        {
            int id = 0;
            PasswordModels passwrd = new PasswordModels(); 
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();
            connection.Open();
            string Sql = "SELECT usersId FROM users where username='"+data.Username+ "'AND passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1('"+data.Password+ "')))));";
            MySqlDataAdapter dap = new MySqlDataAdapter(Sql, connection);
            dap.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
              
                    id = int.Parse(dr["usersId"].ToString());
                
            }
            connection.Close();
            return id;
        }

        public string Getrole (int id)
        {
            PrivileageModels pau = new PrivileageModels();
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
            connection.Close();
            if (pau.candrop != "0" )
            {
                return "SuperAdmin";
            }
            else if (pau.candelete != "0")
            {
                return "Admin";
            }
            else if (pau.canread != "0" && pau.caninsert != "0" && pau.canupdate != "0")
            {
                return "User";
            }
            else
            {
                return "Geust";
            }
          


            
            
        }
    }
}

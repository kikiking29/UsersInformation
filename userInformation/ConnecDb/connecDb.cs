using MySql.Data.MySqlClient;
using System.Data;

namespace userInformation.ConnecDb
{
    public class connecDb
    {
        string connectionstring = "Server=localhost;Database=userinformaation;Uid=root;Pwd=1234;";
        public string connectDb() {
            string connectionString = "Server=localhost;Database=userinformaation;Uid=root;Pwd=1234;";
            return connectionString;
        }

        public String Setdata(string Sql)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            //DataSet ds = new DataSet();
            connection.Open();

            //insert updat delete
            MySqlCommand comm = new MySqlCommand(Sql, connection);

            String result = Convert.ToString(comm.ExecuteNonQuery());


            connection.Close();


            return result;
        }

        public DataSet Selectdata(string Sql)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();


            connection.Open();

            MySqlDataAdapter dap = new MySqlDataAdapter(Sql, connection);

            //select
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

            //select
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
    }
}

using MySql.Data.MySqlClient;
using System.Data;

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

        public DataSet GetOldPassword(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            DataSet ds = new DataSet();
            connection.Open();
            string sql = "SELECT password FROM users WHERE usersId='"+id+"';";
            

            MySqlDataAdapter dap = new MySqlDataAdapter(sql, connection);

            //select
            dap.Fill(ds);

            connection.Close();
            return ds;
        }
    }
}

using MySql.Data.MySqlClient;
using System.Data;

namespace userInformation.ConnecDb
{
    public class connecDb
    {
        string connectionstring = "Server=localhost;Database=userinformaation;Uid=root;Pwd=1234;";
        public String connectDb() {
            string connectionString = "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
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


    }
}

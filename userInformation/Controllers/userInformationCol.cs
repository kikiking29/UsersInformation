using Microsoft.AspNetCore.Mvc;
using userInformation;
using userInformation.ConnecDb;

using System;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace userInformation.Controllers
{ 
    public class userInformationCol : ControllerBase
    {
        connecDb conn = new connecDb();
        public class myParam
        {
            public string name;
            public object value;
        }

        [HttpGet]
        [Route("Usersinformation")]
        public List<UsersinforModels> GetAll(){

            List<UsersinforModels> users = new List<UsersinforModels>();
            try
            {
                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM users;");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UsersinforModels user = new UsersinforModels()
                    {
                        usersId = int.Parse(dr["usersId"].ToString()),
                        username = dr["username"].ToString(),
                        password = (byte[])dr["password"],
                        name = dr["name"].ToString(),
                        status = dr["status"].ToString()

                    };
                    users.Add(user);
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return users ;
        }

        [HttpGet]
        [Route("Usersinformation/{id}")]
        public UsersinforModels GetbyId(int id)
        {
            List<myParam> param = new List<myParam>();
            UsersinforModels user = new UsersinforModels();
            myParam p = new myParam();
            
            try
            {
                
                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM users WHERE usersId='"+id+"';");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    user = new UsersinforModels()
                    {
                        usersId = int.Parse(dr["usersId"].ToString()),
                        username = dr["username"].ToString(),
                        password = (byte[])dr["password"],
                        name = dr["name"].ToString(),
                        status = dr["status"].ToString()

                    };
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        [HttpGet]
        [Route("Usersinformation/privileage")]
        public UsersinforAndPrivileageModels GetAllPrivileage()
        {

            UsersinforAndPrivileageModels uap = new UsersinforAndPrivileageModels();
            myParam p = new myParam();

            try
            {

                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM users LEFT JOIN privileage ON privileage.usersId = users.usersId;");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    uap = new UsersinforAndPrivileageModels()
                    {
                        u_usersId = int.Parse(dr["usersId"].ToString()),
                        username = dr["username"].ToString(),
                        password = (byte[])dr["password"],
                        name = dr["name"].ToString(),
                        status = dr["status"].ToString(),
                        privileageId = int.Parse(dr["privileageId"].ToString()),
                        p_usersId = int.Parse(dr["usersId"].ToString()),
                        canread = dr["canread"].ToString(),
                        caninsert = dr["caninsert"].ToString(),
                        canupdate = dr["canupdate"].ToString(),
                        candelete = dr["candelete"].ToString(),
                        candrop = dr["candrop"].ToString(),
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return uap;
        }

        [HttpPost]
        [Route("UsersInformation")]
        public NewUsersinforModels InsertImage(NewUsersinforModels data)
        {
            try
            {
                List<myParam> param = new List<myParam>();
                myParam p = new myParam();
                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "INSERT into users set username=@username,password=@password,name=@name,status=@status;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@username", data.username);
                comm.Parameters.AddWithValue("@password", data.password);
                //Console.WriteLine(data.image.Length.ToString());
                comm.Parameters.AddWithValue("@name", data.name);
                comm.Parameters.AddWithValue("@status", data.status);
                //cc.Setdata(sql, comm);
                comm.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new NewUsersinforModels
            {
                username = data.username,
                password = data.password,
                name = data.name,
                status = data.status,
            };
        }

        [HttpPut]
        [Route("UsersInformation/{id}")]
        public UsersinforModels Update(UsersinforModels data)
        {
            UsersinforModels user = new UsersinforModels();
            connecDb conn = new connecDb();
            try
            {

                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "UPDATE users SET username=@username,password=@password,name=@name,status=@status  WHERE usersId=@usersId ;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@usersId", data.usersId);
                comm.Parameters.AddWithValue("@username", data.username);
                comm.Parameters.AddWithValue("@password", data.password);
                //Console.WriteLine(data.image.Length.ToString());
                comm.Parameters.AddWithValue("@name", data.name);
                comm.Parameters.AddWithValue("@status", data.status);
                //cc.Setdata(sql);
                comm.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return new UsersinforModels
            {
                usersId = data.usersId,
                username = data.username,
                password = data.password,
                name = data.name,
                status = data.status
            };
        }


        [HttpPut]
        [Route("UsersInformation/{id}/password")]
        public PasswordModels UpdatetoPassword(PasswordModels data)
        {
            PasswordModels user = new PasswordModels();
            connecDb conn = new connecDb();
            try
            {

                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "UPDATE users SET password=@password  WHERE usersId=@usersId ;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@usersId", data.usersId);
                comm.Parameters.AddWithValue("@password", data.password);
                comm.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return new PasswordModels
            {
                password = data.password,
            };
        }

        [HttpDelete]
        [Route("UsersInformation/{id}")]
        public void Delete(int id)
        {
            try
            {

                string sql = "DELETE FROM users WHERE usersId ='" + id + "'  ;";
                conn.Setdata(sql);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }


        }
    }
}
using Microsoft.AspNetCore.Mvc;
using userInformation.ConnecDb;

using System;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using userInformation.Model;
using Microsoft.AspNetCore.Authorization;
using userInformation.Entities;
//using userInformation.Authorization;
//using AuthorizeAttribute = userInformation.Authorization.AuthorizeAttribute;

namespace userInformation.Controllers
{
    
    public class userInformationCol : ControllerBase
    {
        connecDb conn = new connecDb();
     

        //[Authorize(Role.Admin)]
        [HttpGet]
        [Route("Usersinformation")]
        public List<UsersinforModels> Getusersdataall(){

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
                        password = dr["passwrd"].ToString(),
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

        //[Authorize(Role.Admin)]
        [HttpGet]
        [Route("Usersinformation/{id}")]
        public UsersinforModels Getbyusersid(int id)
        {
            UsersinforModels user = new UsersinforModels();
            
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
                        password = dr["passwrd"].ToString(),
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


        //[Authorize(Role.Admin, Role.User)]
        [HttpPost]
        [Route("Usersinformation/item/")]
        public List<Selectwithpagging> Getbyparamforselectwithpagging(Selectwithpagging data)
        {
            List<Selectwithpagging> users = new List<Selectwithpagging>();

            try
            {

                DataSet ds = new DataSet();
                var sql = "SELECT * FROM users WHERE TRUE";

                if (data != null)
                {
                    if(data.username != null)
                    {
                        var un = " AND username like '"+ data.username +"%' ";
                        sql = sql+ un;
                    }
                    if (data.name != null)
                    {
                        var n = " AND name like '" + data.name + "%' ";
                        sql = sql + n;
                    }
                    if (data.status != null)
                    {
                        var s = " AND status like '" + data.status + "%' ";
                        sql = sql +  s;
                    }
                }
                sql = sql + ";";
                ds = conn.Selectdata(sql);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Selectwithpagging user = new Selectwithpagging()
                    {
                        username = dr["username"].ToString(),
                        name = dr["name"].ToString(),
                        status = dr["status"].ToString()

                    };
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return users;
        }

        //[Authorize(Role.Admin)]
        [HttpGet]
        [Route("Usersinformation/privileage")]
        public List<UsersinforAndPrivileageModels> Getalluserandprivileage()
        {
            List<UsersinforAndPrivileageModels> uaps = new List<UsersinforAndPrivileageModels>();

            try
            {

                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM users  JOIN privileage ON privileage.usersId = users.usersId;");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UsersinforAndPrivileageModels uap = new UsersinforAndPrivileageModels()
                    {
                        u_usersId = int.Parse(dr["usersId"].ToString()),
                        username = dr["username"].ToString(),
                        password = dr["passwrd"].ToString(),
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
                    uaps.Add(uap);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return uaps;
        }

        //[Authorize(Role.Admin, Role.User)]
        [HttpPost]
        [Route("UsersInformation")]
        public NewUsersinforModels Registerusers(NewUsersinforModels data)
        {
            try
            {
               
                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "INSERT into users set username=@username,passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1(@password))))),name=@name,status=@status;";
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


        //[Authorize(Role.Admin, Role.User)]
        [HttpPut]
        [Route("UsersInformation/{id}")]
        public UsersinforModels Updateusers(UsersinforModels data)
        {
            UsersinforModels user = new UsersinforModels();
            connecDb conn = new connecDb();
            try
            {
                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "UPDATE users SET username=@username,passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1(@password))))),name=@name,status=@status  WHERE usersId=@usersId ;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@usersId", data.usersId);
                comm.Parameters.AddWithValue("@username", data.username);
                comm.Parameters.AddWithValue("@password", data.password);
                comm.Parameters.AddWithValue("@name", data.name);
                comm.Parameters.AddWithValue("@status", data.status);
                comm.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex){Console.WriteLine(ex.Message);}

            return new UsersinforModels
            {
                usersId = data.usersId,
                username = data.username,
                password = data.password,
                name = data.name,
                status = data.status
            };
        }

        //[Authorize(Role.Admin, Role.User)]
        [HttpPut]
        [Route("UsersInformation/username/password")]
        public PasswordModels Updatepassword(PasswordModels data)
        {
            PasswordModels pass = new PasswordModels();
            connecDb conn = new connecDb();
            try
            {
                pass = conn.ChackPassword(data);
                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "UPDATE users SET passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1(@password)))))  WHERE usersId='"+pass.usersId+"' AND passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1('"+data.old_password+"'))))) ;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                if(data.recheck_password != data.password)
                {
                    return new PasswordModels{ password = "Passwords are not the same." };

                }
                comm.Parameters.AddWithValue("@password", data.password);
                comm.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex){Console.WriteLine(ex.Message);}
            return new PasswordModels { usersId=pass.usersId, password = data.password };
        }

        //[Authorize(Role.Admin)]
        [HttpPut]
        [Route("UsersInformation/status/Active/{id}")]
        public void upstatusActive(int id)
        {
            try
            {

                string sql = "UPDATE users SET status='Active' WHERE usersId='" + id + "';";

                conn.Setdata(sql);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }

        //[Authorize(Role.Admin)]
        [HttpPut]
        [Route("UsersInformation/status/Inactive/{id}")]
        public void upstatusInactive(int id)
        {
         
            try
            {

                string sql = "UPDATE users SET status='Inactive' WHERE usersId='" + id + "';";

                conn.Setdata(sql);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }

        //[Authorize(Role.Admin)]
        [HttpDelete]
        [Route("UsersInformation/{id}")]
        public void Deleteusers(int id)
        {
            try
            {
                string sql = "UPDATE users SET status='DELETE' WHERE usersId='"+id+"';";
                conn.Setdata(sql);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }



    }
}
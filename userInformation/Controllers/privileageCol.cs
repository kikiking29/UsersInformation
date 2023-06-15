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
    public class privileageCol : ControllerBase
    {
        connecDb conn = new connecDb();
        public class myParam
        {
            public string name;
            public object value;
        }

        [HttpGet]
        [Route("Privileage")]
        public List<PrivileageModels> GetAll(){

            List<PrivileageModels> privileages = new List<PrivileageModels>();
            try
            {
                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM privileage;");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PrivileageModels privileage = new PrivileageModels()
                    {
                        privileageId = int.Parse(dr["privileageId"].ToString()),
                        usersId = int.Parse(dr["usersId"].ToString()),
                        canread = dr["canread"].ToString(),
                        caninsert = dr["caninsert"].ToString(),
                        canupdate = dr["canupdate"].ToString(),
                        candelete = dr["candelete"].ToString(),
                        candrop = dr["candrop"].ToString()
                    };
                    privileages.Add(privileage);
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return privileages;
        }

        [HttpGet]
        [Route("Privileage/{id}")]
        public PrivileageModels GetbyId(int id)
        {
            PrivileageModels privileage = new PrivileageModels();
            myParam p = new myParam();
            
            try
            {
                
                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM privileage WHERE privileageId='" + id+"';");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    privileage = new PrivileageModels()
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
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return privileage;
        }

        [HttpGet]
        [Route("privileage/Usersinformation")]
        public UsersinforAndPrivileageModels GetAllPrivileage()
        {

            UsersinforAndPrivileageModels pau = new UsersinforAndPrivileageModels();
            myParam p = new myParam();

            try
            {

                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM  privileage LEFT JOIN  users ON privileage.usersId = users.usersId;");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    pau = new UsersinforAndPrivileageModels()
                    {
                        
                        privileageId = int.Parse(dr["privileageId"].ToString()),
                        p_usersId = int.Parse(dr["usersId"].ToString()),
                        canread = dr["canread"].ToString(),
                        caninsert = dr["caninsert"].ToString(),
                        canupdate = dr["canupdate"].ToString(),
                        candelete = dr["candelete"].ToString(),
                        candrop = dr["candrop"].ToString(),
                        u_usersId = int.Parse(dr["usersId"].ToString()),
                        username = dr["username"].ToString(),
                        password = (byte[])dr["passwrd"],
                        name = dr["name"].ToString(),
                        status = dr["status"].ToString(),
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pau;
        }

        //[HttpPost]
        //[Route("UsersInformation")]
        //public NewUsersinforModels InsertImage(NewUsersinforModels data)
        //{
        //    try
        //    {
        //        List<myParam> param = new List<myParam>();
        //        myParam p = new myParam();
        //        MySqlConnection connection = new MySqlConnection(conn.connectDb());
        //        connection.Open();
        //        string sql = "INSERT into users set username=@username,passwrd=CONCAT('*', UPPER(SHA1(UNHEX(SHA1(@password))))),name=@name,status=@status;";
        //        MySqlCommand comm = new MySqlCommand(sql, connection);
        //        comm.Parameters.AddWithValue("@username", data.username);
        //        comm.Parameters.AddWithValue("@password", data.password);
        //        //Console.WriteLine(data.image.Length.ToString());
        //        comm.Parameters.AddWithValue("@name", data.name);
        //        comm.Parameters.AddWithValue("@status", data.status);
        //        //cc.Setdata(sql, comm);
        //        comm.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return new NewUsersinforModels
        //    {
        //        username = data.username,
        //        password = data.password,
        //        name = data.name,
        //        status = data.status,
        //    };
        //}










        //[HttpDelete]
        //[Route("UsersInformation/{id}")]
        //public void Delete(int id)
        //{
        //    try
        //    {

        //        string sql = "DELETE FROM users WHERE usersId ='" + id + "'  ;";
        //        conn.Setdata(sql);
        //    }
        //    catch (Exception ex)
        //    { Console.WriteLine(ex.Message); }


        //}



    }
}
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
    
    public class privileageCol : ControllerBase
    {

        connecDb conn = new connecDb();
        public class myParam
        {
            public string name;
            public object value;
        }

        //[Authorize(Role.User)]
        [HttpGet]
        [Route("Privileage")]
        public List<PrivileageModels> Getprivileagedataall(){
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

        //[Authorize(Role.Admin)]
        [HttpGet]
        [Route("Privileage/{id}")]
        public PrivileageModels Getbyprivileageid(int id)
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


        //[Authorize(Role.Admin)]
        [HttpGet]
        [Route("privileage/Usersinformation")]
        public List<PrivileageAndUsersinforModels> Getallprivileageandusers()
        {

            List<PrivileageAndUsersinforModels> paus = new List<PrivileageAndUsersinforModels>();
            myParam p = new myParam();

            try
            {

                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM  privileage  JOIN  users ON privileage.usersId = users.usersId;");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PrivileageAndUsersinforModels pau = new PrivileageAndUsersinforModels()
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
                        password = dr["passwrd"].ToString(),
                        name = dr["name"].ToString(),
                        status = dr["status"].ToString(),
                    };
                    paus.Add(pau);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return paus;
        }


        //[Authorize(Role.Admin)]
        [HttpPost]
        [Route("Privileage")]
        public NewPrivileageModels Insertprivileage(NewPrivileageModels data)
        {
            try
            {
                
                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "INSERT into privileage set usersId=@usersId,canread=@canread,caninsert=@caninsert,canupdate=@canupdate,candelete=@candelete,candrop=@candrop;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@usersId", data.usersId);
                comm.Parameters.AddWithValue("@canread", data.canread);
                comm.Parameters.AddWithValue("@caninsert", data.caninsert);
                comm.Parameters.AddWithValue("@canupdate", data.canupdate);
                comm.Parameters.AddWithValue("@candelete", data.candelete);
                comm.Parameters.AddWithValue("@candrop", data.candrop);
                comm.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new NewPrivileageModels
            {
                usersId = data.usersId,
                canread = data.canread,
                caninsert = data.caninsert,
                canupdate = data.canupdate,
                candelete = data.candelete,
                candrop = data.candrop,
            };
        }


        //[Authorize(Role.User)]
        [HttpPut]
        [Route("Privileage/{id}")]
        public PrivileageModels Updateprivileage(PrivileageModels data)
        {
            PrivileageModels user = new PrivileageModels();
            connecDb conn = new connecDb();
            try
            {

                MySqlConnection connection = new MySqlConnection(conn.connectDb());
                connection.Open();
                string sql = "UPDATE privileage SET usersId=@usersId,canread=@canread,caninsert=@caninsert,canupdate=@canupdate,candelete=@candelete,candrop=@candrop  WHERE privileageId=@privileageId ;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@privileageId", data.privileageId);
                comm.Parameters.AddWithValue("@usersId", data.usersId);
                comm.Parameters.AddWithValue("@canread", data.canread);
                comm.Parameters.AddWithValue("@caninsert", data.caninsert);
                comm.Parameters.AddWithValue("@canupdate", data.canupdate);
                comm.Parameters.AddWithValue("@candelete", data.candelete);
                comm.Parameters.AddWithValue("@candrop", data.candrop);
                //cc.Setdata(sql);
                comm.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return new PrivileageModels
            {
                privileageId = data.privileageId,
                usersId = data.usersId,
                canread = data.canread,
                caninsert = data.caninsert,
                canupdate = data.canupdate,
                candelete = data.candelete,
                candrop = data.candrop,
            };
        }


        //[Authorize(Role.Admin)]
        [HttpDelete]
        [Route("Privileage/{id}")]
        public void Deleteprivileage(int id)
        {
            try
            {

                string sql = "DELETE FROM privileage WHERE privileageId ='" + id + "'  ;";
                conn.Setdata(sql);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }


        }



    }
}
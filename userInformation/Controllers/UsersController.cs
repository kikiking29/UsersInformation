using System;
using System.IO;
using System.Data;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using testWebapi.DB;

// Connect Models
using testWebapi.Models;
using System.Security.Cryptography;
using System.Text;

namespace testWebapi.Controllers
{
    [ApiController]
    

    public class UsersController :  ControllerBase
    {

        [HttpGet]
        [Route("api/users")]
        public List<UsersModel> GetAll()
        {

            List<UsersModel> users = new List<UsersModel>();

            try
            {
               
                connectDb cc = new connectDb();
                MySqlDataAdapter dap = new MySqlDataAdapter();
                DataSet ds = new DataSet();

                ds = cc.Selectdata("SELECT * FROM usres;");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    UsersModel user = new UsersModel()
                    {
                        
                        Id = int.Parse(dr["id"].ToString()),
                        Name = dr["name"].ToString(),
                        Age = int.Parse(dr["age"].ToString()),
                        Image = (byte[])dr["Image"]
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



        [HttpGet]
        [Route("api/GetByID/{id}")]
        public UsersModel GetByID(string id)
        {
            UsersModel user = new UsersModel();
            try
            {
                
                connectDb cc = new connectDb();
                DataSet ds = new DataSet();

                ds = cc.Selectdata("SELECT * FROM  usres WHERE id ='" + id + "';");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    user = new UsersModel()
                    {
                        Id = int.Parse(dr["id"].ToString()),
                        Name = dr["name"].ToString(),
                        Age = int.Parse(dr["age"].ToString()),
                    };

                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
                return user;
        }



        [HttpPost]
        [Route("api/image")]
        public UsersModel InsertImage(NewUsersModel data)
        {
            try
            {
                connectDb cc = new connectDb();
                MySqlConnection connection = new MySqlConnection(cc.connect_Db());
                connection.Open();
                string sql = "INSERT into usres set Name=@name,Age=@age,Image=@image;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@name", data.name);
                comm.Parameters.AddWithValue("@age", data.age);
                //Console.WriteLine(data.image.Length.ToString());
                comm.Parameters.AddWithValue("@image",data.image);
                //cc.Setdata(sql, comm);
                comm.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new UsersModel
            {
                Name = data.name,
                Age = data.age,
                Image = data.image,
            };
        }


        [HttpPut]
        [Route("api/Update")]
        public UsersModel Update(UsersModel data)
        {
            UsersModel user = new UsersModel();
            try
            {
                
                connectDb cc = new connectDb();
                MySqlConnection connection = new MySqlConnection(cc.connect_Db());
                connection.Open();
                string sql = "UPDATE usres SET Name=@name,Age=@age,Image=@image  WHERE Id =@id ;";
                MySqlCommand comm = new MySqlCommand(sql, connection);
                comm.Parameters.AddWithValue("@id", data.Id);
                comm.Parameters.AddWithValue("@name", data.Name);
                comm.Parameters.AddWithValue("@age", data.Age);
                //Console.WriteLine(data.image.Length.ToString());
                comm.Parameters.AddWithValue("@image", data.Image);
                //cc.Setdata(sql);
                comm.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return new UsersModel
            {
                Id = data.Id,
                Name = data.Name,
                Age = data.Age,
                Image = data.Image
            };
        }


        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"> id </param>
        [HttpDelete]
        [Route("api/Delete/{id}")]
        public void Delete(int id)
        {
            try
            {
                connectDb cc = new connectDb();

                string sql = "DELETE FROM usres WHERE id ='" + id + "'  ;";
                cc.Setdata(sql);
            }
            catch (Exception ex) { Console.WriteLine( ex.Message); }
            

        }


        //[HttpPost]
        //[Route("api/Insert")]
        //public UsersModel Create(String name, int age, String image)
        //{
        //    try
        //    {
        //        connectDb cc = new connectDb();

        //        string sql = "INSERT into usres set Name='" + name + "',Age=" + age + " ;";

        //        cc.Setdata(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return new UsersModel
        //    {
        //        Name = name,
        //        Age = age,
        //    };
        //}

        //[HttpPost]
        //[Route("api/image")]
        //public UsersModel InsImage(NewUsersModel data)
        //{
        //    try
        //    {
        //        connectDb cc = new connectDb();
        //        cc.conn_Db();
        //        cc.conn_Db().Open();
        //        string sql = "INSERT into usres set Name=@name,Age=@age,Image=@image;";
        //        MySqlCommand comm = new MySqlCommand(sql,cc.conn_Db());
        //        comm.Parameters.AddWithValue("@name", data.name);
        //        comm.Parameters.AddWithValue("@age", data.age);
        //        //Console.WriteLine(data.image.Length.ToString());
        //        comm.Parameters.AddWithValue("@image", data.image);
        //        //cc.Setdata(sql, comm);
        //        comm.ExecuteNonQuery();
        //        cc.conn_Db().Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return new UsersModel
        //    {
        //        Name = data.name,
        //        Age = data.age,
        //        Image = data.image,
        //    };
        //}
    }
}
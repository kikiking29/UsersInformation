using Microsoft.AspNetCore.Mvc;
using userInformation;
using userInformation.ConnecDb;

using System;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace userInformation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userInformationCol : ControllerBase
    {
        connecDb conn = new connecDb();
        public class myParam
        {
            public string name;
            public object value;
        }

        [HttpGet]
        [Route("api/users")]
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
        [Route("GetbyId/{id}")]
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
        [Route("GetAllPrivileage/privileage")]
        public UsersinforAndPrivileageModels GetAllPrivileage()
        {

            UsersinforAndPrivileageModels uap = new UsersinforAndPrivileageModels();
            myParam p = new myParam();

            try
            {

                DataSet ds = new DataSet();
                ds = conn.Selectdata("SELECT * FROM users INNER JOIN privileage ON privileage.usersId = users.usersId;");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    uap = new UsersinforAndPrivileageModels()
                    {
                        u_usersId = int.Parse(dr[0].ToString()),
                        username = dr[1].ToString(),
                        password = (byte[])dr[2],
                        name = dr[3].ToString(),
                        status = dr[4].ToString(),
                        privileageId = int.Parse(dr[5].ToString()),
                        p_usersId = int.Parse(dr[6].ToString()),
                        read = Boolean.Parse(dr[7].ToString()),
                        insert = Boolean.Parse(dr[8].ToString()),
                        update = Boolean.Parse(dr[9].ToString()),
                        delete = Boolean.Parse(dr[10].ToString()),
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return uap;
        }

        
    }
}
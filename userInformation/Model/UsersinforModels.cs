using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace userInformation.Model
{
    public class UsersinforModels
    {
        //[Required(ErrorMessage = "UsersId is required")]
        [RegularExpression(@"\d+", ErrorMessage = "Please enter a valid usersId")]
        public int usersId { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.@#]{4,20}$", ErrorMessage = "Please enter a valid username")]
        public string? username { get; set; } = string.Empty;

        
        [RegularExpression(@"^[a-zA-Z0-9_#@.]{8,20}$", ErrorMessage = "Please enter a valid password")]
        public string? password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]{4,20}$", ErrorMessage = "Please enter a valid name")]
        public string? name { get; set; } = string.Empty;


        [RegularExpression(@"(Active|InActive|Delete)", ErrorMessage = "Please enter a valid status")]
        public string? status { get; set; } = string.Empty;
    }

    public class PrivileageModels
    {
        [Required(ErrorMessage = "PrivileageId is required")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Please enter a valid PrivileageId")]
        public int privileageId { get; set; }

        [Required(ErrorMessage = "UsersId is required")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Please enter a valid usersId")]
        public int usersId { get; set; }

        [Required(ErrorMessage = "Canread is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid canread")]
        public string canread { get; set; }

        [Required(ErrorMessage = "Caninsert is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid caninsert")]
        public string caninsert { get; set; }

        [Required(ErrorMessage = "Canupdate is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid canupdate")]
        public string canupdate { get; set; }

        [Required(ErrorMessage = "Candelete is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid candelete")]
        public string candelete { get; set; }

        [Required(ErrorMessage = "Candrop is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid candrop")]
        public string candrop { get; set; }

    }
    public class UsersinforAndPrivileageModels
    {
        public int u_usersId { get; set; }
        public string? username { get; set; }
        public string password { get; set; }
        public string? name { get; set; }
        public string status { get; set; }
        public int privileageId { get; set; }
        public int p_usersId { get; set; }
        public string canread { get; set; }
        public string caninsert { get; set; }
        public string canupdate { get; set; }
        public string candelete { get; set; }

        public string candrop { get; set; }
    }

    public class PrivileageAndUsersinforModels
    {

        public int privileageId { get; set; }
        public int p_usersId { get; set; }
        public string canread { get; set; }
        public string caninsert { get; set; }
        public string canupdate { get; set; }
        public string candelete { get; set; }
        public string candrop { get; set; }
        public int u_usersId { get; set; }
        public string? username { get; set; }
        public string password { get; set; }
        public string? name { get; set; }
        public string status { get; set; }
    }

    public class NewUsersinforModels
    {

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.@#]{4,20}$", ErrorMessage = "Please enter a valid username")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9_#@.]{8,20}$", ErrorMessage = "Please enter a valid password")]
        public string? password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"[a-zA-Z]{4,20}$", ErrorMessage = "Please enter a valid name")]
        public string? name { get; set; }

        //[Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"(Active|InActive|Delete)", ErrorMessage = "Please enter a valid status")]
        public string? status { get; set; } = string.Empty;
    }

    public class Selectwithpagging
    {
        //[Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,20}$", ErrorMessage = "Please enter a valid username")]
        public string? username { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]{0,20}$", ErrorMessage = "Please enter a valid name")]
        public string? name { get; set; }

        //[Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"(Active|InActive|Delete|A|a|I|i|D|d)$", ErrorMessage = "Please enter a valid status")]
        public string? status { get; set; }
    }
   

    public class PasswordModels
    {

        //[Required(ErrorMessage = "UsersId is required")]
        [RegularExpression(@"\d+", ErrorMessage = "Please enter a valid usersId")]
        public int usersId { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.@#]{4,20}$", ErrorMessage = "Please enter a valid username")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Oldpassword is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.#@]{8,20}$", ErrorMessage = "Please enter a valid oldpassword")]
        public string old_password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.#@]{8,20}$", ErrorMessage = "Please enter a valid password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Recheckpassword is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.#@]{8,20}$", ErrorMessage = "Please enter a valid recheckpassword")]
        public string recheck_password { get; set; }
    }
    

    public class NewPrivileageModels
    {

        [Required(ErrorMessage = "UsersId is required")]
        [RegularExpression(@"\d+", ErrorMessage = "Please enter a valid usersId")]
        public int usersId { get; set; }

        [Required(ErrorMessage = "Canread is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid canread")]
        public string canread { get; set; }

        [Required(ErrorMessage = "Caninsert is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid caninsert")]
        public string caninsert { get; set; }

        [Required(ErrorMessage = "Canupdate is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid canupdate")]
        public string canupdate { get; set; }

        [Required(ErrorMessage = "Candelete is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid candelete")]
        public string candelete { get; set; }

        [Required(ErrorMessage = "Candrop is Recheckpassword")]
        [RegularExpression(@"(0|1)$", ErrorMessage = "Please enter a valid candrop")]
        public string candrop { get; set; }
    }
   

    
    public class UserDto
    {

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.@#]+", ErrorMessage = "Please enter a valid Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.#@]+", ErrorMessage = "Please enter a valid password")]
        public string? Password { get; set; }
    }
}

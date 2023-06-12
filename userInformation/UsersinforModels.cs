using System;


namespace userInformation
{
    public class UsersinforModels
    {
        public int usersId { get; set; }
        public string? username { get; set; }
        public byte[] password { get; set; }
        public string? name { get; set; }
        public string status { get; set; }
    }
    public class NewUsersinforModels
    {
        public string? username { get; set; }
        public byte[] password { get; set; }
        public string? name { get; set; }
        public string status { get; set; }
    }

    public class PasswordModels
    {
        public int usersId { get; set; }
        public byte[] password { get; set; }
    }
    public class PrivileageModels
    {
        public int privileageId { get; set; }
        public int usersId { get; set; }
        public Boolean read { get; set; }
        public Boolean insert { get; set; }
        public Boolean update { get; set; }
        public Boolean delete { get; set; }
    }

    public class UsersinforAndPrivileageModels
    {
        public int u_usersId { get; set; }
        public string? username { get; set; }
        public byte[] password { get; set; }
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

}

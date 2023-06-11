using System;


namespace userInformation.Models
{
    public class UsersModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int Age { get; set; }
        public byte[] Image { get; set; }
    }


    public class NewUsersModel
    {
        public string name { get; set; }
        public int age { get; set; }
        public byte[] image { get; set; }
    }
}

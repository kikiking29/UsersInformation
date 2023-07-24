using System.Text.Json.Serialization;

namespace userInformation.Entities
{

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public string Role { get; set; }

    }
}

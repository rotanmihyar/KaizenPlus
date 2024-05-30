namespace BlazorApplication.Models
{
    public class Data
    {
        public string id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
        public string dateOfBirth { get; set; }
        public string createdDate { get; set; }
        public List<Role> roles { get; set; }
        public string token { get; set; }
        public RefreshToken refreshToken { get; set; }
    }

    public class RefreshToken
    {
        public string refreshToken { get; set; }
        public string validUntil { get; set; }
    }

    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class LoginRoot
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool success { get; set; }
        public Data data { get; set; }
    }
}
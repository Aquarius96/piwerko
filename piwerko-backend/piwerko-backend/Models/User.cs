namespace Piwerko.Api.Models
{
    public class User
    {
        public long id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string avatar_URL { get; set; }
        public bool isAdmin { get; set; }
        public bool isConfirmed { get; set; }

        public string ConfirmationCode { get; set; }


    }
}

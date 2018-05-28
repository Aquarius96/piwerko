namespace Piwerko.Api.Models.DB
{
    public class Brewery
    {
        public long id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string streetNumber { get; set; }
        public string description { get; set; }
        public string web_Url { get; set; }
        public string photo_URL { get; set; }
        public bool isConfirmed { get; set; }
    }
}

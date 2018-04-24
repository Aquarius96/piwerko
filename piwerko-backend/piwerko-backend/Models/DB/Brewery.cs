namespace Piwerko.Api.Models.DB
{
    public class Brewery
    {
        public long id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public int streetNumber { get; set; }
        public string description { get; set; }
        public string webUrl { get; set; }
        public bool isConfirmed { get; set; }
    }
}

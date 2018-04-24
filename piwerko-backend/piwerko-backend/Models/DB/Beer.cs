namespace Piwerko.Api.Models.DB
{
    public class Beer
    {
        public long id { get; set; }
        public string name { get; set; }
        public double alcohol { get; set; }
        public double ibu { get; set; }
        public int breweryId { get; set; }
        public double servingTemp { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public bool isConfirmed { get; set; }
    }
}

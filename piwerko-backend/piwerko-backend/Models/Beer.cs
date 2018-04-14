namespace Piwerko.Api.Models
{
    public class Beer
    {
        public long id { get; set; }
        public string name { get; set; }
        public double alcohol { get; set; }
        public double ibu { get; set; }
        public int breweryId { get; set; }
        public int servingTemp { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public bool isConfirmed { get; set; }
    }
}

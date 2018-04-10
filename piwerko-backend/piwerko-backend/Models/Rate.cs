namespace Piwerko.Api.Models
{
    public class Rate
    {
        public long id { get; set; }
        public double value { get; set; }
        public int userId { get; set; }
        public int beerId { get; set; }
    }
}

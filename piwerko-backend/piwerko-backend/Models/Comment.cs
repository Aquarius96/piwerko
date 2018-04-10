namespace Piwerko.Api.Models
{
    public class Comment
    {
        public long id { get; set; }
        public string content { get; set; }
        public int userId { get; set; }
        public int beerId { get; set; }
        public int breweryId { get; set; }
        public string DateTime { get; set; }
    }
}

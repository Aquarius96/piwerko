namespace Piwerko.Api.Models
{
    public class Comment
    {
        public long id { get; set; }
        public string content { get; set; }
        public long userId { get; set; }
        public long beerId { get; set; }
        public long breweryId { get; set; }
        public string DateTime { get; set; }
    }
}

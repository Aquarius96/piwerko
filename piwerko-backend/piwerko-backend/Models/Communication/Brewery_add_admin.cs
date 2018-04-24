using Piwerko.Api.Models.DB;

namespace Piwerko.Api.Models.Communication
{
    public class BreweryModel
    {
        public int user_id { get; set; }

        public string name { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public int streetNumber { get; set; }
        public string description { get; set; }
        public string webUrl { get; set; }

        public Brewery GetBrewery()
        {
            var brewery = new Brewery { name = name, city = city, street = street, streetNumber = streetNumber, description = description, webUrl = webUrl, isConfirmed = true };
            return brewery;
        }
    }
}

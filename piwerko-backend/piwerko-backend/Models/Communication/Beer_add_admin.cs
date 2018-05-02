using Piwerko.Api.Models.DB;

namespace Piwerko.Api.Models.Communication
{
    public class BeerModel
    {
        public int user_id { get; set; }

        public string name { get; set; }
        public double alcohol { get; set; }
        public double ibu { get; set; }
        public int breweryId { get; set; }
        public double servingTemp { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string photo_URL { get; set; }


        public Beer GetBeer()
        {
            var beer = new Beer { name = name, alcohol = alcohol, ibu = ibu, breweryId = breweryId, servingTemp = servingTemp, type = type, description = description,photo_URL=photo_URL, isConfirmed = true };
            return beer;
        }
    }
}

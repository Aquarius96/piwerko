using Piwerko.Api.Models.DB;

namespace Piwerko.Api.Models.Communication
{
    public class Beer_add_admin
    {
        public int user_id { get; set; }

        public string name { get; set; }
        public double alcohol { get; set; }
        public double ibu { get; set; }
        public int breweryId { get; set; }
        public double servingTemp { get; set; }
        public string type { get; set; }
        public string description { get; set; }


        public Beer GetBeer()
        {
            var beer = new Beer { name = name, alcohol = alcohol, ibu = ibu, breweryId = breweryId, servingTemp = servingTemp, type = type, description = description, isConfirmed = true };
            return beer;
        }
    }
}

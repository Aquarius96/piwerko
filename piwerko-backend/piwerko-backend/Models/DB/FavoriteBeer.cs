using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Models.DB
{
    public class FavoriteBeer
    {
        public long id { get; set; }
        public int user_id { get; set; }
        public int beer_id { get; set; }
    }
}

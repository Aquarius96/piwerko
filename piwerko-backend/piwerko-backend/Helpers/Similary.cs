using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Helpers
{
    public class Similary
    {
        public int alco { get; set; }
        public int ibu { get; set; }
        public int temp { get; set; }

        public Similary()
        {
            this.alco = 5;
            this.ibu = 5;
            this.temp = 5;
        }
    }
}

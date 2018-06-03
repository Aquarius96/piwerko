using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Helpers
{
    public class Similary : IComparable
    {
        public int alco { get; set; }
        public int ibu { get; set; }
        public int temp { get; set; }

        public double points { get; set; }
        public Beer beer { get; set; }

        public Similary()
        {
            this.alco = 2;
            this.ibu = 7;
            this.temp = 5;
        }

        public Similary(Beer beer, Beer otherBeer)
        {
            this.points = SetSimilaryPoints(beer,otherBeer);
            this.beer = beer;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Similary otherSimilary = obj as Similary;
            if (otherSimilary != null)
                return this.points.CompareTo(otherSimilary.points);
            else
                throw new ArgumentException("Object is not a Similary");
        }

        private double SetSimilaryPoints(Beer b1,Beer b2)
        {
            double alco_, temp_, ibu_;

            alco_ = (b1.alcohol - b2.alcohol) * 175;
            temp_ = (b1.servingTemp - b2.servingTemp) * 80;
            ibu_ = (b1.ibu - b2.ibu) * 110;


            return (alco_ + temp_ + ibu_)/3;
        }
    }
}

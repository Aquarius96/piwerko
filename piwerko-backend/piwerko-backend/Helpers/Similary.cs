﻿using Piwerko.Api.Models.DB;
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
            double alco, temp, ibu;

            alco = (b1.alcohol - b2.alcohol) * 100;
            temp = (b1.servingTemp - b2.servingTemp) * 100;
            ibu = (b1.ibu - b2.ibu) * 100;


            return (alco + temp + ibu)/3;
        }
    }
}

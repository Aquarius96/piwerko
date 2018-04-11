using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IRateService
    {
        bool Add(double value_, int beerid, int userid);
        bool Update(double value_, int beerid, int userid);
        double GetById(int id);
        double Getrate(int beerid, int userid);
    }
}

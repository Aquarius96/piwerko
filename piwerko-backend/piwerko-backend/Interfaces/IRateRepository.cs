using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IRateRepository
    {
        bool Add(double value_, int beerid, int userid);
        bool Update(double value_, int beerid, int userid);
        IEnumerable<Rate> GetById(int id);
        Rate GetRate(int beerid, int userid);
        void ClearByUserId(int user_id);

    }
}

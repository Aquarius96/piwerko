using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IFavoriteRepository
    {
        void Add(FavoriteBeer data);
        IEnumerable<Beer> FindFavorite(int userid);
        void Delete(int userid, int beerid);
        void Save();

    }
}

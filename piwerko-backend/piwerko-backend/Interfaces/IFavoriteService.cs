using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IFavoriteService
    {
        FavoriteBeer Add(FavoriteBeer favoriteBeer);
        void Delete(int userid, int beerid);
        IEnumerable<Beer> FindFavorite(int userid);

    }
}

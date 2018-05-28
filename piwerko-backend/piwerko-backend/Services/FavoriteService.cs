using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public FavoriteBeer Add(FavoriteBeer favoriteBeer)
        {
            _favoriteRepository.Add(favoriteBeer);
            _favoriteRepository.Save();
            return favoriteBeer;
        }

        public void Delete(int userid,int beerid)
        {
            _favoriteRepository.Delete(userid,beerid);
        }

        public IEnumerable<Beer> FindFavorite(int userid)
        {
            return _favoriteRepository.FindFavorite(userid);
        }

    }
}

using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Repo
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DataContext _context;

        public FavoriteRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(FavoriteBeer data)
        {
            _context.Favorites.Add(data);
        }

        public IEnumerable<Beer> FindFavorite(int userid)
        {
            var list = _context.Favorites.Where(x => x.user_id == userid);

            var result = new List<Beer>();
            foreach(var var in list)
            {
                result.Add(_context.Beers.Where(x => x.id == var.beer_id).FirstOrDefault());
            }
            return result;
        }

        private FavoriteBeer FindByIds(int userid, int beerid)
        {
            var res = _context.Favorites.Where(x => x.beer_id == beerid && x.user_id == userid).FirstOrDefault();
            return res;
        }

        public void Delete(int userid, int beerid)
        {
            _context.Favorites.Remove(FindByIds(userid, beerid));

            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}

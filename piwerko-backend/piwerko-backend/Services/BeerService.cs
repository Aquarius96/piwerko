using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System.Collections.Generic;
using System.Linq;

namespace Piwerko.Api.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private Similary similary;

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
            similary = new Similary();
        }
        
        public IEnumerable<Beer> GetSimilary(int beerId)
        {
            var list = _beerRepository.GetSimilary(similary.alco, similary.temp, similary.ibu, GetBeerById(beerId));
            var points = new List<Similary>();
            foreach (var var in list)
            {
                points.Add(new Similary(var, GetBeerById(beerId)));
            }

            points.Sort();

            var result = new List<Beer>();
            foreach (var var in points)
            {
                result.Add(var.beer);
            }
            
            return result.Take(5);
        }

        public IEnumerable<Beer> GetAll()
        {
            return _beerRepository.GetAll();
        }

        public Beer GetBeerById(int beerId)
        {
            return _beerRepository.GetBeerById(beerId);
            
        }

        public IEnumerable<Beer> GetBeerByName(string name_)
        {
            return _beerRepository.GetBeerByName(name_);

        }

        public IEnumerable<Beer> GetBeerUnconfirmed()
        {
            return _beerRepository.GetBeerUnconfirmed();

        }

        public Beer Add(Beer beer)
        {
            beer.photo_URL = "http://www.uncommonsnyc.com/wp-content/uploads/2015/06/beer-bottle.png";
            _beerRepository.Add(beer);
            _beerRepository.Save();

            return beer;
        }


        public void Update(Beer beer_)
        {

            _beerRepository.Update(beer_);
        }


        public bool Delete(int id)
        {
            return _beerRepository.Delete(id);
        }

    }
}

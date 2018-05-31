using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class BeerService : IBeerService
    {
        private readonly PhotoSettings _photoSettings;
        private readonly IBeerRepository _beerRepository;
        private Similary similary;

        public BeerService(IBeerRepository beerRepository, IOptions<PhotoSettings> options)
        {
            _beerRepository = beerRepository;
            _photoSettings = options.Value;
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
            beer.photo_URL = _photoSettings.DefaultBeerPhotoUrl;
            _beerRepository.Add(beer);
            _beerRepository.Save();

            return beer;
        }


        public async Task UploadPhoto(int beerId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var beer = _beerRepository.GetBeerById(beerId);
            beer.photo_URL = @"http://localhost:8080/api/photo/" + "beers/" + $"{fileName}";
            _beerRepository.Update(beer);


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

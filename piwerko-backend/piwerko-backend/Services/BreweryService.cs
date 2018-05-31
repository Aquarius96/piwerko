using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Piwerko.Api.Helpers;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly PhotoSettings _photoSettings;

        public BreweryService(IBreweryRepository breweryRepository, IOptions<PhotoSettings> options)
        {
            _breweryRepository = breweryRepository;
            _photoSettings = options.Value;
        }


        public async Task UploadPhoto(int breweryId, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var brewery = _breweryRepository.GetBreweryById(breweryId);
            brewery.photo_URL = @"http://localhost:8080/api/photo/" + _photoSettings.DirOfBrewery + "/" + $"{fileName}";
            _breweryRepository.Update(brewery);


        }

        public IEnumerable<Brewery> GetAll()
        {
            return _breweryRepository.GetAll();
        }

        public Brewery GetBreweryById(int breweryId)
        {
            return _breweryRepository.GetBreweryById(breweryId);
            
        }

        public IEnumerable<Brewery> GetBreweryByName(string name_)
        {
            return _breweryRepository.GetBreweryByName(name_);

        }

        public IEnumerable<Brewery> GetBreweryUnconfirmed()
        {
            return _breweryRepository.GetBreweryUnconfirmed();

        }

        public Brewery Add(Brewery brewery)
        {
            brewery.photo_URL = _photoSettings.DefaultBreweryPhotoUrl;
            _breweryRepository.Add(brewery);
            _breweryRepository.Save();

            return brewery;
        }

        public void Update(Brewery brewery_)
        {
            _breweryRepository.Update(brewery_);
        }


        public bool Delete(int id)
        {
            return _breweryRepository.Delete(id);
        }

    }
}

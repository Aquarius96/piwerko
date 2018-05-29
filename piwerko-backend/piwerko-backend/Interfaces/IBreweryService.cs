using Microsoft.AspNetCore.Http;
using Piwerko.Api.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IBreweryService
    {
        IEnumerable<Brewery> GetAll();
        Brewery GetBreweryById(int breweryId);
        IEnumerable<Brewery> GetBreweryByName(string name_);
        IEnumerable<Brewery> GetBreweryUnconfirmed();
        Brewery Add(Brewery brewery);
        void Update(Brewery brewery_);
        bool Delete(int id);
        Task UploadPhoto(int breweryId, IFormFile file, string uploadsFolderPath);
    }
}

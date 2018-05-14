using Microsoft.AspNetCore.Http;
using Piwerko.Api.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface IBeerService
    {
        IEnumerable<Beer> GetAll();
        Beer GetBeerById(int beerId);
        IEnumerable<Beer> GetBeerByName(string name_);
        IEnumerable<Beer> GetBeerUnconfirmed();
        Beer Add(Beer beer);
        void Update(Beer beer_);
        bool Delete(int id);
        IEnumerable<Beer> GetSimilary(int beerId);
        Task UploadPhoto(int beerId, IFormFile file, string uploadsFolderPath);
    }
}

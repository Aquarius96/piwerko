using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IBreweryRepository
    {
        IEnumerable<Brewery> GetAll();
        Brewery GetBreweryById(int breweryId);
        IEnumerable<Brewery> GetBreweryByName(string name_);
        IEnumerable<Brewery> GetBreweryUnconfirmed();
        void Add(Brewery brewery);
        void Update(Brewery brewery_);
        bool Delete(long id);
        void Save();
    }
}

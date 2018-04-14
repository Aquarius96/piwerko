using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IBreweryService
    {
        IEnumerable<Brewery> GetAll();
        Brewery GetBreweryById(int breweryId);
        IEnumerable<Brewery> GetBreweryByName(string name_);
        IEnumerable<Brewery> GetBreweryUnconfirmed();
        Brewery Add(Brewery brewery);
        Brewery AddByAdmin(Brewery brewery);
        void Update(Brewery brewery_);
        bool Delete(int id);
    }
}

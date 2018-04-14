using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IBeerService
    {
        IEnumerable<Beer> GetAll();
        Beer GetBeerById(int beerId);
        IEnumerable<Beer> GetBeerByName(string name_);
        IEnumerable<Beer> GetBeerUnconfirmed();
        Beer AddByAdmin(Beer beer);
        Beer Add(Beer beer);
        void Update(Beer beer_);
        bool Delete(int id);
    }
}

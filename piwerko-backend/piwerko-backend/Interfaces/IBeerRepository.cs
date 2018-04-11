using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IBeerRepository
    {
        bool Delete(long id);
        Beer GetBeerById(int id);
        IEnumerable<Beer> GetBeerByName(string name);
        IEnumerable<Beer> GetAll();
        void Add(Beer beer);
        void Save();

        void Update(Beer beer);

        
    }
}

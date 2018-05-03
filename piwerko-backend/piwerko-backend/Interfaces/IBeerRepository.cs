using Piwerko.Api.Models.DB;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IBeerRepository
    {
        bool Delete(long id);
        Beer GetBeerById(int id);
        IEnumerable<Beer> GetBeerByName(string name);
        IEnumerable<Beer> GetBeerUnconfirmed();
        IEnumerable<Beer> GetAll();
        void Add(Beer beer);
        void Save();

        void Update(Beer beer);

        IEnumerable<Beer> GetSimilary(int alco, int temp, int ibu,Beer beer);
    }
}

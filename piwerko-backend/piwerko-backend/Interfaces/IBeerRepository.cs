﻿using Piwerko.Api.Models;
using System.Collections.Generic;

namespace Piwerko.Api.Interfaces
{
    public interface IBeerRepository
    {
        bool Delete(long id);
        Beer GetBeerById(int id);
        Beer GetBeerByName(string name);
        IEnumerable<Beer> GetAll();
        //bool CheckLogin(string username);
        void Add(Beer beer);
        void Save();

        void Update(Beer beer);

        
    }
}

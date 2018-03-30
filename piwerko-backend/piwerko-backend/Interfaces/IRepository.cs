using System;
using System.Collections.Generic;
using System.Text;
using Piwerko.Data.DbModels;

namespace Piwerko.Data.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        bool Exist(Func<T, bool> function);
        T GetBy(Func<T, bool> function);
        void Insert(T user);
    }
}

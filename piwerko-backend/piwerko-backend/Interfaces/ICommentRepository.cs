using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface ICommentRepository
    {
        bool Delete(long id);
        IEnumerable<Comment> GetByBeerId(int BeerId);
        void Add(Comment comment);
        void Update(Comment Comment);
        void Save();
        void ClearByUserId(int user_id);
    }
}

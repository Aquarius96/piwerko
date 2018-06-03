using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Interfaces
{
    public interface ICommentService
    {
        Comment Add(Comment comment);
        Comment Update(Comment comment);
        IEnumerable<Comment> GetByBeerId(int beerid);
        bool Delete(int id);
        void ClearByUserId(int user_id);
        Comment GetById(int id);
    }
}

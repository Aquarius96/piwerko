using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Comment Add(Comment comment)
        {
            _commentRepository.Add(comment);
            _commentRepository.Save();
            return comment;
        }
        public Comment Update(Comment comment)
        {
            _commentRepository.Update(comment);
            return comment;
        }
        public IEnumerable<Comment> GetByBeerId(int beerid)
        {
            return _commentRepository.GetByBeerId(beerid).ToList();
        }

        public bool Delete(int id)
        {
            return _commentRepository.Delete(id);
        }
    }
}


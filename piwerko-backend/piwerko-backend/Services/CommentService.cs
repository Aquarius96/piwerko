using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Comment Add(Comment comment)
        {
            _commentRepository.Add(comment);
            return comment;
        }
        public Comment Update(Comment comment)
        {
            _commentRepository.Update(comment);
            return comment;
        }
        public Comment GetByBeerId(int beerid)
        {
            var all = _commentRepository.GetByBeerId(beerid).ToList();

            double suma = 0;

            foreach (var var in all)
            {
                suma += var.value;
            }

            var result = suma / all.Count;

            return result;
        }

        public double Getrate(int beerid, int userid)
        {
            var rate = _commentRepository.GetRate(beerid, userid);
            return rate.value;
        }
    }
}
}

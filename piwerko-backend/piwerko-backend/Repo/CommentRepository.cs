using Piwerko.Api.Interfaces;
using Piwerko.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api.Repo
{
    public class CommentRepository : ICommentRepository 
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public bool Delete(long id)
        {
            var comments = _context.Comments.FirstOrDefault(t => t.id == id);

            if (comments == null)
            {
                return false;
            }

            _context.Comments.Remove(comments);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Comment> GetByBeerId(int BeerId)
        {
            var comment = _context.Comments.Where(x => x.id == BeerId);

            return comment;
        }
        
        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void Update(Comment Comment)
        {
            try
            {
                _context.Comments.Update(Comment);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

﻿using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;
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
            var comment = _context.Comments.Where(x => x.beerId == BeerId);

            return comment;
        }
        
        public Comment GetById(int id)
        {
            return _context.Comments.Where(x => x.id == id).FirstOrDefault();
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

        public void ClearByUserId(int user_id)
        {
            foreach (var var in _context.Comments.Where(x => x.userId == user_id).ToList())
            {
                _context.Comments.Remove(var);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

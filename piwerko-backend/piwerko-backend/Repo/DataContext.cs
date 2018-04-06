﻿using Microsoft.EntityFrameworkCore;
using Piwerko.Api.Models;

namespace Piwerko.Api.Repo
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}

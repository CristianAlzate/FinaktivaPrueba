using Finaktiva.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finaktiva.Repository
{
    public class Context : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Context(DbContextOptions<Context> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}

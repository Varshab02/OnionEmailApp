using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnionEmailApp.Domain.Entities;

namespace OnionEmailApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}

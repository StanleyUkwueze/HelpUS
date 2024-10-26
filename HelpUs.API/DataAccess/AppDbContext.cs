using HelpUs.API.Entity.Entities;
using HelpUs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpUs.API.DataAccess
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

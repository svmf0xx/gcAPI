using Microsoft.EntityFrameworkCore;
using gcapi.Models;

namespace gcapi.db
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EventModel> EventTable { get; set; }
        public DbSet<UserModel> UserTable { get; set; }
    }
}

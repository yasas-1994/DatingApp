using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
        }
        //returns a DbSet. DbSet<Value> stands that it returns value types of data set from value class.
        // Values will be the table name.
        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users   { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
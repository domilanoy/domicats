using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CatmashAPI
{
    // https://docs.microsoft.com/fr-fr/ef/core/get-started/netcore/new-db-sqlite
    internal class CatContext : DbContext
    {
        const string DATABASE = "Data Source=cats.db3";

        public DbSet<Cat> Cats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DATABASE);
        }
    }
    public class Cat
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int Score { get; set; }
    }
}

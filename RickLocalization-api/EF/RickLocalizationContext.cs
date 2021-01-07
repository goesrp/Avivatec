
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RickLocalization_api.EF
{
    public class RickLocalizationContext :  DbContext
    {
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Rick> Ricks { get; set; }
        public DbSet<Morty> Mortys { get; set; }
        public DbSet<Travel> Travels {get;set;}

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        // {
        //     optionsBuilder.UseSqlServer(@"Server=DESKTOP-ALG84S1\SQLEXPRESS;Database=RickLocationDB;Trusted_Connection=True;");
        // }

        public RickLocalizationContext (DbContextOptions options) : base (options) { }
    }
}
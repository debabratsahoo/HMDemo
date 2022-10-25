using HM.Product.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace HM.Product.Data
{
    public class HMProductContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "HMProductDB");
        }
        public DbSet<ProductDB> Product { get; set; }
        public DbSet<ArticleDB> Article { get; set; }
        //protected override void OnModelCreating()
        //{
        //    // configures one-to-many relationship
        //    modelBuilder.Entity<ArticleDB>()
        //        .HasRequired(s => s.Product)
        //        .WithMany(g => g.Articles)
        //        .HasForeignKey(s => s.Id);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleDB>().HasKey(h => new {h.Id, h.ColorId });

         //   modelBuilder.Entity<ProductDB>()
         //.HasMany(c => c.Articles)
         //.WithOne(e => e.Product).IsRequired()
         //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProductDB>()
            //            .HasMany<ArticleDB>(s => s.Articles)
            //            .WithMany(c => c.Products).LeftNavigation()
            //            //.(cs =>
            //            //{
            //            //    cs.MapLeftKey("Id");
            //            //    cs.MapRightKey("ArticleId");
            //            //    cs.ToTable("ProductArticle");
            //            //});

        }

    }
}

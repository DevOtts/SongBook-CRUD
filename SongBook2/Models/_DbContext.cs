using System;
using Microsoft.EntityFrameworkCore;
namespace SongBook2.Models
{
    public class _DbContext : DbContext
    {
        public _DbContext(DbContextOptions<_DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Mapping inheritance of Song to Article
            modelBuilder.Entity<Song>().HasBaseType<Article>();

            //Mapping many-to-many relationship betwwen Article and Tags
            //https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
            modelBuilder.Entity<ArticleTag>().HasKey(sc => new { sc.ArticleId, sc.TagId });
            modelBuilder.Entity<ArticleTag>()
                .HasOne<Article>(sc => sc.Article)
                .WithMany(s => s.ArticleTags)
                .HasForeignKey(sc => sc.ArticleId);
            modelBuilder.Entity<ArticleTag>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.ArticleTags)
                .HasForeignKey(sc => sc.TagId);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleTag> ArticleTag { get; set; }

    }


}

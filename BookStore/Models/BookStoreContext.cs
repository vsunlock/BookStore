using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookStoreContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
            .HasMany(t => t.Books)
            .WithMany(t => t.Authors)
            .Map(m =>
            {
                m.ToTable("AuthorBooks");
                m.MapLeftKey("AuthorID");
                m.MapRightKey("BookID");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
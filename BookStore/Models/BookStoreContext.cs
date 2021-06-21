using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookStoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Author>()
            //.HasMany(t => t.Books)
            //.WithMany(t => t.Authors)
            //.Map(m =>
            //{
            //    m.ToTable("AuthorBooks");
            //    m.MapLeftKey("author_id");
            //    m.MapRightKey("book_id");
            //});

            modelBuilder.Entity<AuthorBook>()
            .HasKey(bc => new { bc.book_id, bc.author_id });

            modelBuilder.Entity<AuthorBook>()
                .HasRequired(bc => bc.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(bc => bc.book_id);

            modelBuilder.Entity<AuthorBook>()
                .HasRequired(bc => bc.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(bc => bc.author_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
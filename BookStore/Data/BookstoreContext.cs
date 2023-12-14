using Bookstore.Models;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Accessories> Accessories { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }   
        public DbSet<DeliveryType> DeliveryTypes { get; set;}
        public DbSet<EBook> EBooks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set;}
        public DbSet<PhysicalBook> PhysicalBooks { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookstoreDB;Integrated Security=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthors>().HasNoKey();
            modelBuilder.Entity<OrderItem>()
        .HasKey(oi => new { oi.ItemId, oi.OrderId });
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany()  // No need for a navigation property on Item
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
            // One-to-many 
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany()
                .HasForeignKey(b => b.PublisherId);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Translator)
                .WithMany()
                .HasForeignKey(b => b.TranslatorId);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany()
                .HasForeignKey(b => b.GenreId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithOne()
                .HasForeignKey<Order>(o => o.DeliveryId);

            // One-to-one
            modelBuilder.Entity<Accessories>()
                .HasOne(a => a.Item)
                .WithOne()
                .HasForeignKey<Accessories>(a => a.ItemId);

            modelBuilder.Entity<EBook>()
                .HasOne(e => e.Item)
                .WithOne()
                .HasForeignKey<EBook>(e => e.ItemId);

            modelBuilder.Entity<PhysicalBook>()
                .HasOne(p => p.Item)
                .WithOne()
                .HasForeignKey<PhysicalBook>(p => p.ItemId);

            // Many-to-many
            modelBuilder.Entity<BookAuthors>()
                .HasOne<Item>(ba => ba.Item)
                .WithMany()
                .HasForeignKey(ba => ba.ItemId);

            modelBuilder.Entity<BookAuthors>()
                .HasOne<Author>(ba => ba.Author)
                .WithMany()
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<Review>()
                .HasKey(r => new { r.UserId, r.ItemId });

            modelBuilder.Entity<WishlistItem>()
                .HasKey(wi => new { wi.UserId, wi.ItemId });
        }
    }
}

using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookEdition> BookEditions { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BorrowRequest> BorrowRequests { get; set; }
        public DbSet<BorrowRequestItem> BorrowRequestItems { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<BorrowPolicy> BorrowPolicies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureRelationships(modelBuilder);

            SeedRoles(modelBuilder);
            SeedMembers(modelBuilder);
            SeedAccount(modelBuilder);
            SeedBorrowPolicy(modelBuilder);
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.BorrowRequestItem)
                .WithMany(i => i.Loans)
                .HasForeignKey(l => l.BorrowRequestItemId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(x => new { x.BookId, x.AuthorId });

            modelBuilder.Entity<BookCategory>()
                .HasKey(x => new { x.BookId, x.CategoryId });
        }
        private void SeedBorrowPolicy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowPolicy>().HasData(
                new BorrowPolicy
                {
                    Id = 1,
                    MaxBooksPerRequest = 3,
                    MaxDaysBorrow = 14,
                    MaxActiveLoan = 5
                }
            );
        }
        private void SeedAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                // Manager
                new Account
                {
                    Id = Guid.Parse("aaaaaaaa-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Username = "admin",
                    Email = "nghethuat24tren7@gmail.com",
                    PasswordHash = "$2a$11$ydw5pVe0.2x8GC6MvPtP6.KsX7fceOXl8Tzhny.XVf0Z2KkiSzhji",
                    IsActive = true,
                    IsEmailVerified = true,
                    RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    MemberId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },

                // Librarian
                new Account
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Username = "librarian",
                    Email = "viethdhe161234@fpt.edu.vn",
                    PasswordHash = "$2a$11$ydw5pVe0.2x8GC6MvPtP6.KsX7fceOXl8Tzhny.XVf0Z2KkiSzhji",
                    IsActive = true,
                    IsEmailVerified = true,
                    RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    MemberId = Guid.Parse("bbbbbbbb-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },

                // Normal User
                new Account
                {
                    Id = Guid.Parse("cccccccc-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Username = "user",
                    Email = "dicket60@gmail.com",
                    PasswordHash = "$2a$11$ydw5pVe0.2x8GC6MvPtP6.KsX7fceOXl8Tzhny.XVf0Z2KkiSzhji",
                    IsActive = true,
                    IsEmailVerified = true,
                    RoleId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    MemberId = Guid.Parse("cccccccc-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                }
            );
        }

        private void SeedMembers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    FullName = "Admin User",
                    Phone = "0123456789",
                    Address = "Library"
                },
                new Member
                {
                    Id = Guid.Parse("bbbbbbbb-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    FullName = "Librarian User",
                    Phone = "0987654321",
                    Address = "Library Staff"
                },
                new Member
                {
                    Id = Guid.Parse("cccccccc-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    FullName = "Normal Member",
                    Phone = "0111111111",
                    Address = "City"
                }
            );
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Manager"
                },
                new Role
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Librarian"
                },
                new Role
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "User"
                }
            );
        }
    }
}

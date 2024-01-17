using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model13")
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Publishings> Publishings { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Authors>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Authors>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Authors>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Authors)
                .HasForeignKey(e => e.id_Author);

            modelBuilder.Entity<Books>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.Binding)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Books)
                .HasForeignKey(e => e.id_Book);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Cost)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Publishings>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Publishings>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<Publishings>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Publishings)
                .HasForeignKey(e => e.id_Publishing);

            modelBuilder.Entity<Workers>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Workers>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Workers>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Workers>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<Workers>()
                .Property(e => e.Passport_series)
                .IsFixedLength();

            modelBuilder.Entity<Workers>()
                .Property(e => e.Passport_number)
                .IsUnicode(false);

            modelBuilder.Entity<Workers>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Workers)
                .HasForeignKey(e => e.id_Worker);
        }
    }
}

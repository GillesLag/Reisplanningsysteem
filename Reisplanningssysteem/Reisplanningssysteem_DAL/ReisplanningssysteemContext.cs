using Reisplanningssysteem_Models;
using Microsoft.EntityFrameworkCore;

namespace Reisplanningssysteem_DAL
{
    public class ReisplanningssysteemContext : DbContext
    {
        public DbSet<Bestemming> Bestemmingen { get; set; }
        public DbSet<Boeking> Boekingen { get; set; }
        public DbSet<Cursus> Cursussen { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<GebruikerCursus> GebruikersCursusen { get; set; }
        public DbSet<Gemeente> Gemeenten { get; set; }
        public DbSet<LeeftijdsCategorie> LeeftijdsCategorieën { get; set; }
        public DbSet<Onkost> Onkosten { get; set; }
        public DbSet<Reis> Reizen { get; set; }
        public DbSet<Thema> Themas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Reisplanningssysteem;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RPS");

            modelBuilder.Entity<Gebruiker>()
                .HasOne(x => x.Gemeente)
                .WithMany(x => x.Gebruikers)
                .HasForeignKey(x => x.GemeenteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GebruikerCursus>()
                .HasOne(x => x.Gebruiker)
                .WithMany(x => x.GebruikerCursussen)
                .HasForeignKey(x => x.GebruikerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GebruikerCursus>()
                .HasOne(x => x.Cursus)
                .WithMany(x => x.GebruikerCursussen)
                .HasForeignKey(x => x.CursusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Boeking>()
                .HasOne(x => x.Gebruiker)
                .WithMany(x => x.Boekingen)
                .HasForeignKey(x => x.GebruikerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Boeking>()
                .HasOne(x => x.Reis)
                .WithMany(x => x.Boekingen)
                .HasForeignKey(x => x.ReisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reis>()
                .HasOne(x => x.Hoofdmonitor)
                .WithMany(x => x.Reizen)
                .HasForeignKey(x => x.HoofdmonitorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reis>()
                .HasOne(x => x.LeeftijdsCategorie)
                .WithMany(x => x.Reizen)
                .HasForeignKey(x => x.LeeftijdsCategorieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reis>()
                .HasOne(x => x.Thema)
                .WithMany(x => x.Reizen)
                .HasForeignKey(x => x.ThemaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Reis>()
                .HasOne(x => x.Bestemming)
                .WithMany(x => x.Reizen)
                .HasForeignKey(x => x.BestemmingsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Onkost>()
                .HasOne(x => x.Reis)
                .WithMany(x => x.Onkosten)
                .HasForeignKey(x => x.ReisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

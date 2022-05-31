using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LocationVoitureApi.Models
{
    public partial class projetContext : DbContext
    {
        IConfiguration _configuration;

        public projetContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public projetContext(DbContextOptions<projetContext> options, IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration;
        }


        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Agence> Agences { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Employer> Employers { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Marque> Marques { get; set; } = null!;
        public virtual DbSet<Voiture> Voitures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                String connection = _configuration.GetSection("Connection:string").Value;
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer(@"database=projet;server=LENOVO242\SQL2K14;User ID=sa;pwd=pass");
                optionsBuilder.UseNpgsql(
                    connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin", "mydb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Nom)
                    .HasMaxLength(45)
                    .HasColumnName("nom");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.Photo)
                    .HasMaxLength(45)
                    .HasColumnName("photo");
            });

            modelBuilder.Entity<Agence>(entity =>
            {
                entity.ToTable("agence", "mydb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(45)
                    .HasColumnName("adresse");

                entity.Property(e => e.Nom)
                    .HasMaxLength(45)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "mydb");

                entity.HasIndex(e => e.Cin, "client$cin_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresse).HasColumnName("adresse");

                entity.Property(e => e.Cin)
                    .HasMaxLength(45)
                    .HasColumnName("cin");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Nom)
                    .HasMaxLength(45)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(45)
                    .HasColumnName("prenom");
                entity.Property(e => e.Password)
                    .HasColumnName("password");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(45)
                    .HasColumnName("telephone");
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.ToTable("employeur", "mydb");

                entity.HasIndex(e => e.Idagence, "fk_employeur_agence1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idagence).HasColumnName("idagence");

                entity.Property(e => e.Nom)
                    .HasMaxLength(45)
                    .HasColumnName("nom");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.Photo)
                    .HasMaxLength(45)
                    .HasColumnName("photo");

                entity.HasOne(d => d.IdagenceNavigation)
                    .WithMany(p => p.Employeurs)
                    .HasForeignKey(d => d.Idagence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employeur$fk_employeur_agence1");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location", "mydb");

                entity.HasIndex(e => e.IdClient, "fk_location_Client1_idx");

                entity.HasIndex(e => e.Idemployeur, "fk_location_employeur1_idx");

                entity.HasIndex(e => e.VoitureMatricule, "fk_location_voiture1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateDeb)
                    .HasColumnType("date")
                    .HasColumnName("date_deb");

                entity.Property(e => e.DateFin)
                    .HasColumnType("date")
                    .HasColumnName("date_fin");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.Idemployeur).HasColumnName("idemployeur");

                entity.Property(e => e.VoitureMatricule)
                    .HasMaxLength(50)
                    .HasColumnName("voiture_matricule");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location$fk_location_Client1");

                entity.HasOne(d => d.IdemployeurNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.Idemployeur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_location_employeur");

                entity.HasOne(d => d.VoitureMatriculeNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.VoitureMatricule)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_location_voiture");
            });

            modelBuilder.Entity<Marque>(entity =>
            {
                entity.ToTable("marque", "mydb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(45)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Voiture>(entity =>
            {
                entity.HasKey(e => e.Matricule);

                entity.ToTable("voiture", "mydb");

                entity.HasIndex(e => e.Idmarque, "fk_voiture_marque_idx");

                entity.Property(e => e.Matricule)
                    .HasMaxLength(50)
                    .HasColumnName("matricule");

                entity.Property(e => e.Idmarque).HasColumnName("idmarque");

                entity.Property(e => e.Modele)
                    .HasMaxLength(45)
                    .HasColumnName("modele");

                entity.Property(e => e.Photo)
                    .HasMaxLength(45)
                    .HasColumnName("photo");

                entity.Property(e => e.Poids).HasColumnName("poids");

                entity.Property(e => e.Prix)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("prix");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .HasColumnName("type");

                entity.HasOne(d => d.marque)
                    .WithMany(p => p.Voitures)
                    .HasForeignKey(d => d.Idmarque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_voiture_marque");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
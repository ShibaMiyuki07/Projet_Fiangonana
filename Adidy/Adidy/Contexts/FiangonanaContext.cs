using Microsoft.EntityFrameworkCore;
using Modele;

namespace Adidy.Contexts;

public partial class FiangonanaContext : DbContext
{
    public FiangonanaContext()
    {
    }

    public FiangonanaContext(DbContextOptions<FiangonanaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Droit> Droits { get; set; }

    public virtual DbSet<DroitUtilisateur> DroitUtilisateurs { get; set; }

    public virtual DbSet<Mpandray> Mpandrays { get; set; }

    public virtual DbSet<PaiementAdidy> PaiementAdidies { get; set; }

    public virtual DbSet<PaiementIsantaona> PaiementIsantaonas { get; set; }

    public virtual DbSet<Modele.Type> Types { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:Psql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Droit>(entity =>
        {
            entity.HasKey(e => e.Iddroit).HasName("droits_pkey");

            entity.ToTable("droits");

            entity.Property(e => e.Iddroit).HasColumnName("iddroit");
            entity.Property(e => e.Typedroit).HasColumnName("typedroit");
        });

        modelBuilder.Entity<DroitUtilisateur>(entity =>
        {
            entity.HasKey(e => e.IddroitUtilisateur).HasName("droit_utilisateurs_pkey");

            entity.ToTable("droit_utilisateurs");

            entity.Property(e => e.IddroitUtilisateur)
                .HasMaxLength(6)
                .HasDefaultValueSql("concat('DU00', nextval('iddroit_utilisateur'::regclass))")
                .IsFixedLength()
                .HasColumnName("iddroit_utilisateur");
            entity.Property(e => e.Iddroit).HasColumnName("iddroit");
            entity.Property(e => e.Idutilisateur)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("idutilisateur");
            entity.Property(e => e.Isvalid).HasColumnName("isvalid");

            entity.HasOne(d => d.IddroitNavigation).WithMany(p => p.DroitUtilisateurs)
                .HasForeignKey(d => d.Iddroit)
                .HasConstraintName("droit_utilisateurs_iddroit_fkey");

            entity.HasOne(d => d.IdutilisateurNavigation).WithMany(p => p.DroitUtilisateurs)
                .HasForeignKey(d => d.Idutilisateur)
                .HasConstraintName("droit_utilisateurs_idutilisateur_fkey");
        });

        modelBuilder.Entity<Mpandray>(entity =>
        {
            entity.HasKey(e => e.Numero).HasName("mpandray_pkey");

            entity.ToTable("mpandray");

            entity.Property(e => e.Numero)
                .ValueGeneratedNever()
                .HasColumnName("numero");
            entity.Property(e => e.Anarana).HasColumnName("anarana");
            entity.Property(e => e.Fanampiny).HasColumnName("fanampiny");
            entity.Property(e => e.Fokontany).HasColumnName("fokontany");
            entity.Property(e => e.Fonenana).HasColumnName("fonenana");
            entity.Property(e => e.Tel)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("tel");
        });

        modelBuilder.Entity<PaiementAdidy>(entity =>
        {
            entity.HasKey(e => e.IdpaiementAdidy).HasName("paiement_adidy_pkey");

            entity.ToTable("paiement_adidy");

            entity.Property(e => e.IdpaiementAdidy)
                .HasMaxLength(5)
                .HasDefaultValueSql("concat('PA', nextval('idpaiement_adidy'::regclass))")
                .IsFixedLength()
                .HasColumnName("idpaiement_adidy");
            entity.Property(e => e.AnneeDebut).HasColumnName("annee_debut");
            entity.Property(e => e.AnneeFin).HasColumnName("annee_fin");
            entity.Property(e => e.Dateheurepaiemet)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateheurepaiemet");
            entity.Property(e => e.Duree).HasColumnName("duree");
            entity.Property(e => e.MoisDebut).HasColumnName("mois_debut");
            entity.Property(e => e.MoisFin).HasColumnName("mois_fin");
            entity.Property(e => e.Montant)
                .HasPrecision(10, 2)
                .HasColumnName("montant");
            entity.Property(e => e.NumeroMpandray).HasColumnName("numero_mpandray");

            entity.HasOne(d => d.NumeroMpandrayNavigation).WithMany(p => p.PaiementAdidies)
                .HasForeignKey(d => d.NumeroMpandray)
                .HasConstraintName("paiement_adidy_numero_mpandray_fkey");
        });

        modelBuilder.Entity<PaiementIsantaona>(entity =>
        {
            entity.HasKey(e => e.IdpaiementIsantaona).HasName("paiement_isantaona_pkey");

            entity.ToTable("paiement_isantaona");

            entity.Property(e => e.IdpaiementIsantaona)
                .HasMaxLength(5)
                .HasDefaultValueSql("concat('PI', nextval('idpaiement_isantaona'::regclass))")
                .IsFixedLength()
                .HasColumnName("idpaiement_isantaona");
            entity.Property(e => e.AnneeDebut).HasColumnName("annee_debut");
            entity.Property(e => e.AnneeFin).HasColumnName("annee_fin");
            entity.Property(e => e.Dateheurepaiemet)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateheurepaiemet");
            entity.Property(e => e.Montant)
                .HasPrecision(10, 2)
                .HasColumnName("montant");
            entity.Property(e => e.NumeroMpandray).HasColumnName("numero_mpandray");

            entity.HasOne(d => d.NumeroMpandrayNavigation).WithMany(p => p.PaiementIsantaonas)
                .HasForeignKey(d => d.NumeroMpandray)
                .HasConstraintName("paiement_isantaona_numero_mpandray_fkey");
        });

        modelBuilder.Entity<Modele.Type>(entity =>
        {
            entity.HasKey(e => e.Idtype).HasName("type_pkey");

            entity.ToTable("type");

            entity.Property(e => e.Idtype)
                .HasMaxLength(4)
                .HasDefaultValueSql("concat('T0', nextval('idtype'::regclass))")
                .IsFixedLength()
                .HasColumnName("idtype");
            entity.Property(e => e.Nomadidy).HasColumnName("nomadidy");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Idutilisateur).HasName("utilisateurs_pkey");

            entity.ToTable("utilisateurs");

            entity.Property(e => e.Idutilisateur)
                .HasMaxLength(5)
                .HasDefaultValueSql("concat('U00', nextval('idutilisateur'::regclass))")
                .IsFixedLength()
                .HasColumnName("idutilisateur");
            entity.Property(e => e.Motdepasse).HasColumnName("motdepasse");
            entity.Property(e => e.Nomutilisateur).HasColumnName("nomutilisateur");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

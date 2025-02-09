using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APIforVIPMVPClubs;

public partial class VipclubsContext : DbContext
{
    public VipclubsContext()
    {
    }

    public VipclubsContext(DbContextOptions<VipclubsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application1> Application1s { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusApplication> StatusApplications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;userid=root;database=VIPClubs;characterset=utf8mb4", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("application1");

            entity.HasIndex(e => e.IdStatus, "Application_Status_application_FK");

            entity.HasIndex(e => e.IdApplicant, "Application_User_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfFiling)
                .HasColumnType("datetime")
                .HasColumnName("date_of_filing");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.IdApplicant).HasColumnName("idApplicant");
            entity.Property(e => e.IdStatus).HasColumnName("idStatus");
            entity.Property(e => e.Image)
                .HasColumnType("mediumblob")
                .HasColumnName("image");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.IdApplicantNavigation).WithMany(p => p.Application1s)
                .HasForeignKey(d => d.IdApplicant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Application_User_FK");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Application1s)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Application_Status_application_FK");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("club");

            entity.HasIndex(e => e.IdBoss, "Club_User_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.IdBoss).HasColumnName("idBoss");
            entity.Property(e => e.Image)
                .HasColumnType("blob")
                .HasColumnName("image");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.IdBossNavigation).WithMany(p => p.Clubs)
                .HasForeignKey(d => d.IdBoss)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Club_User_FK");

            entity.HasMany(d => d.Users).WithMany(p => p.ClubsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ClubHasUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_club_has_user_user1"),
                    l => l.HasOne<Club>().WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_club_has_user_club1"),
                    j =>
                    {
                        j.HasKey("ClubId", "UserId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("club_has_user");
                        j.HasIndex(new[] { "ClubId" }, "fk_club_has_user_club1_idx");
                        j.HasIndex(new[] { "UserId" }, "fk_club_has_user_user1_idx");
                        j.IndexerProperty<int>("ClubId").HasColumnName("club_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<StatusApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("status_application");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.IdRole, "User_Role_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first name");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last name");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnName("phone");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Role_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

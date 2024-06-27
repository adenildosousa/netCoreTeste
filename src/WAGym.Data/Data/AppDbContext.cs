using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WAGym.Data.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Functionality> Functionalities { get; set; }

    public virtual DbSet<FunctionalityProfile> FunctionalityProfiles { get; set; }

    public virtual DbSet<FunctionalityProfileLog> FunctionalityProfileLogs { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileLog> ProfileLogs { get; set; }

    public virtual DbSet<ProfileUser> ProfileUsers { get; set; }

    public virtual DbSet<ProfileUserLog> ProfileUserLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Functionality>(entity =>
        {
            entity.ToTable("Functionality");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FunctionalityProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FunctionalityProfile_ID");

            entity.ToTable("FunctionalityProfile", tb => tb.HasTrigger("Tg_FunctionalityProfileLog"));

            entity.HasOne(d => d.Functionality).WithMany(p => p.FunctionalityProfiles)
                .HasForeignKey(d => d.FunctionalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FunctionalityProfile_Functionality_ID");

            entity.HasOne(d => d.UserUpdate).WithMany(p => p.FunctionalityProfiles)
                .HasForeignKey(d => d.UserUpdateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FunctionalityProfile_UserUpdateId");

            entity.HasOne(d => d.Profile).WithMany(p => p.FunctionalityProfiles)
                .HasForeignKey(d => new { d.ProfileId, d.CompanyId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FunctionalityProfile_FunctionalityId");
        });

        modelBuilder.Entity<FunctionalityProfileLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FunctionalityProfileLog");

            entity.HasIndex(e => e.Operation, "IX_ProfileLog_Operation");

            entity.HasIndex(e => e.OperationDate, "IX_ProfileLog_OperationDate");

            entity.Property(e => e.OperationDate).HasColumnType("datetime");
            entity.Property(e => e.OperationNote)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.UserUpdate).WithMany()
                .HasForeignKey(d => d.UserUpdateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FunctionalityProfileLog_UserUpdateId");

            entity.HasOne(d => d.Profile).WithMany()
                .HasForeignKey(d => new { d.ProfileId, d.CompanyId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FunctionalityProfileLog_FunctionalityId");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CompanyId });

            entity.ToTable("Profile", tb => tb.HasTrigger("Tg_ProfileLog"));

            entity.HasIndex(e => e.StatusId, "IX_Profile_StatusId");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UserUpdate).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserUpdateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_UserUpdateId");
        });

        modelBuilder.Entity<ProfileLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProfileLog");

            entity.HasIndex(e => e.Operation, "IX_ProfileLog_Operation");

            entity.HasIndex(e => e.OperationDate, "IX_ProfileLog_OperationDate");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OperationDate).HasColumnType("datetime");
            entity.Property(e => e.OperationNote)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.UserUpdate).WithMany()
                .HasForeignKey(d => d.UserUpdateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileLog_UserUpdateId");

            entity.HasOne(d => d.Profile).WithMany()
                .HasForeignKey(d => new { d.Id, d.CompanyId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileLog_ProfileId");
        });

        modelBuilder.Entity<ProfileUser>(entity =>
        {
            entity.ToTable("ProfileUser", tb => tb.HasTrigger("Tg_ProfileUserLog"));

            entity.HasIndex(e => e.PersonId, "IX_ProfileUser_PersonId");

            entity.HasIndex(e => e.UserId, "IX_ProfileUser_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.ProfileUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileUser_UserId");

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfileUsers)
                .HasForeignKey(d => new { d.ProfileId, d.CompanyId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileUser_ProfileId");
        });

        modelBuilder.Entity<ProfileUserLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProfileUserLog");

            entity.HasIndex(e => e.Operation, "IX_ProfileLog_Operation");

            entity.HasIndex(e => e.OperationDate, "IX_ProfileLog_OperationDate");

            entity.Property(e => e.OperationDate).HasColumnType("datetime");
            entity.Property(e => e.OperationNote)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileUserLog_UserId");

            entity.HasOne(d => d.UserUpdate).WithMany()
                .HasForeignKey(d => d.UserUpdateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileUserLog_UserUpdateId");

            entity.HasOne(d => d.Profile).WithMany()
                .HasForeignKey(d => new { d.ProfileId, d.CompanyId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileUserLog_ProfileId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", tb => tb.HasTrigger("Tg_UserLog"));

            entity.HasIndex(e => e.Password, "IX_User_Password");

            entity.HasIndex(e => e.PersonId, "IX_User_PersonId");

            entity.HasIndex(e => e.StatusId, "IX_User_StatusId");

            entity.HasIndex(e => e.Username, "IX_User_UserName");

            entity.HasIndex(e => new { e.PersonId, e.Username }, "UK_User").IsUnique();

            entity.HasIndex(e => new { e.CompanyId, e.Username }, "UK_User_CompanyId_Username").IsUnique();

            entity.HasIndex(e => e.PersonId, "UK_User_PersonId").IsUnique();

            entity.HasIndex(e => e.Username, "UK_User_Username").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UserUpdate).WithMany(p => p.InverseUserUpdate)
                .HasForeignKey(d => d.UserUpdateId)
                .HasConstraintName("FK_User_UserUpdateId");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserLog");

            entity.HasIndex(e => e.Operation, "IX_ProfileLog_Operation");

            entity.HasIndex(e => e.OperationDate, "IX_ProfileLog_OperationDate");

            entity.Property(e => e.OperationDate).HasColumnType("datetime");
            entity.Property(e => e.OperationNote)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_UserLog_UserId");

            entity.HasOne(d => d.UserUpdate).WithMany()
                .HasForeignKey(d => d.UserUpdateId)
                .HasConstraintName("FK_UserLog_UserUpdateId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

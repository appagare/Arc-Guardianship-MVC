namespace GuardianshipFinancialReporting.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GuardianshipFinancialReportingDBContext : DbContext
    {
        public GuardianshipFinancialReportingDBContext()
            : base("name=GuardianshipFinancialReportingDBContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DefaultCategory> DefaultCategories { get; set; }
        public virtual DbSet<DefaultSetting> DefaultSettings { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportDetail> ReportDetails { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserCategories)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserSettings)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DefaultCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemGroup)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemParameter)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemValue)
                .IsUnicode(false);

            modelBuilder.Entity<Report>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.Report)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReportDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ReportDetail>()
                .Property(e => e.Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<UserCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<UserCategory>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.UserCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Group)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Setting)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.Suffix)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Ward)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.Ward)
                .WillCascadeOnDelete(false);
        }
    }
}

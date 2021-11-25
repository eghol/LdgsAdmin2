using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LdgsAdminAPI.DTO.db;
using LdgsAdminAPI.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
#nullable disable

namespace LdgsAdminAPI.Data
{
    public partial class LdgsAdminDBContext : DbContext
    {
        public LdgsAdminDBContext()
        {
        }

        private readonly ILogger<LdgsAdminDBContext> _logger;
        private readonly IConfiguration _config;
        private readonly string cns;
        public LdgsAdminDBContext(ILogger<LdgsAdminDBContext> logger,DbContextOptions<LdgsAdminDBContext> options, IConfiguration config) : base(options)
        {
            _config = config;
            _logger = logger;
            cns = config.GetConnectionString("LdgsAdminAPIContext");
        }


        public virtual DbSet<dbAdminCustomerAndSite> AdminCustomerAndSites { get; set; }
        public virtual DbSet<dbConfiguration> Configurations { get; set; }
        public virtual DbSet<dbCustomer> Customers { get; set; }
        public virtual DbSet<dbCustomerSite> CustomerSites { get; set; }
        public virtual DbSet<dbCustomerUser> CustomerUsers { get; set; }
        public virtual DbSet<dbEvent> Events { get; set; }
        public virtual DbSet<dbEventProp> EventProps { get; set; }
        public virtual DbSet<dbPermission> Permissions { get; set; }
        public virtual DbSet<dbPermissionType> PermissionTypes { get; set; }
        public virtual DbSet<dbSalt> Salts { get; set; }
        public virtual DbSet<dbSite> Sites { get; set; }
        public virtual DbSet<dbUser> Users { get; set; }
        public virtual DbSet<dbUserType> UserTypes { get; set; }
        //public virtual DbSet<dbViewAllUserInfo> ViewAllUserInfos { get; set; }
        //public virtual DbSet<dbViewConfigurationCustomersSite> ViewConfigurationCustomersSites { get; set; }
        //public virtual DbSet<dbViewCustomerAndSite> ViewCustomerAndSites { get; set; }
        //public virtual DbSet<dbViewUserAllInfo> ViewUserAllInfos { get; set; }

        //protected  void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ldgs-authDB-we;Integrated Security=True");
        //    }
        //}

        //protected  void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        //    modelBuilder.Entity<dbAdminCustomerAndSite>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.ToView("_adminCustomerAndSite");

        //        entity.Property(e => e.Customer).HasMaxLength(100);

        //        entity.Property(e => e.Site).HasMaxLength(100);
        //    });

        //    modelBuilder.Entity<dbConfiguration>(entity =>
        //    {
        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.Property(e => e.Value).IsRequired();

        //        entity.HasOne(d => d.CustomerSite)
        //            .WithMany(p => p.Configurations)
        //            .HasForeignKey(d => d.CustomerSiteId)
        //            .HasConstraintName("FK_Configurations_CustomerSites");
        //    });

        //    modelBuilder.Entity<dbCustomer>(entity =>
        //    {
        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(100);
        //    });

        //    modelBuilder.Entity<dbCustomerSite>(entity =>
        //    {
        //        entity.HasIndex(e => new { e.CustomerId, e.SiteId }, "IX_CustomerSites");

        //        entity.HasOne(d => d.Customer)
        //            .WithMany(p => p.CustomerSites)
        //            .HasForeignKey(d => d.CustomerId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_CustomerSites_Customers");

        //        entity.HasOne(d => d.Site)
        //            .WithMany(p => p.CustomerSites)
        //            .HasForeignKey(d => d.SiteId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_CustomerSites_Sites");
        //    });

        //    modelBuilder.Entity<dbCustomerUser>(entity =>
        //    {
        //        entity.HasIndex(e => new { e.CustomerId, e.UserId }, "IX_CustomerUser")
        //            .IsUnique();

        //        entity.HasOne(d => d.Customer)
        //            .WithMany(p => p.CustomerUsers)
        //            .HasForeignKey(d => d.CustomerId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_CustomerUser_Customers");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.CustomerUsers)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_CustomerUser_Users");
        //    });

        //    modelBuilder.Entity<dbEvent>(entity =>
        //    {
        //        entity.Property(e => e.CaseId)
        //            .HasMaxLength(50)
        //            .HasColumnName("CaseID");

        //        entity.Property(e => e.ChangeDate)
        //            .HasColumnType("datetime")
        //            .HasDefaultValueSql("(getdate())");

        //        entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

        //        entity.Property(e => e.ErrorCode).HasMaxLength(50);

        //        entity.Property(e => e.MigDate).HasColumnType("datetime");

        //        entity.Property(e => e.Model).HasMaxLength(50);

        //        entity.Property(e => e.Series).HasMaxLength(50);

        //        entity.Property(e => e.SiteId).HasColumnName("SiteID");

        //        entity.Property(e => e.TopicId)
        //            .HasMaxLength(50)
        //            .HasColumnName("TopicID");

        //        entity.HasOne(d => d.Customer)
        //            .WithMany(p => p.Events)
        //            .HasForeignKey(d => d.CustomerId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Event_Customer");

        //        entity.HasOne(d => d.Site)
        //            .WithMany(p => p.Events)
        //            .HasForeignKey(d => d.SiteId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Event_Site");
        //    });

        //    modelBuilder.Entity<dbEventProp>(entity =>
        //    {
        //        entity.Property(e => e.EventId).HasColumnName("EventID");

        //        entity.Property(e => e.Title).HasMaxLength(200);

        //        entity.HasOne(d => d.Event)
        //            .WithMany(p => p.EventProps)
        //            .HasForeignKey(d => d.EventId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_EventProps_Event");
        //    });

        //    modelBuilder.Entity<dbPermission>(entity =>
        //    {
        //        entity.Property(e => e.PermissionTypeId).HasDefaultValueSql("((1))");

        //        entity.HasOne(d => d.PermissionType)
        //            .WithMany(p => p.Permissions)
        //            .HasForeignKey(d => d.PermissionTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Permissions_PermissionTypes");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.Permissions)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Permissions_Users");
        //    });

        //    modelBuilder.Entity<dbPermissionType>(entity =>
        //    {
        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(50);
        //    });

        //    modelBuilder.Entity<dbSalt>(entity =>
        //    {
        //        entity.Property(e => e.Salt1)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .HasColumnName("Salt");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.Salts)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Salts_Users");
        //    });

        //    modelBuilder.Entity<dbSite>(entity =>
        //    {
        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(100);
        //    });

        //    modelBuilder.Entity<dbUser>(entity =>
        //    {
        //        entity.HasIndex(e => e.Username, "IX_Users")
        //            .IsUnique();

        //        entity.Property(e => e.Password)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .UseCollation("SQL_Latin1_General_CP1_CS_AS");

        //        entity.Property(e => e.UserTypeId).HasDefaultValueSql("((1))");

        //        entity.Property(e => e.Username)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.HasOne(d => d.UserType)
        //            .WithMany(p => p.Users)
        //            .HasForeignKey(d => d.UserTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Users_UserTypes");
        //    });

        //    modelBuilder.Entity<dbUserType>(entity =>
        //    {
        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(50);
        //    });

        //    modelBuilder.Entity<dbViewAllUserInfo>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.ToView("_viewAllUserInfo");

        //        entity.Property(e => e.CustomerUsersId).HasColumnName("CustomerUsers_Id");

        //        entity.Property(e => e.CustomersName)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .HasColumnName("Customers_Name");

        //        entity.Property(e => e.Password)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .UseCollation("SQL_Latin1_General_CP1_CS_AS");

        //        entity.Property(e => e.PermissionTypesName)
        //            .IsRequired()
        //            .HasMaxLength(50)
        //            .HasColumnName("PermissionTypes_Name");

        //        entity.Property(e => e.Salt)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.Property(e => e.SaltsId).HasColumnName("Salts_Id");

        //        entity.Property(e => e.UserTypesName)
        //            .IsRequired()
        //            .HasMaxLength(50)
        //            .HasColumnName("UserTypes_Name");

        //        entity.Property(e => e.Username)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.Property(e => e.UsersId).HasColumnName("Users_Id");
        //    });

        //    modelBuilder.Entity<dbViewConfigurationCustomersSite>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.ToView("view_ConfigurationCustomersSites");

        //        entity.Property(e => e.ConfigurationName)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .HasColumnName("Configuration_Name");

        //        entity.Property(e => e.CustomersName)
        //            .HasMaxLength(100)
        //            .HasColumnName("Customers_Name");

        //        entity.Property(e => e.SitesName)
        //            .HasMaxLength(100)
        //            .HasColumnName("Sites_Name");

        //        entity.Property(e => e.Value).IsRequired();
        //    });

        //    modelBuilder.Entity<dbViewCustomerAndSite>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.ToView("view_CustomerAndSite");

        //        entity.Property(e => e.Customer).HasMaxLength(100);

        //        entity.Property(e => e.Site).HasMaxLength(100);
        //    });

        //    modelBuilder.Entity<dbViewUserAllInfo>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.ToView("view_UserAllInfo");

        //        entity.Property(e => e.CustomersName)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.Property(e => e.Password)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .UseCollation("SQL_Latin1_General_CP1_CS_AS");

        //        entity.Property(e => e.PermissionType)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.Salt)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.Property(e => e.UserType)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.Username)
        //            .IsRequired()
        //            .HasMaxLength(100);

        //        entity.Property(e => e.XDescription).HasColumnName("xDescription");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
    }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    //}
}

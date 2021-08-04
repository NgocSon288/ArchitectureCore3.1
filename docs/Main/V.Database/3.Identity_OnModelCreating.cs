using System;
using eShopSolution.Data.Configurations;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Data.EF
{
    public class EShopDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration()); 

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId); 
        } 
    }
}

// # Các bước theo tedu

// - Tạo lớp [AppUser] kế thừa từ [IdentityUser<Guid>]
// - Tạo lớp [AppRole] kế thừa từ [IdentityRole<Guid>]
// - Tạo lớp [Configuration] cho 2 class trên

// - Thay vì kế thừa từ [DbContext] thì mình kế thừa từ [IdentityDbContext<AppUser,AppRole,Guid>]

// - Cấu hình các bảng của [Identity] trong phương thức [OnModelCreating]

//     <!--
//         modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
//         modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
//         modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

//         modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
//         modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
//     -->

// - Tạo SeedData cho bảng [AppUser] và [AppRole] và [IdentityUserRole]

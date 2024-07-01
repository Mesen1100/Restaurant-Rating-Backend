using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> RelationUsers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodComment> FoodComments {  get; set; }
        public DbSet<FoodRate> FoodRates { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuType> MenuTypes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceComment> PlaceComments { get; set; }
        public DbSet<PlaceRate> PlaceRates { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Food>()
                .HasMany(e => e.FoodComments)
                .WithOne(e => e.Food)
                .HasForeignKey(e => e.FoodId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Food>()
                .HasMany(e => e.FoodRates)
                .WithOne(e => e.Food)
                .HasForeignKey(e => e.FoodId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Food>()
                .HasOne(e => e.Menu)
                .WithMany(e => e.Foods)
                .HasForeignKey(e => e.MenuId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Food>()
                .HasOne(e => e.FoodType)
                .WithMany(e => e.Foods)
                .HasForeignKey(e => e.FoodTypeId)
                .OnDelete(DeleteBehavior.NoAction) 
                .IsRequired();

            builder.Entity<Menu>()
                .HasOne(e => e.Place)
                .WithMany(e => e.Menus)
                .HasForeignKey(e => e.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Menu>()
                .HasOne(e => e.MenuType)
                .WithMany(e => e.Menus)
                .HasForeignKey(e => e.MenuTypeId)
                .OnDelete(DeleteBehavior.NoAction) 
                .IsRequired();

            builder.Entity<Place>()
                .HasMany(e => e.PlaceRates)
                .WithOne(e => e.Place)
                .HasForeignKey(e => e.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Place>()
                .HasMany(e => e.PlaceComments)
                .WithOne(e => e.Place)
                .HasForeignKey(e => e.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Place>()
                .HasOne(p => p.User)
                .WithMany(u => u.Places)
                .HasForeignKey(p => p.ManagerUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Place>()
                .HasOne(e => e.City)
                .WithMany(e => e.Places)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.NoAction) 
                .IsRequired();

            builder.Entity<Place>()
                .HasOne(e => e.District)
                .WithMany(e => e.Places)
                .HasForeignKey(e => e.DistrictId)
                .OnDelete(DeleteBehavior.NoAction) 
                .IsRequired();

            builder.Entity<User>()
                .HasMany(e => e.FoodRates)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<User>()
                .HasMany(e => e.FoodComments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<User>()
                .HasMany(e => e.PlaceRates)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<User>()
                .HasMany(e => e.PlaceComments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<City>()
                .HasMany(e => e.Districts)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
           

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);
        }
    }
}

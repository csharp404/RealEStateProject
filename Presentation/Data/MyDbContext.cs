using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Data
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Hood> Hoods { get; set; }
        public DbSet<RealES> RealES { get; set; }
        public DbSet<RealESFeature> RealESFeatures { get; set; }
        public DbSet<RealESService> RealESServices { get; set; }
        public DbSet<RealESImages> RealESImages { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<RealESFeature>().HasKey(x => new { x.FeatureID, x.RealESID });
            modelBuilder.Entity<RealESService>().HasKey(x => new { x.ServiceID, x.RealESId }); // corrected RealESId to RealESID
            modelBuilder.Entity<Favorite>().HasKey(x => new { x.RealESID, x.UserID });

            modelBuilder.Entity<RealESFeature>()
                .HasOne(rf => rf.RealES)  // Assuming RealES is the navigation property
                .WithMany(r => r.RealESFeatures)  // Assuming RealESFeatures is the collection in the RealES entity
                .HasForeignKey(rf => rf.RealESID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.RealES)
                .WithMany(r => r.Favorites)
                .HasForeignKey(f => f.RealESID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.RealES)
                .WithMany(re => re.Comments)
                .HasForeignKey(c => c.RealESID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Address>()
                .HasOne(x => x.Country).WithMany(x => x.Addresses).HasForeignKey(x => x.CountryID);
            modelBuilder.Entity<Address>()
                    .HasOne(x => x.City).WithMany(x => x.Addresses).HasForeignKey(x => x.CityID);
            modelBuilder.Entity<Address>()
                    .HasOne(x => x.Hood).WithMany(x => x.Addresses).HasForeignKey(x => x.HoodID);
            modelBuilder.Entity<Address>()
                   .HasOne(a => a.RealES)
                   .WithOne(r => r.Address)
                   .HasForeignKey<RealES>(r => r.AddressID).OnDelete(DeleteBehavior.Cascade);

            





            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hood>()
                .HasOne(c => c.City)
               .WithMany(x => x.hoods)
               .HasForeignKey(x => x.CityId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
               .HasOne(c => c.RealES)
              .WithOne(x => x.Room)
              .HasForeignKey<RealES>(x => x.RoomID)
              .OnDelete(DeleteBehavior.Cascade);


        }
    }
}

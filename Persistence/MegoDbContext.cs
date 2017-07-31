using mego.Core.Models;
using Microsoft.EntityFrameworkCore; 

namespace mego.Models
{
    public class MegoDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Image> Images { get; set; }
        

        public MegoDbContext(DbContextOptions<MegoDbContext> options):
            base(options)
        {

        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderFeature>().HasKey(
                vf=> new {
                vf.OrderId, vf.FeatureId});
        }
        
    }
}

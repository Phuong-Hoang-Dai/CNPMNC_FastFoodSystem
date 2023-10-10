using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgentManager.WebApp.Models.Data
{
    public class AgentManagerDbContext : IdentityDbContext<Staff>
    {
        public AgentManagerDbContext(DbContextOptions<AgentManagerDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tao khoa cho cac bang cua Identity
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FFSProductOrder>().HasKey(x => new { x.FFSOrderId, x.FFSProductId });
            modelBuilder.Entity<FFSShipment>().HasKey(x => new { x.FFSIngredientId, x.FFSDeliveryRecievedNoteId });
			//Sua ten cac bang cua Identity
			FixNameIdentityTables(modelBuilder);
        }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<Staff>? Staffs { get; set; }

		//FastFoodSystem
		public DbSet<FFSProduct>? FFSProducts { get; set; }
		public DbSet<FFSProductOrder>? FFSProductOrders { get; set; }
		public DbSet<FFSProductCategory>? FFSProductCategories { get; set; }
		public DbSet<FFSOrder>? FFSOrders { get; set; }
        public DbSet<FFSVoucher> FFSVouchers { get; set; }
        public DbSet<FFSDeliveryRecievedNote> FFSDeliveryRecievedNotes { get; set; }
		public DbSet<FFSShipment> FFSShipments{ get; set; }
		public DbSet<FFSIngredient> FFSIngredients { get; set; }
		public DbSet<FFSCatere> FFSCateres { get; set; }
		private static void FixNameIdentityTables(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tablename = entityType.GetTableName() + "";
                if (tablename.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tablename.Substring(6));
                }
            }
        }
    }
}

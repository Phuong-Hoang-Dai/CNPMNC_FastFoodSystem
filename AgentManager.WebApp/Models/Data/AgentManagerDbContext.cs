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
            modelBuilder.Entity<DeliveryNoteDetail>().HasKey(x => new { x.ProductId, x.DeliveryNoteId });

            modelBuilder.Entity<FFSProductOrder>().HasKey(x => new { x.FFSOrderId, x.FFSProductId });
            modelBuilder.Entity<FFSShipment>().HasKey(x => new { x.FFSIngredientId, x.FFSDeliveryRecievedNoteId });
			//Sua ten cac bang cua Identity
			FixNameIdentityTables(modelBuilder);
        }

        //AgentManageSystem
        public DbSet<Agent>? Agents { get; set; }
        public DbSet<AgentCategory>? AgentCategories { get; set; }
        public DbSet<DeliveryNote>? DeliveryNotes { get; set; }
        public DbSet<DeliveryNoteDetail>? DeliveryNoteDetails { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<District>? Districts { get; set; }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductCategory>? ProductCategories { get; set; }
        public DbSet<Receipt>? Receipts { get; set; }
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
		//public DbSet<Staff>? Staffs { get; set; }
		//public DbSet<Position>? Positions { get; set; }








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

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


            //Sua ten cac bang cua Identity
            FixNameIdentityTables(modelBuilder);
        }
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

using AgentManager.WebApp.Models.Data;

namespace AgentManager.WebApp.Models
{
    public class DBHelper
    {
        AgentManagerDbContext dbContext;
        public DBHelper(AgentManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<FFSProduct> GetProducts()
        {
            return dbContext.FFSProducts.ToList();
        }

        public FFSProduct GetProductByID(string id)
        {
            return dbContext.FFSProducts.First(x => x.FFSProductId == id);
        }

        public void InsertProduct(FFSProduct sanPham)
        {
            dbContext.FFSProducts.Add(sanPham);
            dbContext.SaveChanges();
        }

        public void EditProduct(FFSProduct sanPham)
        {
            dbContext.FFSProducts.Update(sanPham);
            dbContext.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            FFSProduct sanPham = GetProductByID(id);
            dbContext.FFSProducts.Remove(sanPham);
            dbContext.SaveChanges();
        }
    }
}
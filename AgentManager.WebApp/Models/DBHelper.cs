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

        public List<Product> GetProducts()
        {
            return dbContext.Products.ToList();
        }

        public Product GetProductByID(int id)
        {
            return dbContext.Products.First(x => x.ProductId == id);
        }

        public void InsertProduct(Product sanPham)
        {
            dbContext.Products.Add(sanPham);
            dbContext.SaveChanges();
        }

        public void EditProduct(Product sanPham)
        {
            dbContext.Products.Update(sanPham);
            dbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product sanPham = GetProductByID(id);
            dbContext.Products.Remove(sanPham);
            dbContext.SaveChanges();
        }
    }
}
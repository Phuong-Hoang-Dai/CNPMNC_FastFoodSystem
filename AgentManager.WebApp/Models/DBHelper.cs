using FastFoodSystem.WebApp.Controllers;
using FastFoodSystem.WebApp.Models.Data;

namespace FastFoodSystem.WebApp.Models
{
    public class DBHelper
    {
        FastFoodSystemDbContext dbContext;
        public DBHelper(FastFoodSystemDbContext dbContext)
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

        //Staff
        public List<Staff> GetStaffs()
        {
            return dbContext.Staffs.ToList();
        }

        public Staff GetStaffByID(string id)
        {
            return dbContext.Staffs.First(x => x.Id == id);
        }

        public void InsertStaff(Staff staff)
        {
            dbContext.Staffs.Add(staff);
            dbContext.SaveChanges();
        }

        public void EditStaff(Staff staff)
        {
            dbContext.Staffs.Update(staff);
            dbContext.SaveChanges();
        }

        public void DeleteStaff(string id)
        {
            Staff staff = GetStaffByID(id);
            dbContext.Staffs.Remove(staff);
            dbContext.SaveChanges();
        }
        public Staff GetStaffByIdPosition(int id)
        {
            return dbContext.Staffs.FirstOrDefault(x => x.PositionId == id);
        }
        //Position
        public List<Position> GetPositions()
        {
            return dbContext.Positions.ToList();
        }

        public Position GetPositionByID(int id)
        {
            return dbContext.Positions.First(x => x.PositionId == id);
        }
        
        public void InsertPositions(Position position)
        {
            dbContext.Positions.Add(position);
            dbContext.SaveChanges();
        }
        public void EditPositions(Position position)
        {
            dbContext.Positions.Update(position);
            dbContext.SaveChanges();
        }

        internal void DeletePositions(int id)
        {
            Position position = GetPositionByID(id);
            dbContext.Positions.Remove(position);
            dbContext.SaveChanges();
        }

        //Voucher

        public List<FFSVoucher> GetVouchers()
        {
            return dbContext.FFSVouchers.ToList();
        }

        public FFSVoucher GetVoucherByID(string id)
        {
            return dbContext.FFSVouchers.First(x => x.FFSVoucherId == id);
        }

        public void InsertVoucher(FFSVoucher voucher)
        {
            dbContext.FFSVouchers.Add(voucher);
            dbContext.SaveChanges();
        }

        public void EditVoucher(FFSVoucher voucher)
        {
            dbContext.FFSVouchers.Update(voucher);
            dbContext.SaveChanges();
        }

        public void DeleteVoucher(string id)
        {
            FFSVoucher voucher = GetVoucherByID(id);
            dbContext.FFSVouchers.Remove(voucher);
            dbContext.SaveChanges();
        }

    }
}
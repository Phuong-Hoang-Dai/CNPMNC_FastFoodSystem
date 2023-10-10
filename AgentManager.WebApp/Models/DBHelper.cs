﻿using FastFoodSystem.WebApp.Models.Data;

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

    }
}
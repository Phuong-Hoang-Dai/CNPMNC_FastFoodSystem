using OfficeOpenXml;
using System.Collections.Generic;
using FastFoodSystem.WebApp.Models.Data;
using FastFoodSystem.WebApp.Models.ViewModel;

namespace FastFoodSystem.WebApp.Controllers.ExcelImport
{
    public class ExcelReader
    {
        public static List<FFSProduct> ImportProductsFromExcel(ExcelPackage package)
        {
            List<FFSProduct> importedProducts = new List<FFSProduct>();

            var worksheet = package.Workbook.Worksheets.First();

            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                // Kiểm tra xem ô trong cột 2 có giá trị hay không
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 2].Text))
                {
                    string productID = worksheet.Cells[row, 1].Text;
                    string productName = worksheet.Cells[row, 2].Text;
                    string cateID = worksheet.Cells[row, 3].Text;
                    string des = worksheet.Cells[row, 5].Text;
                    string img = worksheet.Cells[row, 6].Text;
                    int price;

                    if (int.TryParse(worksheet.Cells[row, 4].Text, out price))
                    {
                        // Thêm sản phẩm vào danh sách sản phẩm đã import
                        FFSProduct sanPham = new FFSProduct()
                        {
                            FFSProductId = productID,
                            Name = productName,
                            Price = price,
                            FFSProductCategoryId = cateID,
                            Desc = des,
                            Image = img,
                            // Các thông tin khác của sản phẩm
                        };
                        importedProducts.Add(sanPham);
                    }
                }
            }
            /*
             * Note Feature:
             Missing:
            - Now cant check Id exists in database, them make exception if has product with the same id in dtb
             Fix: create bool to check id of product and insert it in row: tryparse price
            - Now cant use import single producct as conflict with error null value of single form
            Fix: 2 ways: fix the strict rule null value single form or split excel import to another page (refer way 2)
             Author: Viet 28/10/2023
             */

            return importedProducts;
        }
    }
}

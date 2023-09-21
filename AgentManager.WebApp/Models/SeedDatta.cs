using AgentManager.WebApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace AgentManager.WebApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (AgentManagerDbContext? context = new AgentManagerDbContext(serviceProvider.GetRequiredService<
                    DbContextOptions<AgentManagerDbContext>>()))
            {
                if (!context.AgentCategories.Any())
                {
                    context.AddRange(
                    new AgentCategory
                    {
                        MaxDebt = 20000000
                    },
                    new AgentCategory
                    {
                        MaxDebt = 10000000
                    },
                    new AgentCategory
                    {
                        MaxDebt = 5000000
                    });
                    context.SaveChanges();
                }

                if (!context.Districts.Any())
                {
                    context.AddRange(
                    new District
                    {
                        DistrictName = "Quan 1"
                    },
                    new District
                    {
                        DistrictName = "Quan 2"
                    },
                    new District
                    {
                        DistrictName = "Quan 3"
                    },
                    new District
                    {
                        DistrictName = "Quan 4"
                    },
                    new District
                    {
                        DistrictName = "Quan 5"
                    },
                    new District
                    {
                        DistrictName = "Quan 6"
                    },
                    new District
                    {
                        DistrictName = "Quan 7"
                    },
                    new District
                    {
                        DistrictName = "Quan 8"
                    },
                    new District
                    {
                        DistrictName = "Quan 9"
                    },
                    new District
                    {
                        DistrictName = "Quan 10"
                    },
                    new District
                    {
                        DistrictName = "Quan 11"
                    },
                    new District
                    {
                        DistrictName = "Quan 12"
                    });
                    context.SaveChanges();
                }

                if (!context.Positions.Any())
                {
                    context.AddRange(
                    new Position
                    {
                        PositionName = "Nhân viên"
                    },
                    new Position
                    {
                        PositionName = "Trưởng phòng"
                    }
                    );
                    context.SaveChanges();
                }

                if (!context.Departments.Any())
                {
                    context.AddRange(
                    new Department
                    {
                        DepartmentName = "Quản lý sản phẩm"
                    },
					new Department
					{
						DepartmentName = "Quản lý nhân viên"
					},
					new Department
					{
						DepartmentName = "Quản lý Đơn hàng"
					},
					new Department
					{
						DepartmentName = "Quản lý báo cáo tài chính"
					},
					new Department
					{
						DepartmentName = "Quản lý trị hệ thống"
					},
					new Department
                    {
                        DepartmentName = "Quản lý đại lý"
                    }
                    );
                    context.SaveChanges();
                }

                if (!context.Agents.Any())
                {
                    context.AddRange(
                    new Agent
                    {
                        AgentName = "Hai Nam",
                        Address = "18 Ly Thuong Kiet P5 Q10",
                        Phone = "626-527-1165",
                        ReceptionDate = new DateTime(2023, 5, 10),
                        DistrictId = 10,
                        AgentCategoryId = 1
                    },
                    new Agent
                    {
                        AgentName = "Hong Phuong",
                        Address = "20 Tran Hung Dao P6 Q5",
                        Phone = "858-416-0537",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 5,
                        AgentCategoryId = 2
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    },
                    new Agent
                    {
                        AgentName = "Kien",
                        Address = "28 Phan Van Tri P2 Q7",
                        Phone = "212-962-7488",
                        ReceptionDate = new DateTime(2023, 2, 9),
                        DistrictId = 7,
                        AgentCategoryId = 3
                    });
                    context.SaveChanges();
                }
                Random r = new Random();
                if (!context.Staffs.Any())
                {
                    for(int i= 0; i < 10; i++)
                    {
                        context.AddRange(
                        new Staff
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = $"email{i.ToString("D3")}@example",
                            Email = $"email{i.ToString("D3")}@example",
                            SecurityStamp = Guid.NewGuid().ToString(),
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnabled = false,
                            AccessFailedCount = 0,
                            PositionId = r.Next(1,3),
                            DepartmentId = r.Next(1,3),
                            StaffName = "Staff-" + i.ToString("D3"),
                            Gender = "Nam",
                            DoB = new DateTime(r.Next(1990,2000),r.Next(1,12), r.Next(1, 30)),
                            Address = "....@#____"
                        });
                    }
                    context.SaveChanges();
                }
                if (!context.ProductCategories.Any())
                {
                    context.AddRange(
                        new ProductCategory
                        {
                            ProductCategoryName = "Sữa tươi"
                        },
                        new ProductCategory
                        {
                            ProductCategoryName = "Kem"
                        },
                        new ProductCategory
                        {
                            ProductCategoryName = "Phô mai"
                        }
                    );
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    context.AddRange(
                    new Product
                    {
                        ProductName = "Sữa tươi tách béo Vinamilk 100% Sữa Tươi",
                        Image = "",
                        Price = 10000,
                        ProductWeight = 180,
                        ItemUnit = "ml",
                        InventoryQuantity = 10000,
                        ProductCategoryId = 1
                    },
                    new Product
                    {
                        ProductName = "Sữa dinh dưỡng có đường Vinamilk A&D3",
                        Image = "",
                        Price = 7500,
                        ProductWeight = 220,
                        ItemUnit = "ml",
                        InventoryQuantity = 10000,
                        ProductCategoryId = 1
                    },
                    new Product
                    {
                        ProductName = "Sữa dinh dưỡng Vinamilk ADM Gold",
                        Image = "",
                        Price = 10000,
                        ProductWeight = 180,
                        ItemUnit = "ml",
                        InventoryQuantity = 10000,
                        ProductCategoryId = 1
                    },
                    new Product
                    {
                        ProductName = "Kem hộp trân châu đường đen Vinamilk",
                        Image = "",
                        Price = 67000,
                        ProductWeight = 275,
                        ItemUnit = "g",
                        InventoryQuantity = 7000,
                        ProductCategoryId = 2
                    },
                    new Product
                    {
                        ProductName = "Kem hộp trân châu hoàng kim phô mai Vinamilk",
                        Image = "",
                        Price = 67000,
                        ProductWeight = 275,
                        ItemUnit = "g",
                        InventoryQuantity = 6000,
                        ProductCategoryId = 2
                    },
                    new Product
                    {
                        ProductName = "Kem sầu riêng Vinamilk",
                        Image = "",
                        Price = 51000,
                        ProductWeight = 247,
                        ItemUnit = "g",
                        InventoryQuantity = 5000,
                        ProductCategoryId = 2
                    },
                    new Product
                    {
                        ProductName = "Phô mai Vinamilk",
                        Image = "",
                        Price = 29700,
                        ProductWeight = 120,
                        ItemUnit = "g",
                        InventoryQuantity = 7500,
                        ProductCategoryId = 3
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

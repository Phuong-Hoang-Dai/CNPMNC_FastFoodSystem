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

				//Add data for FastFood
				if (!context.FFSProducts.Any())
				{
					context.AddRange(
					new FFSProduct
					{
						FFSProductId = "BG001",
						Name = "BURGER MEXICANA",
						Desc = "- Burger Mexicana ( lựa chọn vị cay/ không cay)\r\n- 1 Tương ớt + 1 Tương cà",
						Price = 69000,
						FFSProductCategoryId = "BG",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "BG002",
						Name = "BURGER TEX SUPREME",
						Desc = "- Burger Tex Supreme ( lựa chọn vị cay/ không cay)\r\n- 1 Tương ớt + 1 Tương cà",
						Price = 59000,
						FFSProductCategoryId = "BG",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "BG003",
						Name = "BURGER TÔM",
						Desc = "- Burger Tôm\r\n- 1 Tương ớt + 1 Tương cà",
						Price = 490000,
						FFSProductCategoryId = "BG",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CK004",
						Name = "GÀ RÁN CÓ XƯƠNG – 1 MIẾNG",
						Desc = "- 1 Miếng gà rán có xương (lựa chọn vị cay/ không cay)\r\n- 1 Tương ớt + 1 Tương cà",
						Price = 39000,
						FFSProductCategoryId = "CK",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CK005",
						Name = "GÀ GIÒN KHÔNG XƯƠNG – 6 MIẾNG",
						Desc = "- 6 Miếng gà giòn không xương (lựa chọn vị cay/ không cay)\r\n - 1 Sốt mù tạt mật ong\r\n- 2 Tương ớt + 2 Tương cà",
						Price = 95000,
						FFSProductCategoryId = "CK",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CK006",
						Name = "KHUỶU CÁNH GÀ CAY – 6 MIẾNG",
						Desc = "- 6 Khuỷu cánh gà cay\r\n- 2 Tương ớt + 2 Tương cà",
						Price = 95000,
						FFSProductCategoryId = "CK",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CB007",
						Name = "COMBO VUI VẺ A",
						Desc = "- 2 Miếng gà rán có xương (lựa chọn vị cay/ không cay)\r\n\r\n- 1 Burger tôm (lựa chọn vị cay/ không cay)\r\n\r\n- 2 Nước ngọt\r\n\r\n- 2 Tương ớt + 2 Tương cà",
						Price = 179000,
						FFSProductCategoryId = "CB",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CB008",
						Name = "COMBO VUI VẺ B",
						Desc = "- 2 Miếng gà rán có xương (lựa chọn vị cay/ không cay)\r\n\r\n- 2 Miếng gà giòn không xương (lựa chọn vị cay/ không cay)\r\n\r\n- 2 Bánh quy bơ mật ong\r\n\r\n- 2 Nước ngọt\r\n\r\n- 2 Tương ớt + 2 Tương cà",
						Price = 194000,
						FFSProductCategoryId = "CB",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CB009",
						Name = "COMBO VUI VẺ C",
						Desc = "- 3 Miếng gà rán có xương (lựa chọn vị cay/ không cay)\r\n\r\n- 1 Khoai tây chiên phô mai\r\n\r\n- 2 Nước ngọt\r\n\r\n- 3 Tương ớt + 2 Tương cà",
						Price = 218000,
						FFSProductCategoryId = "CB",
						Image = ""
					},
					new FFSProduct
					{
						FFSProductId = "CB010",
						Name = "COMBO VUI VẺ D",
						Desc = "- 3 Miếng gà rán có xương (lựa chọn vị cay/ không cay)\r\n\r\n- 1 Burger gà cổ điển\r\n\r\n- 1 Khoai tây chiên vừa\r\n\r\n- 2 Nước ngọt\r\n\r\n- 3 Tương ớt + 2 Tương cà",
						Price = 247000,
						FFSProductCategoryId = "CB",
						Image = ""
					}
					);
					context.SaveChanges();
				}

				if (!context.FFSProductCategories.Any())
				{
					context.AddRange(
						new FFSProductCategory
						{
							FFSProductCategoryId = "BG",
							Name = "Burger"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CK",
							Name = "Các món gà rán"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CBCK2",
							Name = "Conbo gà rán (2 người)"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CBCK1",
							Name = "Conbo gà rán (1 người)"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CKSM",
							Name = "Gà giòn sốt me"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CKBT",
							Name = "Gà sốt bơ tỏi & thảo mộc"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CBGD",
							Name = "Combo gia đình & bạn bè"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CBTK",
							Name = "Combo tiết kiệm"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CBC",
							Name = "Combo cơm"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "SCKX",
							Name = "Set gà rán có xương"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "BC",
							Name = "Bánh cuộn"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CMAK",
							Name = "Các món ăn kèm"
						}
					);
					context.SaveChanges();
				}

				if (!context.FFSCateres.Any())
				{
					context.AddRange(
						new FFSCatere
						{
							FFSCatereId = "CT1",
							Name = "Thực Phẩm Hòa Phát - Công Ty TNHH Thương Mại Chế Biến Thực Phẩm Hòa Phát",
							Address = "39/4F Ấp Nam Lân, Bà Điểm, Hóc Môn, Tp. Hồ Chí Minh (TPHCM)",
							ContractId = "0975 284 283",
							PhoneNumber = "0975 284 283",
							EmailAddress = "hoaphattapioca@gmail.com"
						},
						new FFSCatere
						{
							FFSCatereId = "CT2",
							Name = "Thực Phẩm Phúc Đạt - Công Ty TNHH XNK Quốc Tế Phúc Đạt",
							Address = "83 Phan Văn Hân, P. 17, Q. Bình Thạnh, Tp. Hồ Chí Minh (TPHCM)",
							ContractId = "0931 327 379",
							PhoneNumber = "0931 327 379",
							EmailAddress = "info@phucdatfood.com"
						},
						new FFSCatere
						{
							FFSCatereId = "CT3",
							Name = "Công ty TNHH Nước giải khát SUNTORY PEPSICO Việt Nam",
							Address = "88 Đường Đồng Khởi, Quận 1, Thành phố Hồ Chí Minh",
							ContractId = "028 3821 9437",
							PhoneNumber = "028 3821 9437",
							EmailAddress = "talent.acquisition@suntorypepsico.vn"
						},
						new FFSCatere
						{
							FFSCatereId = "CT4",
							Name = "Công ty Cổ phần Masan MEATLife",
							Address = "Tầng 10, Central Plaza, 17, Đường Lê Duẩn, P.Bến Nghé, Quận 1, TP Hồ Chí Minh",
							ContractId = "028 6256 3862",
							PhoneNumber = "028 6256 3862",
							EmailAddress = "###"
						},
						new FFSCatere
						{
							FFSCatereId = "CT5",
							Name = "Công Ty Cổ Phần Đại Tân Việt",
							Address = "145 Tôn Thất Đạm, P. Bến Nghé, Q. 1, Tp. Hồ Chí Minh (TPHCM)",
							ContractId = "028 62883535",
							PhoneNumber = "028 62883535",
							EmailAddress = "contact@newviet.vn"
						}

					);
					context.SaveChanges();
				}
				if (!context.FFSIngredients.Any())
				{
					context.AddRange(
						new FFSIngredient
						{
							Name = "Chân gà",
							FFSCatereId = "CT1"
						},
						new FFSIngredient
						{
							Name = "Ức gà",
							FFSCatereId = "CT1"
						},
						new FFSIngredient
						{
							Name = "Bột chiên giòn",
							FFSCatereId = "CT2"
						},
						new FFSIngredient
						{
							Name = "Bột chiên xù",
							FFSCatereId = "CT2"
						},
						new FFSIngredient
						{
							Name = "Nước pesi",
							FFSCatereId = "CT3"
						},
						new FFSIngredient
						{
							Name = "Nước cocacola",
							FFSCatereId = "CT3"
						},
						new FFSIngredient
						{
							Name = "Nước soda",
							FFSCatereId = "CT3"
						}
						);
				}

			}
        }
    }
}

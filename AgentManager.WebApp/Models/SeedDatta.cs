using FastFoodSystem.WebApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace FastFoodSystem.WebApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (FastFoodSystemDbContext? context = new FastFoodSystemDbContext(serviceProvider.GetRequiredService<
                    DbContextOptions<FastFoodSystemDbContext>>()))
            {
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
                            //DepartmentId = r.Next(1,3),
                            StaffName = "Staff-" + i.ToString("D3"),
                            Gender = "Nam",
                            DoB = new DateTime(r.Next(1990,2000),r.Next(1,12), r.Next(1, 30)),
                            Address = "....@#____"
                        });
                    }
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
							FFSProductCategoryId = "CBC",
							Name = "Combo cơm"
						},
						new FFSProductCategory
						{
							FFSProductCategoryId = "CB",
							Name = "Combo"
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
                            FFSIngredientId = "CG",
							Name = "Chân gà",
							FFSCatereId = "CT1"
						},
						new FFSIngredient
						{
                            FFSIngredientId = "UG",
							Name = "Ức gà",
							FFSCatereId = "CT1"
						},
						new FFSIngredient
						{
                            FFSIngredientId = "BCG",
							Name = "Bột chiên giòn",
							FFSCatereId = "CT2"
						},
						new FFSIngredient
						{
                            FFSIngredientId = "BCX",
							Name = "Bột chiên xù",
							FFSCatereId = "CT2"
						},
						new FFSIngredient
						{
                            FFSIngredientId = "P",
							Name = "Nước pesi",
							FFSCatereId = "CT3"
						},
						new FFSIngredient
						{
                            FFSIngredientId = "C",
							Name = "Nước cocacola",
							FFSCatereId = "CT3"
						},
						new FFSIngredient
						{
                            FFSIngredientId = "S",
							Name = "Nước soda",
							FFSCatereId = "CT3"
						}
					);
					context.SaveChanges();
				}
                if (!context.FFSVouchers.Any())
                {
                    context.AddRange(
                    new FFSVoucher
                    {
                        FFSVoucherId = "P",
						Num = 50000,
						StartDate = DateTime.Now,
						EndDate = DateTime.Now,
						State = "VND",
						Price = 50000,
                    },
                    new FFSVoucher
                    {
                        FFSVoucherId = "P2",
                        Num = 25000,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        State = "Phần trăm",
                        Price = 20,
                    },

                    new FFSVoucher
                    {
                        FFSVoucherId = "0",
                        Num = 0,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        State = "0",
                        Price = 0,
                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

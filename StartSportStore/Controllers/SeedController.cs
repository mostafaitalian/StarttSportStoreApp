using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StartSportStore.Models;
namespace StartSportStore.Controllers
{
    public class SeedController:Controller
    {
        private ApplicationDbContext context;
        public SeedController(ApplicationDbContext con)
        {
            context = con;
        }
        public IActionResult Index()
        {
            ViewBag.Count = context.Products.Count();
            var products = context.Products.Include(p => p.Category).OrderBy(p => p.Name).Take(20);
            return View(products);
        }

        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
                context.Database
                .ExecuteSqlCommand("DROP PROCEDURE IF EXISTS CreateSeedData");
                context.Database.ExecuteSqlCommand($@"
CREATE PROCEDURE CreateSeedData
@RowCount decimal
AS
BEGIN
SET NOCOUNT ON
DECLARE @i INT = 1;
DECLARE @catId INT;
DECLARE @CatCount INT = @RowCount / 10;
DECLARE @pprice DECIMAL(5,2);
DECLARE @rprice DECIMAL(5,2);
BEGIN TRANSACTION
WHILE @i <= @CatCount
BEGIN
INSERT INTO Categories (Name, Description)
VALUES (CONCAT('Category-', @i),
'Test Data Category');
SET @catId = SCOPE_IDENTITY();
DECLARE @j INT = 1;
WHILE @j <= 10
BEGIN
SET @pprice = RAND()*(500-5+1);
SET @rprice = (RAND() * @pprice)
+ @pprice;
INSERT INTO Products (Name, CategoryId,
Price, RetailPrice,Quantity)
VALUES (CONCAT('Product', @i, '-', @j),
@catId, @pprice, @rprice,10)
SET @j = @j + 1
END
SET @i = @i + 1
END
COMMIT
END");
context.Database.BeginTransaction();
            context.Database
            .ExecuteSqlCommand($"EXEC CreateSeedData @RowCount = {count}");
            context.Database.CommitTransaction();
        }
return RedirectToAction(nameof(Index));
    }
        [HttpPost]
        public IActionResult ClearData()
        {
            context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("DELETE FROM Orders");
            context.Database.ExecuteSqlCommand("DELETE FROM Categories");
            context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult CreateProductionData()
        {
            ClearData();
            context.Categories.AddRange(new Category[]
            {
                new Category {
                    Name = "Watersports",
                    Description = "Make a splash",
                    Products = new Product[] {
                        new Product {
                            Name = "Kayak", 
                            Description = "A boat for one person",
                            RetailPrice = 275,
                            Price = 260
                        },
                        new Product {
                            Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            RetailPrice = 48.95m,
                            Price = 47.90m
                        },
                    }
                },
                new Category {
                    Name = "Soccer",
                    Description = "The World's Favorite Game",
                    Products = new Product[] {
                        new Product {
                            Name = "Soccer Ball",
                            Description = "FIFA-approved size and weight",
                            RetailPrice = 19.50m,
                            Price = 17.5m
                        },
                        new Product {
                            Name = "Corner Flags",
                            Description = "Give your playing field a professional touch",
                            RetailPrice = 34.95m,
                            Price = 32.95m
                        },
                        new Product {
                            Name = "Stadium",
                            Description = "Flat-packed 35,000-seat stadium",
                            RetailPrice = 79500,
                            Price = 75000
                        }
                    }
                },
                new Category {
                    Name = "Chess",
                    Description = "The Thinky Game",
                    Products = new Product[] {
                        new Product {
                            Name = "Thinking Cap",
                            Description = "Improve brain efficiency by 75%",
                            RetailPrice = 16,
                            Price = 14
                        },
                        new Product {
                            Name = "Unsteady Chair",
                            Description = "Secretly give your opponent a disadvantage",
                            RetailPrice = 29.95m,
                            Price = 27.70m
                        },
                        new Product {
                            Name = "Human Chess Board",
                            Description = "A fun game for the family",
                            RetailPrice = 75,
                            Price = 67
                        },
                        new Product {
                            Name = "Bling-Bling King",
                            Description = "Gold-plated, diamond-studded King",
                            RetailPrice = 1200,
                            Price = 1780
                        }
                    }
                }
            });
            context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}

using Microsoft.Extensions.Logging;
using SmartWash.API.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartWash.API.Infrastructure
{
    public class AppDbContextSeeder
    {
        public static async Task Seed(AppDbContext context, ILoggerFactory loggerFactory)
        {
            //await context.Database.BeginTransactionAsync();
            try
            {
                List<Product> products = new();

                if (await context.Products.AnyAsync() == false)
                {

                    products = Enumerable.Range(1, 6).Select(x => new Product()
                    {
                        Name = $"Product {x}",
                        Price = new Random().Next(50, 250)
                    })
                    .ToList();

                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }

                List<SalesPoint> salesPoints = new();
                if (await context.SalesPoints.AnyAsync() == false && (products.Count > 0))
                {
                    salesPoints = new List<SalesPoint>()
                    {
                        new SalesPoint()
                        {
                            Name = "Sales Point 1",
                            ProvidedProducts = new()
                            {
                                new ProvidedProduct()
                                {
                                    ProductId = products[3].Id,
                                    ProductQuantity = 20
                                },
                                new ProvidedProduct()
                                {
                                    ProductId = products[4].Id,
                                    ProductQuantity = 35
                                },
                                new ProvidedProduct()
                                {
                                    ProductId = products[5].Id,
                                    ProductQuantity = 228
                                }
                            }
                        },
                        new SalesPoint()
                        {
                            Name = "Sales Point 2",
                            ProvidedProducts = new()
                            {
                                new ProvidedProduct()
                                {
                                    ProductId = products[0].Id,
                                    ProductQuantity = 40
                                },
                                new ProvidedProduct()
                                {
                                    ProductId = products[1].Id,
                                    ProductQuantity = 62
                                },
                                new ProvidedProduct()
                                {
                                    ProductId = products[2].Id,
                                    ProductQuantity = 300
                                }
                            }
                        }
                    };

                    context.SalesPoints.AddRange(salesPoints);
                    await context.SaveChangesAsync();
                }

                if (await context.Buyers.AnyAsync() == false)
                {
                    var buyer = new Buyer()
                    {
                        Name = "Igor"
                    };
                    var buyer2 = new Buyer()
                    {
                        Name = "Dmintriy"
                    };

                    context.Buyers.Add(buyer);
                    context.Buyers.Add(buyer2);
                    await context.SaveChangesAsync();
                }

                //await context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                //await context.Database.RollbackTransactionAsync();
                var logger = loggerFactory.CreateLogger<AppDbContextSeeder>();
                logger.LogError(ex.Message);
            }
        }
    }
}

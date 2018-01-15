//using BuyingAgentBackEnd.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;

//namespace BuyingAgentBackEnd.Services
//{
//    public static class BuyingAgentContextExtensions
//    { 
//      public static void EnsureSeedDataForContext(this BuyingAgentContext context)
//      {

//            // init seed data

//                var products = new[]
//                {
//                new Product { Name = "Golden",Description = "Good",TransactionId = 4,Price = 3,Charged = 5 },
//                new Product { Name = "Pineapple",Description = "Well",TransactionId = 4,Price = 3 ,Charged = 5 },
//                new Product { Name = "Girlscout",Description = "Not Bad",TransactionId = 4,Price = 3 ,Charged = 5 },
//                new Product { Name = "Cookies",Description = "So So",TransactionId = 4,Price = 3 ,Charged = 5 }
//            };

//                var Categories = new[]
//                {
//                new Category { Name = "Baby Formula" },
//                new Category { Name = "Calsium Tablets" },
//                new Category { Name = "Protein Supplements" }
//            };

//            var transaction = new Transaction
//            {
               
//                Charged = 20,
//                CustomerId = 2,
//                PostId = 2,
//                VisitId = 2,
//                TransactionTime = new DateTime(2010, 8, 18),
//              Products = new List<Product>()
//                {
//                new Product { Name = "Golden",Description = "Good",TransactionId = 4,Price = 3,Charged = 5 },
//                new Product { Name = "Pineapple",Description = "Well",TransactionId = 4,Price = 3 ,Charged = 5 },
//                new Product { Name = "Girlscout",Description = "Not Bad",TransactionId = 4,Price = 3 ,Charged = 5 },
//                new Product { Name = "Cookies",Description = "So So",TransactionId = 4,Price = 3 ,Charged = 5 }

//                }
//            };

//            var customer = new Customer
//            {
//                CustomerSince = new DateTime(2010, 8, 18),
//                Name = "name"
//            };

//            var post = new Post
//            {
//                ExpectedTime = 20000,
//                Price = 50
//            };

//            var visit = new Visit
//            {
//                Profit = 20,
//                VisitTime = new DateTime(2010, 8, 18),
//            };
//            context.Add(customer);
//            context.Add(post);
//            context.Add(visit);
//            context.Add(transaction);

//            context.Add(new ProductCategory { Product = products[0], Category = Categories[1] });
//            context.Add(new ProductCategory { Product = products[0], Category = Categories[2] });
//            context.Add(new ProductCategory { Product = products[1], Category = Categories[0] });
//            context.Add(new ProductCategory { Product = products[2], Category = Categories[1] });
//            context.Add(new ProductCategory { Product = products[3], Category = Categories[2] });

//            context.SaveChanges();


//            var returnProducts = LoadAndDisplayProducts(context, "as added");

//            returnProducts.Add(context.Add(new Product { Name = "A2 Stage 3",Description = "goooood", TransactionId = 4, Price = 3, Charged = 5 }).Entity);

//            var newCategory1 = new Category { Name = "Unknown" };
//            var newCategory2 = new Category { Name = "Fish Oil" };

//            foreach (var product in returnProducts)
//            {
//                var oldProductCategory = product.ProductCategories.FirstOrDefault(e => e.Category.Name == "Baby Formula");
//                if (oldProductCategory != null)
//                {
//                    product.ProductCategories.Remove(oldProductCategory);
//                    product.ProductCategories.Add(new ProductCategory { Product = product, Category = newCategory1 });
//                }
//                product.ProductCategories.Add(new ProductCategory { Product = product, Category = newCategory2 });
//            }

//            context.SaveChanges();

//            LoadAndDisplayProducts(context, "after manipulation");

//        }

//        private static List<Product> LoadAndDisplayProducts(this BuyingAgentContext context, string message)
//        {
//            Debug.WriteLine($"Dumping products {message}:");

//            var products = context.Products
//                .Include(e => e.ProductCategories)
//                .ThenInclude(e => e.Category)
//                .ToList();

//            foreach (var product in products)
//            {
//                Debug.WriteLine($"  Product {product.Name}");
//                foreach (var category in product.ProductCategories.Select(e => e.Category))
//                {
//                    Debug.WriteLine($"    Category {category.Name}");
//                }
//            }

//            return products;
//        }
//    }
//}

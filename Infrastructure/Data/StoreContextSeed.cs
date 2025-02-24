﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var productsdata = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);
                if (products == null)
                {
                    return;
                }
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}

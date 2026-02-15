using System.Text.Json;
using API.Core;

namespace API.Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, IWebHostEnvironment env)
    {
        if (!context.Products.Any())
        {
            var path = Path.Combine(
                env.ContentRootPath,
                "Infrastructure",
                "Data",
                "SeedData",
                "products.json"
            );

            var productsData = await File.ReadAllTextAsync(path);

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null) return;

            context.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}

using Mapster;
using Microsoft.EntityFrameworkCore;
using StoreManagement.App.Contexts;
using StoreManagement.App.DTOs;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.App.Services.Product
{
    internal class ProductService : IProductService
    {
        public async Task CreateProduct(NewProduct product)
        {
            using ApplicationDbContext ctx = new();
            var input = product.Adapt<Models.Product>();

            await ctx.Products.AddAsync(input);
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> DeleteProduct(int id)
        {
            using ApplicationDbContext ctx = new();
            var input = await ctx.Products.Where(x => x.Id == id).ExecuteDeleteAsync();

            return input > 0;
        }

        public async Task<FrozenSet<Models.Product>> GetAllProducts()
        {
            using ApplicationDbContext ctx = new();
            var data = await ctx.Products.Include(x => x.Category).ToListAsync();

            return data.ToFrozenSet();
        }

        public async Task<Models.Product> GetProduct(int id)
        {
            using ApplicationDbContext ctx = new();
            var input = await ctx.Products.FirstOrDefaultAsync(x => x.Id == id);

            return input.Adapt<Models.Product>();
        }
    }
}

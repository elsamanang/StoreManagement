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

namespace StoreManagement.App.Services.ProductCategory
{
    internal class ProductCategoryService : IProductCategoryService
    {
        public async Task CreateProductCategory(NewProductCategory productCategory)
        {
            using ApplicationDbContext context = new();
            var input = productCategory.Adapt<Models.ProductCategory>();

            await context.ProductCategories.AddAsync(input);
            await context.SaveChangesAsync();
        }

        public async Task<FrozenSet<Models.ProductCategory>> GetProductCategories()
        {
            using ApplicationDbContext ctx = new();
            var data = await ctx.ProductCategories.ToListAsync();

            return data.ToFrozenSet();
        }

        public async Task<Models.ProductCategory> GetProductCategory(int id)
        {
            using ApplicationDbContext ctx = new();
            var data = await ctx.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);

            return data.Adapt<Models.ProductCategory>();
        }

        public async Task<FrozenSet<Models.ProductCategory>> GetProductCategoryWithProduct()
        {
            using ApplicationDbContext ctx = new();
            var data = await ctx.ProductCategories.Include(x => x.Products).ToListAsync();

            return data.ToFrozenSet();
        }

        public async Task<bool> DeleteProductCategory(int id)
        {
            using ApplicationDbContext ctx = new();
            var input = await ctx.ProductCategories.Where(x => x.Id == id).ExecuteDeleteAsync();

            return input > 0;
        }
    }
}

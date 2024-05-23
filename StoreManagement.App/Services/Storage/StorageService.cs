using Mapster;
using Microsoft.EntityFrameworkCore;
using StoreManagement.App.Contexts;
using StoreManagement.App.DTOs;
using System.Collections.Frozen;

namespace StoreManagement.App.Services.Storage
{
    internal class StorageService : IStorageService
    {
        public async Task CreateAsync(NewStorage storage)
        {
            using ApplicationDbContext context = new();

            var str = storage.Adapt<Models.Storage>();

            await context.Storages.AddAsync(str);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using ApplicationDbContext context = new();

            var entry = await context
                .Storages
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();

            return entry > 0;
        }

        public async Task<FrozenSet<Models.Storage>> GetActiveAsync()
        {
            using ApplicationDbContext context = new();

            var data = await context.Storages.Include(x => x.Product).ToListAsync();

            return data.ToFrozenSet();
        }

        public async Task<Models.Storage?> GetAsync(int id)
        {
            using ApplicationDbContext context = new();

            return await context.Storages.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

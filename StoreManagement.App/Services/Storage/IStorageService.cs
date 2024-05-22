using StoreManagement.App.DTOs;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.App.Services.Storage
{
    public interface IStorageService
    {
        Task CreateAsync(NewStorage storage);
        Task<bool> DeleteAsync(int id);
        Task<Models.Storage?> GetAsync(int id);
        Task<FrozenSet<Models.Storage>> GetActiveAsync();
    }
}

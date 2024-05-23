using StoreManagement.App.DTOs;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.App.Services.Product
{
    public interface IProductService
    {
        Task CreateProduct(NewProduct product);
        Task<bool> DeleteProduct(int id);
        Task<Models.Product> GetProduct(int id);
        Task<FrozenSet<Models.Product>> GetAllProducts();
    }
}

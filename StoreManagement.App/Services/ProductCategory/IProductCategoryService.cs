using StoreManagement.App.DTOs;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.App.Services.ProductCategory
{
    public interface IProductCategoryService
    {
        Task CreateProductCategory(NewProductCategory productCategory);
        Task<bool> DeleteProductCategory(int id);
        Task<Models.ProductCategory> GetProductCategory(int id);
        Task<FrozenSet<Models.ProductCategory>> GetProductCategories();
        Task<FrozenSet<Models.ProductCategory>> GetProductCategoryWithProduct();
    }
}

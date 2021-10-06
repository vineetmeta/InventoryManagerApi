using InventoryManager.BusinessModels.Models;
using InventoryManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManager.Interfaces
{
    public interface IProductHandler
    {
        public Task<List<ProductMaster>> GetProductListAsync();
        public Task<ProductMaster> GetProductByIdAsync(int productId);

        public Task<int> AddProduct(ProductMaster product);

        public Task<bool> DeleteProduct(int ProductId);


        public Task<bool> UpdateProduct(int ProductId, ProductMaster product);
    }
}

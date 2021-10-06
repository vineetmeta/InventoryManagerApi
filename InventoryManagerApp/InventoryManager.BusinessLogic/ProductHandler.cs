using InventoryManager.BusinessModels;
using InventoryManager.DataAccess;
using InventoryManager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManager.BusinessLogic
{
    public class ProductHandler : IProductHandler
    {
        InventoryManagerDbContext _dbContext;
        public ProductHandler(InventoryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddProduct(ProductMaster product)
        {
            _dbContext.Add(product);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
                return product.Id;

            return -1;
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            var product = await _dbContext.FindAsync<ProductMaster>(ProductId);
            if (product != null)
            {
                _ = _dbContext.Remove(product);
                var result = await this._dbContext.SaveChangesAsync();
                return result > 0;
            }
            throw new ProductNotFoundException($"INVALID_PRODUCT_ID:{ProductId}");
        }

        public async Task<ProductMaster> GetProductByIdAsync(int productId)
        {
            return await _dbContext.FindAsync<ProductMaster>(productId);
        }

        public Task<List<ProductMaster>> GetProductListAsync()
        {
            return _dbContext.ProductMasters.ToListAsync();
        }

        public async Task<bool> UpdateProduct(int ProductId, ProductMaster inproduct)
        {
            try
            {
                var product = await _dbContext.FindAsync<ProductMaster>(ProductId);
                _dbContext.Entry(product).State = EntityState.Modified;
                product.Description = inproduct.Description;
                product.ItemName = inproduct.ItemName;
                product.Price = inproduct.Price;
                _dbContext.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }
    }
}

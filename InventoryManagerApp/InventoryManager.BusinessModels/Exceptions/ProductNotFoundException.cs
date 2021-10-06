using System;

namespace InventoryManager.BusinessModels
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {

        }

    }
}

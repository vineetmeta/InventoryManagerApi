using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.BusinessModels.Models
{
    public partial class ProductItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace InventoryManager.DataAccess
{
    public partial class ProductMaster
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}

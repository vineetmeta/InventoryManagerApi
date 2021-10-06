using InventoryManager.DataAccess;
using InventoryManagerApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManager.BusinessLogic;

namespace InventoryManager.UnitTest
{
    [TestClass]
    public class InventoryControllerUnitTest
    {
        InventoryController controller;
        [TestInitialize]
        public void TestInitializer()
        {
            var options = new DbContextOptionsBuilder<InventoryManagerDbContext>()
           .UseInMemoryDatabase(databaseName: "ProductHandler").Options;

            InventoryManagerDbContext context = new InventoryManagerDbContext(options);
            ProductHandler handler = new ProductHandler(context);
            this.controller = new InventoryController(handler);
        }

        [TestMethod]
        public void TestCreateProduct()
        {
            var product = new ProductMaster();
            product.ItemName = "Pen Drive";
            product.Description = "4 GB Pendrive";
            product.Price = -11500;
            var result = controller.Post(product);
            Assert.IsNotNull(result, "product Creation Failed");

            var product1 = controller.Get(1);
            Assert.AreEqual(1, product1.Id, "Failed to get the product with Product Id");


            var product_1 = new ProductMaster();
            product.ItemName = "Hard Disk";
            product.Description = "SATA Hard disk";
            product.Price = 5000;
            result = controller.Post(product_1);

            var productList = controller.Get();
            Assert.AreEqual(2, productList.Result.Value.Count, "List all Product Failed");
        }

        [TestMethod]
        public  void TestUpdateProduct()
        {
            //Create Product
            var product = new ProductMaster();
            product.ItemName = "Pen Drive";
            product.Description = "4 GB Pendrive";
            product.Price = 1500;
            var result = controller.Post(product).GetAwaiter().GetResult();
            Assert.IsNotNull(result, "product Creation Failed");

           
            var newProduct = result.Value;
            newProduct.ItemName = "Pen Drive large";
            newProduct.Description = "8 GB Pendrive";
            newProduct.Price = 2500;

            //Update Product
            var isSuccess = controller.Put(newProduct.Id, newProduct).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(isSuccess, typeof(OkResult),  "Failed to update the Product");


            var updatedProduct = controller.Get(newProduct.Id).GetAwaiter().GetResult();
            Assert.AreEqual("Pen Drive large", updatedProduct.Value.ItemName, "Failed to update the product Name ");
            Assert.AreEqual("8 GB Pendrive", updatedProduct.Value.Description, "Failed to update the product Description");
            Assert.AreEqual(2500, updatedProduct.Value.Price, "Failed to update the product Price");
        }



        [TestMethod]
        public void TestDeleteProductValid()
        {
            var result = controller.Delete(1);
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));

        }

        [TestMethod]
        public void TestDeleteProductMissing()
        {
            var result = controller.Delete(10);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}

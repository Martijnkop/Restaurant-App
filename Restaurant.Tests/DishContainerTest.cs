using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Logic.dish;
using Restaurant.Tests.TestDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests
{
    [TestClass]
    public class DishContainerTests
    {

        #region GetAll

        [TestMethod]
        [TestCategory("GetAll")]
        public void GetAll_NoDishes_EmptyList()
        {
            DishTestDAL dal = new();

            List<Dish> dishes = new DishContainer(dal).GetAll();

            Assert.AreEqual(dishes.Count, 0);
        }

        [TestMethod]
        [TestCategory("GetAll")]
        public void GetAll_WithDishes_ReturnAll()
        {
            DishTestDAL dal = new();

            Dish d = new("testName", dal: dal);

            d.Add();

            List<Dish> dishes = new DishContainer(dal).GetAll();

            Assert.AreEqual(dishes.Count, 1);

            Assert.AreEqual(dishes[0].Name, "testName");
        }

        #endregion

        #region FindByName

        [TestMethod]
        [TestCategory("FindByName")]
        public void FindByName_NameNotExist_Null()
        {
            DishTestDAL dal = new();

            Dish shouldBeNull = new DishContainer(dal).FindByName("notExist");

            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        [TestCategory("FindByName")]
        public void FindByName_DoesExist_Return()
        {
            DishTestDAL dal = new();

            Dish d = new Dish(name: "test", dal: dal);

            d.Add(dal);

            Dish shouldExist = new DishContainer(dal).FindByName("test");

            Assert.AreEqual(shouldExist.Name, d.Name);
            Assert.AreEqual(shouldExist.Price, d.Price);
        }

        #endregion

    }
}

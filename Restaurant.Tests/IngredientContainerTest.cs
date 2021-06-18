using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Logic.ingredient;
using Restaurant.Tests.TestDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests
{
    [TestClass]
    public class IngredientContainerTest
    {

        #region GetAll

        [TestMethod]
        [TestCategory("GetAll")]
        public void GetAll_NoIngredients_EmptyList()
        {
            IngredientTestDAL dal = new();

            IngredientContainer container = new(dal);

            List<Ingredient> ingredients = container.GetAll();

            Assert.IsNotNull(ingredients);

            Assert.AreEqual(ingredients.Count, 0);
        }

        [TestMethod]
        [TestCategory("GetAll")]
        public void GetAll_WithIngredients_ReturnAll()
        {
            IngredientTestDAL dal = new();

            new Ingredient("testIngredient", dal: dal).Add();

            IngredientContainer container = new(dal);

            List<Ingredient> ingredients = container.GetAll();

            Assert.IsNotNull(ingredients);

            Assert.AreEqual(ingredients.Count, 1);
        }

        #endregion

        #region FindByName

        [TestMethod]
        [TestCategory("FindByName")]
        public void FindByName_NameNotExist_Null()
        {

        }

        #endregion
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Logic.dish;
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
    public class DishTests
    {
        #region Add

        [TestMethod]
        [TestCategory("Add")]
        public void AddDish_NoIngredients_Added()
        {
            DishTestDAL dal = new();

            Dish dish = new("testDish", 10f, dal);

            bool result = dish.Add(dal);

            Assert.IsTrue(result);

            Assert.AreEqual(dal.Dishes[0].Name, dish.Name);
        }

        [TestMethod]
        [TestCategory("Add")]
        public void AddDish_WithIngredients_Added()
        {
            IngredientTestDAL ingredientDAL = new();
            DishTestDAL dal = new();

            Ingredient ingredient = new("TestIngredient", 0, ingredientDAL);

            Dish dish = new("testDish", new List<Ingredient> { ingredient }, 10f, dal);

            bool result = dish.Add(dal);

            Assert.IsTrue(result);

            Assert.AreEqual(dal.Dishes[0].Name, dish.Name);
        }

        [TestMethod]
        [TestCategory("Add")]
        public void AddDish_ExistingName_DontAdd()
        {
            DishTestDAL dal = new();

            Dish dish1 = new("test", price: 0, dal: dal);
            Dish dish2 = new("test", price: 1, dal: dal);

            bool res1 = dish1.Add(dal);
            bool res2 = dish2.Add(dal);

            Assert.IsTrue(res1);
            Assert.IsFalse(res2);

            Assert.AreEqual(dal.Dishes[0].Price, 0);
        }

        [TestMethod]
        [TestCategory("Add")]
        public void AddDish_RoundedPrice_PriceRounded()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", price: 2.445555f, dal: dal);

            bool res = dish.Add(dal);

            Assert.IsTrue(res);
            Assert.AreEqual(dal.Dishes[0].Price, 2.45f);
        }

        [TestMethod]
        [TestCategory("Add")]
        public void AddDish_NegativePrice_DontAdd()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", price: -3.5f, dal: dal);

            bool res = dish.Add(dal);

            Assert.IsFalse(res);
        }

        #endregion

        #region GetDiet

        [TestMethod]
        [TestCategory("GetDiet")]
        public void GetDiet_Meat_0()
        {
            DishTestDAL dal = new();

            IngredientTestDAL ingredientDAL = new();

            Ingredient i1 = new("meat", 0, ingredientDAL);
            Ingredient i2 = new("vegetarian", 1, ingredientDAL);
            Ingredient i3 = new("vegan", 2, ingredientDAL);

            List<Ingredient> ingredients = new();
            ingredients.Add(i1);
            ingredients.Add(i2);
            ingredients.Add(i3);

            Dish dish = new("test", ingredients: ingredients, dal: dal);

            int res = dish.Diet;

            Assert.AreEqual(res, 0);
        }

        [TestMethod]
        [TestCategory("GetDiet")]
        public void GetDiet_Vegetarian_1()
        {
            DishTestDAL dal = new();

            IngredientTestDAL ingredientDAL = new();

            Ingredient i1 = new("vegetarian1", 1, ingredientDAL);
            Ingredient i2 = new("vegetarian2", 1, ingredientDAL);
            Ingredient i3 = new("vegan", 2, ingredientDAL);

            List<Ingredient> ingredients = new();
            ingredients.Add(i1);
            ingredients.Add(i2);
            ingredients.Add(i3);

            Dish dish = new("test", ingredients: ingredients, dal: dal);

            int res = dish.Diet;

            Assert.AreEqual(res, 1);
        }

        [TestMethod]
        [TestCategory("GetDiet")]
        public void GetDiet_Vegan_2()
        {
            IngredientTestDAL ingredientDAL = new();
            DishTestDAL dal = new();

            Ingredient i1 = new("vegan1", 2, ingredientDAL);
            Ingredient i2 = new("vegan2", 2, ingredientDAL);

            List<Ingredient> ingredients = new();
            ingredients.Add(i1);
            ingredients.Add(i2);

            Dish dish = new("test", ingredients: ingredients, dal: dal);

            int res = dish.Diet;

            Assert.AreEqual(res, 2);
        }

        [TestMethod]
        [TestCategory("GetDiet")]
        public void GetDiet_NoIngredients_Minus1()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", dal: dal);

            int res = dish.Diet;

            Assert.AreEqual(res, -1);
        }

        #endregion

        #region Update

        [TestMethod]
        [TestCategory("Update")]
        public void UpdateDish_CorrectParameters_Updated()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", dal: dal);

            dish.Add(dal);

            Dish updatedDish = new("test2", price: 10f, dal);

            bool result = updatedDish.Update("test", dal);

            Assert.IsTrue(result);

            Assert.AreEqual(dal.Dishes[0].Name, "test2");
        }

        [TestMethod]
        [TestCategory("Update")]
        public void UpdateDish_DuplicateName_DontUpdate()
        {
            DishTestDAL dal = new();

            Dish dish1 = new("test1", dal: dal);
            Dish dish2 = new("test2", dal: dal);
            Dish updatedDish = new("test1", price: 10f, dal);

            dish1.Add(dal);
            dish2.Add(dal);

            bool result = updatedDish.Update("test2", dal);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void UpdateDish_NameNotExist_DontUpdate()
        {
            DishTestDAL dal = new();

            Dish updatedDish = new("test1", price: 10f, dal);

            bool result = updatedDish.Update("test2", dal);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void UpdateDish_NegativePrice_DontUpdate()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", dal: dal);

            dish.Add(dal);

            Dish updatedDish = new("test2", price: -10f, dal);

            bool result = updatedDish.Update("test", dal);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void UpdateDish_RoundPrice_Update()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", dal: dal);

            dish.Add(dal);

            Dish updatedDish = new("test2", price: 10.0450001f, dal);

            bool result = updatedDish.Update("test", dal);

            Assert.IsTrue(result);

            Assert.AreEqual(dal.Dishes[0].Price, 10.05f);
        }

        #endregion

        #region Remove
        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveDish_CorrectName_Remove()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", dal: dal);

            dish.Add(dal);

            bool result = dish.Remove(dal);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveDish_NameNotExist_DontRemove()
        {
            DishTestDAL dal = new();

            Dish dish = new("test", dal: dal);

            bool result = dish.Remove(dal);

            Assert.IsFalse(result);
        }

        #endregion
    }
}

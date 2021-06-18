using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Factory;
using Restaurant.Interface.ingredient;
using Restaurant.Logic.ingredient;
using Restaurant.Tests.TestDAL;


namespace Restaurant.Tests
{
    [TestClass]
    public class IngredientTests
    {

        #region Add

        [TestMethod]
        [TestCategory("Add")]
        public void AddIngredient_WithCorrectParameters_Added()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient = new Ingredient("test", 0, dal);

            bool result = ingredient.Add();

            Assert.IsTrue(result);

            Assert.AreEqual(dal.TestList.Count, 1);

            Assert.AreEqual(dal.TestList[0].Name, "test");
        }

        [TestMethod]
        [TestCategory("Add")]
        public void AddIngredient_WithIncorrectDiet_DontAdd()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient = new Ingredient("testIngredient", 3, dal);

            bool result = ingredient.Add();

            Assert.IsFalse(result);

            Assert.AreEqual(dal.TestList.Count, 0);
        }

        [TestMethod]
        [TestCategory("Add")]
        public void AddIngredient_TwoWithSameName_DontAdd()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient1 = new Ingredient("testIngredient", 0, dal);
            Ingredient ingredient2 = new Ingredient("testIngredient", 1, dal);

            bool result1 = ingredient1.Add(dal);
            bool result2 = ingredient2.Add(dal);

            Assert.IsTrue(result1);
            Assert.IsFalse(result2);

            Assert.AreEqual(dal.TestList.Count, 1);

            Assert.AreEqual(dal.TestList[0].Diet, 0);
        }

        #endregion

        #region Update

        [TestMethod]
        [TestCategory("Update")]
        public void EditIngredient_WithCorrectParameters_Update()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient = new Ingredient("testIngredient", 0, dal);

            ingredient.Add();

            int ingredientID = dal.FindByName("testIngredient").Id;

            Ingredient ingredient2 = new Ingredient("newName", 1, dal);

            bool result = ingredient2.Update("testIngredient", dal);

            Assert.IsTrue(result);

            IngredientDTO i = dal.FindByName("newName");

            Assert.AreEqual(i.Id, ingredientID);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void EditIngredient_NameDoesNotExist_DontUpdate()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient = new Ingredient("newName", 1, dal);

            bool result = ingredient.Update("oldName");

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void EditIngredient_ToExistingName_DontUpdate()
        {
            IngredientTestDAL dal = new();

            Ingredient i1 = new("ing1", 0, dal);
            Ingredient i2 = new("ing2", 0, dal);
            Ingredient i3 = new("ing1", 1, dal);

            i1.Add(dal);
            i2.Add(dal);
            bool result = i3.Update(i2.Name, dal);

            Assert.IsFalse(result);
            Assert.AreEqual(dal.TestList[1].Name, i2.Name);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void EditIngredient_ToFaultyDiet_DontUpdate()
        {
            IngredientTestDAL dal = new();

            Ingredient i1 = new("ing1", 0, dal);
            Ingredient i2 = new("ing1", 3, dal);

            i1.Add(dal);
            bool result = i2.Update(i1.Name);

            Assert.IsFalse(result);
        }

        #endregion

        #region Remove

        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveIngredient_Exists_Remove()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient = new("ingredient", 0, dal);
            ingredient.Add(dal);
            bool result = ingredient.Remove(dal);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Remove")]
        public void RemoveIngredient_NotExists_DontRemove()
        {
            IngredientTestDAL dal = new();

            Ingredient ingredient = new("ingredient", 0, dal);
            bool result = ingredient.Remove(dal);

            Assert.IsFalse(result);
        }

        #endregion
    }
}

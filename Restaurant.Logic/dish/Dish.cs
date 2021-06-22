using Restaurant.Factory;
using Restaurant.Interface.dish;
using Restaurant.Interface.ingredient;
using Restaurant.Logic.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.dish
{
    public class Dish
    {
        public string Name { get; private set; }
        public float Price { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }

        private List<IngredientDTO> IngredentDTOs { get { return GetIngredientDTOs(); } }

        private IDishDAL dishDAL;

        public int Diet { get { return GetDiet(); } }

        public Dish(string name, List<Ingredient> ingredients, float price = 0, IDishDAL dal = null)
        {
            if (dal != null) this.dishDAL = dal;
            else this.dishDAL = DishFactory.CreateIDishDal();

            this.Name = name;
            this.Price = price;
            this.Ingredients = ingredients;
        }

        public Dish(string name, float price = 0, IDishDAL dal = null)
        {
            if (dal != null) this.dishDAL = dal;
            else this.dishDAL = DishFactory.CreateIDishDal();
            this.Name = name;
            this.Price = price;
        }

        public Dish(string name, List<string> ingredientNames, float price = 0f)
        {
            dishDAL = DishFactory.CreateIDishDal();

            this.Name = name;
            this.Price = price;
            this.Ingredients = new IngredientContainer().FindByNames(ingredientNames);
        }

        public bool Update(string oldName, IDishContainerDAL dal = null)
        {
            if (ExistsInDatabase(this.Name, dal) && this.Name != oldName) return false;

            if (!ExistsInDatabase(oldName, dal)) return false;

            this.Price = (float)Math.Round(this.Price, 2);
            if (this.Price < 0) return false;

            return dishDAL.Update(oldName, this.Name, this.Price, this.IngredentDTOs);
        }

        public bool Add(IDishContainerDAL dal = null)
        {
            if (ExistsInDatabase(this.Name, dal)) return false;

            this.Price = (float)Math.Round(this.Price, 2);
            if (this.Price < 0) return false;

            return dishDAL.Add(this.Name, this.Price, this.IngredentDTOs);
        }

        private bool ExistsInDatabase(string name, IDishContainerDAL dal)
        {
            Dish i = new DishContainer(dal).FindByName(name);
            return i != null;
        }

        public bool Remove(IDishContainerDAL dal = null)
        {
            if (!ExistsInDatabase(this.Name, dal)) return false;
            return dishDAL.Remove(this.Name);
        }

        public List<IngredientDTO> GetIngredientDTOs(IDishContainerDAL dal = null)
        {
            List<IngredientDTO> ingredientDTOs = new();
            if (this.Ingredients != null) foreach (Ingredient ingredient in this.Ingredients)
            {
                ingredientDTOs.Add(new IngredientDTO { Name = ingredient.Name, Diet = ingredient.Diet });
            }
            return ingredientDTOs;
        }

        public Dictionary<string, bool> GetAllIngredients()
        {
            Dictionary<string, bool> dict = new();
            if (this.Name != null) foreach (Ingredient ingredient in new DishContainer().FindByName(this.Name).Ingredients)
            {
                dict[ingredient.Name] = true;
            }

            foreach (Ingredient ingredient in new IngredientContainer().GetAll())
            {
                if (!dict.ContainsKey(ingredient.Name))
                {
                    dict[ingredient.Name] = false;
                }
            }

            return dict;
        }

        private int GetDiet()
        {
            if (this.Ingredients == null || this.Ingredients.Count == 0) return -1;
            int diet = 2;
            foreach (Ingredient i in this.Ingredients)
            {
                if (i.Diet == 0) return 0;
                if (i.Diet == 1) diet = 1;
            }

            return diet;
        }
    }
}

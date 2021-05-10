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
        public int Diet { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }

        private List<IngredientDTO> IngredentDTOs { get { return GetIngredientDTOs(); } }

        private IDishDAL dishDAL;

        public Dish(string name, float price, List<Ingredient> ingredients)
        {
            dishDAL = DishFactory.CreateIDishDal();

            this.Name = name;
            this.Price = price;
            this.Ingredients = ingredients;
        }

        public Dish(string name)
        {
            dishDAL = DishFactory.CreateIDishDal();
            this.Name = name;
        }

        public Dish(string name, float price)
        {
            dishDAL = DishFactory.CreateIDishDal();

            this.Name = name;
            this.Price = price;
        }

        public Dish(string name, float price, List<string> ingredientNames)
        {
            dishDAL = DishFactory.CreateIDishDal();

            this.Name = name;
            this.Price = price;
            this.Ingredients = new IngredientContainer().FindByNames(ingredientNames);
        }

        public void Update(string oldName)
        {
            dishDAL.Update(oldName, this.Name, this.Price, this.IngredentDTOs);
        }

        public void Add()
        {
            dishDAL.Add(this.Name, this.Price, this.IngredentDTOs);
        }
        public void Remove() => dishDAL.Remove(this.Name);

        private List<IngredientDTO> GetIngredientDTOs()
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
    }
}

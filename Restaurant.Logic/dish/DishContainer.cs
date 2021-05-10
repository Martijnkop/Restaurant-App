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
    public class DishContainer
    {
        private IDishContainerDAL dishContainerDAL;
        public DishContainer()
        {
            this.dishContainerDAL = DishFactory.CreateIDishContainerDal();
        }

        public List<Dish> GetAll()
        {
            List<Dish> dishes = new();
            List<DishDTO> dbDishes = dishContainerDAL.GetAll();

            foreach (DishDTO dbDish in dbDishes)
            {
                dishes.Add(new Dish(dbDish.Name, dbDish.Price));
            }

            return dishes;
        }

        public Dish FindByName(string name)
        {
            DishDTO dto = dishContainerDAL.FindByName(name);
            List<Ingredient> ingredients = new();

            foreach(IngredientDTO ingredientDTO in dto.Ingredients)
            {
                ingredients.Add(new Ingredient(ingredientDTO.Name, ingredientDTO.Diet));
            }

            return new Dish(dto.Name, dto.Price, ingredients);
        }
    }
}

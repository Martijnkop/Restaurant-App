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
        public DishContainer(IDishContainerDAL dal = null)
        {
            if (dal == null) this.dishContainerDAL = DishFactory.CreateIDishContainerDal();
            else dishContainerDAL = dal;
        }

        public List<Dish> GetAll()
        {
            List<Dish> dishes = new();
            List<DishDTO> dbDishes = dishContainerDAL.GetAll();

            foreach (DishDTO dbDish in dbDishes)
            {
                dishes.Add(ConvertFromDTO(dbDish));
            }

            return dishes;
        }

        public Dish FindByName(string name)
        {
            DishDTO dto = dishContainerDAL.FindByName(name);

            return ConvertFromDTO(dto);
        }

        public Dish ConvertFromDTO(DishDTO dish)
        {
            if (dish.Name == null) return null;

            List<Ingredient> ingredients = new();
            if (dish.Ingredients != null) foreach (IngredientDTO i in dish.Ingredients)
            {
                ingredients.Add(new IngredientContainer().ConvertFromDTO(i));
            }
            return new Dish(dish.Name, ingredients, dish.Price);
        }

        public List<Dish> ConvertFromDTOs(List<DishDTO> dtos)
        {
            List<Dish> dishes = new();
            if (dtos != null) foreach (DishDTO dto in dtos)
            {
                dishes.Add(ConvertFromDTO(dto));
            }

            return dishes;
        }

        public List<DishDTO> ConvertToDTOs(List<Dish> dishes)
        {
            List<DishDTO> dtos = new();
            if (dishes != null) foreach (Dish dish in dishes)
            {
                dtos.Add(ConvertToDTO(dish));
            }

            return dtos;
        }

        public DishDTO ConvertToDTO(Dish dish)
        {
            return new DishDTO { Name = dish.Name, Price = dish.Price, Ingredients = dish.GetIngredientDTOs() };
        }
    }
}

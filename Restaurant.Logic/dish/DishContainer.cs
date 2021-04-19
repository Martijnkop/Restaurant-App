using Restaurant.Factory;
using Restaurant.Interface.dish;
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
    }
}

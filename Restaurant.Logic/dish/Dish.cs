using Restaurant.Factory;
using Restaurant.Interface.dish;
using Restaurant.Logic.ingredient.models;
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

        private IDishDAL dishDAL;

        public Dish(string name, float price)
        {
            dishDAL = DishFactory.CreateIDishDal();

            this.Name = name;
            this.Price = price;
        }
    }
}

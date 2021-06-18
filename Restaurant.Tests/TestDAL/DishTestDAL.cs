using Restaurant.Interface.dish;
using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests.TestDAL
{
    public class DishTestDAL : IDishDAL, IDishContainerDAL
    {
        public List<DishDTO> Dishes = new();
        public bool Add(string name, double price, List<IngredientDTO> ingredients)
        {
            Dishes.Add(new DishDTO { Id = Dishes.Count, Name = name, Price = (float)price, Ingredients = ingredients });
            return true;
        }

        public DishDTO FindByName(string name)
        {
            foreach (DishDTO dish in Dishes)
            {
                if (dish.Name == name) return dish;
            }

            return new DishDTO();
        }

        public List<DishDTO> GetAll()
        {
            return Dishes;
        }

        public bool Remove(string name)
        {
            Dishes.RemoveAll(d => d.Name == name);
            return true;
        }

        public bool Update(string oldName, string newName, double price, List<IngredientDTO> ingredients)
        {
            int index = Dishes.IndexOf(FindByName(oldName));
            Dishes[index] = new DishDTO { Id = Dishes.ElementAt(index).Id, Name = newName, Price = (float)price, Ingredients = ingredients };
            return true;
        }
    }
}

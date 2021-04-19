using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.dish
{
    public struct DishDTO
    {
        public int Id;
        public string Name;
        public float Price;
        public List<IngredientDTO> Ingredients;
    }
}

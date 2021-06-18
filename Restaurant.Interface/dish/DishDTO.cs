using Restaurant.Interface.ingredient;
using System.Collections.Generic;

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

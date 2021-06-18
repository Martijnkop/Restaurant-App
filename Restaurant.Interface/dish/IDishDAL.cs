using Restaurant.Interface.ingredient;
using System.Collections.Generic;

namespace Restaurant.Interface.dish
{
    public interface IDishDAL
    {
        bool Add(string name, double price, List<IngredientDTO> ingredients);
        bool Remove(string name);
        bool Update(string oldName, string newName, double price, List<IngredientDTO> ingredients);
    }
}

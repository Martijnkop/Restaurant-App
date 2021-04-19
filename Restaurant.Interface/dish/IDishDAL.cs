using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.dish
{
    public interface IDishDAL
    {
        bool Add(string name, double price, List<IngredientDTO> ingredients);
        bool Remove(string name);
        bool Update(string oldName, string newName, double price, List<IngredientDTO> ingredients);
    }
}

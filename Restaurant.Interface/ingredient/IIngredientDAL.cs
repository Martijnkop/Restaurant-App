using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.ingredient
{
    public interface IIngredientDAL
    {
        bool Add(string name, int diet);
        bool Remove(string name);
        bool Update(string oldName, string newName, int newDiet);
    }
}

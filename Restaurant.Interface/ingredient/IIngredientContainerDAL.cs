using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.ingredient
{
    public interface IIngredientContainerDAL
    {
        IngredientDTO FindByName(string name);
        List<IngredientDTO> GetAll();
    }
}

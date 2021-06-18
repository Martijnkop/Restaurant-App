using System.Collections.Generic;

namespace Restaurant.Interface.ingredient
{
    public interface IIngredientContainerDAL
    {
        IngredientDTO FindByName(string name);
        List<IngredientDTO> GetAll();
    }
}

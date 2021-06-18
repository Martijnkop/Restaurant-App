using System.Collections.Generic;

namespace Restaurant.Interface.dish
{
    public interface IDishContainerDAL
    {
        DishDTO FindByName(string name);
        List<DishDTO> GetAll();
    }
}

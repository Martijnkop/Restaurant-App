using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.dish
{
    public interface IDishContainerDAL
    {
        DishDTO FindByName(string name);
        List<DishDTO> GetAll();
    }
}

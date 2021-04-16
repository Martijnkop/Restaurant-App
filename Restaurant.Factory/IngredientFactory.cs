using Restaurant.Data;
using Restaurant.Interface.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Factory
{
    public class IngredientFactory
    {
        public static IIngredientDAL CreateIIngredientDal()
        {
            return new IngredientDAL();
        }

        public static IIngredientContainerDAL CreateIIngredientContainerDal()
        {
            return new IngredientDAL();
        }
    }
}

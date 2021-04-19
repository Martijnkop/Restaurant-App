using Restaurant.Data;
using Restaurant.Interface.ingredient;

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

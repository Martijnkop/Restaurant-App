using Restaurant.Data;
using Restaurant.Interface.dish;

namespace Restaurant.Factory
{
    public class DishFactory
    {
        public static IDishDAL CreateIDishDal()
        {
            return new DishDAL();
        }

        public static IDishContainerDAL CreateIDishContainerDal()
        {
            return new DishDAL();
        }
    }
}

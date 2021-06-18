using Restaurant.Interface.dish;
using System.Collections.Generic;

namespace Restaurant.Interface.bill
{
    public interface IBillDAL
    {
        bool Add(BillDTO bill);
        bool Remove(BillDTO bill);
        bool Archive(BillDTO bill);
        void AddDishes(BillDTO bill, List<DishDTO> dishDTOs);
    }
}

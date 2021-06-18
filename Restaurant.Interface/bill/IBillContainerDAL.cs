using Restaurant.Interface.dish;
using System.Collections.Generic;

namespace Restaurant.Interface.bill
{
    public interface IBillContainerDAL
    {
        BillDTO FindByTableNumber(int tableNumber);
        List<DishDTO> GetAllOnBill(BillDTO bill);
    }
}

using Restaurant.Interface.bill;
using Restaurant.Interface.dish;
using System.Collections.Generic;

namespace Restaurant.Interface.dishBillRelation
{
    public interface IDishBillRelationDAL
    {
        List<DishDTO> GetAllOnBill(BillDTO bill);
        bool AddRange(List<DishDTO> dishes, BillDTO bill);
    }
}

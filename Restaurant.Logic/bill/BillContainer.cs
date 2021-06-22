using Restaurant.Factory;
using Restaurant.Interface.bill;
using Restaurant.Interface.dish;
using Restaurant.Logic.dish;
using Restaurant.Logic.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.bill
{
    public class BillContainer
    {
        private IBillContainerDAL dal;
        public BillContainer(IBillContainerDAL dal = null)
        {
            if (dal == null) this.dal = new BillFactory().CreateIBillContainerDAL();
            else this.dal = dal;
        }

        internal Bill ConvertFromDTO(BillDTO bill)
        {
            List<Dish> dishes = new DishContainer().ConvertFromDTOs(bill.Dishes);
            return new Bill(bill.TableNumber, bill.Status, dishes, bill.Tip, bill.Archived);
        }

        internal BillDTO ConvertToDTO(Bill bill)
        {
            List<DishDTO> dishes = new DishContainer().ConvertToDTOs(bill.Dishes);
            return new BillDTO {
                Dishes = dishes,
                Tip = bill.Tip,
                TableNumber = bill.TableNumber,
                Archived = bill.Archived,
                Status = (BillStatus)bill.Status
            };
        }

        internal Bill FindByTableNumber(int tableNumber)
        {
            BillDTO dto = dal.FindByTableNumber(tableNumber);
            List<DishDTO> dishes = dal.GetAllOnBill(dto);
            dto.Dishes = dishes;
            if (dto.TableNumber == 0) return null;
            return ConvertFromDTO(dto);
        }
    }
}

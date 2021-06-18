using Restaurant.Interface.dish;
using System.Collections.Generic;

namespace Restaurant.Interface.bill
{
    public struct BillDTO
    {
        public int ID { get; set; }
        public List<DishDTO> Dishes { get; set; }
        public int TableNumber { get; set; }
        public BillStatus Status { get; set; }
        public double Tip { get; set; }
        public bool Archived { get; set; }
    }
}

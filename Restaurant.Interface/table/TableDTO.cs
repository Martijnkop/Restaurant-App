using Restaurant.Interface.bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interface.table
{
    public struct TableDTO
    {
        public int Id;
        public int TableNumber;
        public TableStatus Status;
        public BillDTO Bill;
    }
}

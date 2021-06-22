using Restaurant.Factory;
using Restaurant.Interface.bill;
using Restaurant.Interface.table;
using Restaurant.Logic.bill;
using Restaurant.Logic.dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.table
{
    public class Table
    {
        private ITableDAL TableDAL;
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public Bill Bill { get; set; }
        public double TotalPrice { get { return GetTotalPrice(); } }

        public Table(int tableNumber, TableStatus status = TableStatus.Free, Bill bill = null, ITableDAL dal = null)
        {
            if (dal == null) this.TableDAL = new TableFactory().CreateITableDAL();
            else this.TableDAL = dal;
            this.TableNumber = tableNumber;
            this.Status = (int)status;
            this.Bill = bill;
        }

        public bool Add()
        {
            if (this.TableNumber < 1) return false;
            return TableDAL.Add(this.TableNumber);
        }

        public bool Update(int oldTableNumber)
        {
            if (this.TableNumber < 1) return false;
            return this.TableDAL.Update(oldTableNumber, this.TableNumber, this.Status);
        }

        public bool AssignGuest()
        {
            this.Status = (int)TableStatus.Taken;
            this.Bill = new Bill(this.TableNumber, BillStatus.Open);
            this.Bill.Add();
            return Update(this.TableNumber);
        }

        public bool RemoveGuest(ITableContainerDAL dal = null)
        {
            this.Status = (int)TableStatus.Free;
            if (this.Bill == null)
            {
                Table temp = new TableContainer(dal).FindByTableNumber(this.TableNumber);
                if (temp == null || temp.Bill == null) return false;
                this.Bill = temp.Bill;
            }
            this.Bill.RemoveFromTableOrArchive();
            return Update(this.TableNumber);
        }

        private double GetTotalPrice()
        {
            double sum = 0d;
            if (Bill != null)
            {
                foreach (Dish d in Bill.Dishes)
                {
                    sum += d.Price;
                }
                sum += Bill.Tip;
            }

            return sum;
        }
    }
}

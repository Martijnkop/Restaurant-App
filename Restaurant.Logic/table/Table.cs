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
        public Table(int tableNumber)
        {
            this.TableDAL = new TableFactory().CreateITableDAL();
            this.TableNumber = tableNumber;
        }
        public Table(int tableNumber, TableStatus status, Bill bill = null)
        {
            this.TableDAL = new TableFactory().CreateITableDAL();
            this.TableNumber = tableNumber;
            this.Status = (int)status;
            this.Bill = bill;
        }

        public void Add()
        {
            TableDAL.Add(this.TableNumber);
        }

        public void Update(int oldTableNumber)
        {
            this.TableDAL.Update(oldTableNumber, this.TableNumber, this.Status);
        }

        public void AssignGuest()
        {
            this.Status = (int)TableStatus.Taken;
            this.Bill = new Bill(this.TableNumber, BillStatus.Open);
            this.Bill.Add();
            Update(this.TableNumber);
        }

        internal void RemoveGuest()
        {
            this.Status = (int)TableStatus.Free;
            this.Bill.RemoveFromTableOrArchive();
            Update(this.TableNumber);
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

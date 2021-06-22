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
    public class Bill
    {
        public List<Dish> Dishes { get; } = new();
        public int TableNumber { get; }
        public int Status { get; }
        public double Tip { get; }
        public bool Archived { get; }
        
        private IBillDAL dal;

        internal Bill(int table, BillStatus status, List<Dish> dishes = null, double tip = 0d, bool archived = false, IBillDAL dal = null)
        {
            if (dal == null) dal = new BillFactory().CreateIBillDAL();
            else this.dal = dal;
            if (dishes != null) this.Dishes = dishes;
            this.TableNumber = table;
            this.Status = (int)status;
            this.Tip = tip;
            this.Archived = Archived;

            if (dal == null) dal = new BillFactory().CreateIBillDAL();
            else this.dal = dal;
        }

        internal void Add()
        {
            dal.Add(new BillContainer().ConvertToDTO(this));
        }

        internal void Update(int tableNumber)
        {
            throw new NotImplementedException();
        }

        internal void RemoveFromTableOrArchive()
        {
            if (this.Dishes.Count == 0) dal.Remove(new BillContainer().ConvertToDTO(this));
            else dal.Archive(new BillContainer().ConvertToDTO(this));
        }

        public void AddDishes(List<Dish> dishes)
        {
            dal.AddDishes(new BillContainer().ConvertToDTO(this), new DishContainer().ConvertToDTOs(dishes));
        }
    }
}

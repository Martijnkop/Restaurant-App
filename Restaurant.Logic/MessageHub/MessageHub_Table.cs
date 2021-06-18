using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Restaurant.Logic.bill;
using Restaurant.Logic.dish;
using Restaurant.Logic.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public partial class MessageHub : Hub<IMessageClient>
    {
        public async Task GetAllTables()
        {
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
        }

        public async Task AddTable(string tableNumber)
        {
            new Table(Int32.Parse(tableNumber)).Add();
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
        }

        public async Task EditTable(int oldTableNumber, string tableNumber)
        {
            new Table(Int32.Parse(tableNumber)).Update(oldTableNumber);
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
        }

        public async Task AssignGuestToTable(int tableNumber)
        {
            Table table = new TableContainer().FindByTableNumber(tableNumber);
            table.AssignGuest();
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
        }

        public async Task RemoveGuestFromTable(int tableNumber)
        {
            Table table = new TableContainer().FindByTableNumber(tableNumber);
            table.RemoveGuest();
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
        }

        public async Task CreateOrder(String s)
        {
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(s);

            List<Dish> dishes = new();
            int tableNumber = 0;

            foreach (Dictionary<string, string> dishList in order)
            {
                if (dishList["key"] == "")
                {
                    tableNumber = int.Parse(dishList["value"]);
                    continue;
                }
                for (int i = 0; i < int.Parse(dishList["value"]); i++)
                {
                    dishes.Add(new DishContainer().FindByName(dishList["key"]));
                }
            }

            new BillContainer().FindByTableNumber(tableNumber).AddDishes(dishes);
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
            await Clients.All.SendKitchenOrder(dishes);
        }

        public async Task PayForTable(int number)
        {
            Table table = new TableContainer().FindByTableNumber(number);
            table.RemoveGuest();
            await Clients.Caller.SendAllTables(new TableContainer().GetAll());
        }
    }
}

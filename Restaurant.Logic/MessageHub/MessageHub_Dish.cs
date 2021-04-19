using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public partial class MessageHub : Hub<IMessageClient>
    {
        public async Task GetAllDishes() => await Clients.Caller.SendAllDishes(new DishContainer().GetAll());
    }
}

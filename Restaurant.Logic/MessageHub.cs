using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.ingredient;
using Restaurant.Logic.SiteLayout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public class MessageHub : Hub<IMessageClient>
    {
        public async Task GetMenuItems(string accountStatus) => await Clients.Caller.SendMenuItems(MenuItem.GetMenuItems(accountStatus));

        public async Task TestConnection() => await Clients.Caller.ReturnConnected();

        public async Task AddIngredient(string name, int diet)
        {
            new IngredientLogic().Add(name, diet);
            await GetAllIngredients();
        }

        public async Task RemoveIngredient(string name)
        {
            new IngredientLogic().Remove(name);
            await GetAllIngredients();
        }
 
        public async Task GetAllIngredients() => await Clients.Caller.SendAllIngredients(new IngredientLogic().GetAll());

        // TODO
        public async Task GetAllDishes() => await Clients.Caller.SendAllDishes(false);
    }
}

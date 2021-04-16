using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.ingredient;
using Restaurant.Logic.ingredient.models;
using Restaurant.Logic.SiteLayout;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public class MessageHub : Hub<IMessageClient>
    {
        public async Task GetMenuItems(string accountStatus) => await Clients.Caller.SendMenuItems(MenuItem.GetMenuItems(accountStatus));

        public async Task TestConnection() => await Clients.Caller.ReturnConnected();

        public async Task AddIngredient(string name, int diet)
        {
            new Ingredient { Name = name, Diet = diet }.Add();
            await GetAllIngredients();
        }

        public async Task EditIngredient(string oldName, string name, int diet)
        {
            new Ingredient { Name = name, Diet = diet }.Update(oldName);
            await GetAllIngredients();
        }

        public async Task RemoveIngredient(string name)
        {
            new Ingredient { Name = name }.Remove();
            await GetAllIngredients();
        }
 
        public async Task GetAllIngredients() => await Clients.Caller.SendAllIngredients(new IngredientContainer().GetAll());

        // TODO
        public async Task GetAllDishes() => await Clients.Caller.SendAllDishes(false);
    }
}

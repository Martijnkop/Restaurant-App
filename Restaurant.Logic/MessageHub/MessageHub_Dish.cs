using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.dish;
using Restaurant.Logic.ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public partial class MessageHub : Hub<IMessageClient>
    {
        public async Task AddDish(string name, string priceString, List<string> ingredientNames)
        {
            if (float.TryParse(priceString, out float price)) {
                new Dish(name, ingredientNames, price).Add();
            }
            await GetAllDishes();
        }

        public async Task EditDish(string oldName, string newName, string priceString, List<string> ingredientNames)
        {
            if (float.TryParse(priceString, out float price))
            {
                new Dish(newName, ingredientNames, price).Update(oldName);
            }
            await GetAllDishes();
        }

        public async Task RemoveDish(string name)
        {
            new Dish(name).Remove();
            await GetAllDishes();
        }

        public async Task GetAllDishes() => await Clients.Caller.SendAllDishes(new DishContainer().GetAll());
        public async Task GetDishInfo(string name) => await Clients.Caller.SendDish(new DishContainer().FindByName(name));
        public async Task GetDishIngredients(string name) => await Clients.Caller.SendDishIngredients(new Dish(name).GetAllIngredients());
    }
}

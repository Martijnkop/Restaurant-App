using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.ingredient;
using Restaurant.Logic.ingredient.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public partial class MessageHub : Hub<IMessageClient>
    {
        public async Task AddIngredient(string name, int diet)
        {
            new Ingredient(name, diet).Add();
            await GetAllIngredients();
        }

        public async Task EditIngredient(string oldName, string name, int diet)
        {
            new Ingredient(name, diet).Update(oldName);
            await GetAllIngredients();
        }

        public async Task RemoveIngredient(string name)
        {
            new Ingredient(name).Remove();
            await GetAllIngredients();
        }

        public async Task GetAllIngredients() => await Clients.Caller.SendAllIngredients(new IngredientContainer().GetAll());
    }
}

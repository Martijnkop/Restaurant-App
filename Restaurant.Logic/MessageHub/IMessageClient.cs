using Restaurant.Logic.dish;
using Restaurant.Logic.ingredient;
using Restaurant.Logic.SiteLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public interface IMessageClient
    {
        Task ReturnConnected();
        Task SendMenuItems(List<MenuItem> items);
        Task SendAllIngredients(List<Ingredient> ingredients);
        Task SendAllDishes(List<Dish> dishes);
        Task SendDish(Dish dish);
        Task SendDishIngredients(Dictionary<string, bool> ingredients);
    }
}

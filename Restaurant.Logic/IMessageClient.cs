﻿using Restaurant.Logic.ingredient.models;
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
        Task SendAllIngredients(List<IngredientModel> ingredients);
        Task SendAllDishes(bool a);
    }
}

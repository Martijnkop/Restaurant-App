﻿using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.SiteLayout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public class MessageHub : Hub<IMessageClient>
    {
        public async Task GetMenuItems(string accountStatus)
        {
            List<MenuItem> items = new();
            items.Add(new MenuItem
            {
                Text = "Home",
                Href = "/",
                Classes = "navbar-buttons active"
            });

            switch (accountStatus)
            {
                case "guest":
                    items.Add(MenuItem("Login"));
                    items.Add(MenuItem("Register"));
                    break;
                case "admin":
                    items.Add(MenuItem("Admin", "/admin"));
                    items.Add(MenuItem("Logout"));
                    break;
                case "kitchen":
                    items.Add(MenuItem("Kitchen", "/kitchen"));
                    items.Add(MenuItem("Logout"));
                    break;
            }

            await Clients.Caller.SendMenuItems(items);
        }

        private MenuItem MenuItem(string name, string href)
        {
            var item = new MenuItem
            {
                Text = name,
                Classes = "navbar-buttons",
                Href = href
            };
            return item;
        }

        private MenuItem MenuItem(string name)
        {
            var item = new MenuItem
            {
                Text = name,
                Classes = "navbar-buttons",
                Href = $"/home/{name.ToLower()}"
            };
            return item;
        }

        public async Task TestConnection() => await Clients.Caller.ReturnConnected();
    }
}

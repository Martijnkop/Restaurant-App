using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Logic.SiteLayout
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Classes { get; set; }
        public string Href { get; set; }


        internal static List<MenuItem> GetMenuItems(string accountStatus)
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
                    items.Add(GetMenuItem("Login"));
                    items.Add(GetMenuItem("Register"));
                    break;
                case "admin":
                    items.Add(GetMenuItem("Admin", "/admin"));
                    items.Add(GetMenuItem("Logout"));
                    break;
                case "kitchen":
                    items.Add(GetMenuItem("Kitchen", "/kitchen"));
                    items.Add(GetMenuItem("Logout"));
                    break;
            }
            return items;
        }

        private static MenuItem GetMenuItem(string name, string href)
        {
            var item = new MenuItem
            {
                Text = name,
                Classes = "navbar-buttons",
                Href = href
            };
            return item;
        }

        private static MenuItem GetMenuItem(string name)
        {
            var item = new MenuItem
            {
                Text = name,
                Classes = "navbar-buttons",
                Href = $"/home/{name.ToLower()}"
            };
            return item;
        }
    }
}

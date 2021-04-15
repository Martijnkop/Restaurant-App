using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Models
{
    public class PageModel
    {
        public bool ViewHeader { get; protected set; } = true;
        public bool ViewFooter { get; protected set; } = true;
        public string Page { get; protected set; } = "";
        public string Title { get; protected set; } = "temp";
        public string Active(string page) => page.Equals(this.Page) ? "navbar-buttons active" : "navbar-buttons";
    }
}

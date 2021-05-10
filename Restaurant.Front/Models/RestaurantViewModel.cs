using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Models
{
    public class RestaurantViewModel : PageModel
    {
        public RestaurantViewModel() : base()
        {
            this.Page = "Kitchen Home";
            this.Title = "Kitchen Home";
        }
    }
}

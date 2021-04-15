using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Models.Admin
{
    public class AdminHomeModel : PageModel
    {
        public AdminHomeModel() : base()
        {
            this.Page = "Admin Home";
            this.Title = "Admin Home";
        }
    }
}

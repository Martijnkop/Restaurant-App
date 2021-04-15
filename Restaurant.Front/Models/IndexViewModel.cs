using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Models
{
    public class IndexViewModel : PageModel
    {
        public IndexViewModel() : base()
        {
            this.Page = "Home";
            this.Title = "Index";
        }
    }
}

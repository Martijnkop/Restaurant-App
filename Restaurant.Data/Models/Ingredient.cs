using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models
{
    public class DatabaseIngredient
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public byte Diet { get; internal set; }
    }
}

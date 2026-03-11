using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementAtPhatDat.Models
{
    internal class Food
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }

        public bool Status { get; set; }
    }
}

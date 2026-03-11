using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementAtPhatDat.Models
{
    internal class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID {  get; set; }
        public int FoodID { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class CartComponentViewModel
    {
        public int ItemsInCart { get; set; }
        public decimal TotalCost { get; set; }
        public string Disabled => (ItemsInCart == 0) ? "is-disabled" : "";
    }
}

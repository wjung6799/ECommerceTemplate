using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; }
        public Cart(string cartId)
        {
            BuyerId = cartId;
            Items = new List<CartItem>();
        }

    }
}

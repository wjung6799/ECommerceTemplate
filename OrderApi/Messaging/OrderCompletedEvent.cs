using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Messaging
{
    public class OrderCompletedEvent
    {
        public string BuyerId { get; set; }
        public OrderCompletedEvent(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}

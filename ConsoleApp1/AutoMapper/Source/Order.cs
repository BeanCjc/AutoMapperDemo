using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.AutoMapper.Source
{
    class Order
    {
        public Customer Customer { get; set; }
        public int IntToString { get; set; }
        public string StringToInt { get; set; }
        public string UnNecessaryField1 { get; set; }//多余的字段1
        private readonly IList<OrderLineItem> orderLineItems = new List<OrderLineItem>();
        public OrderLineItem[] GetOrderLineItems()
        {
            return orderLineItems.ToArray();
        }
        public void AddOrderLineItem(Product product, int quantity)
        {
            orderLineItems.Add(new OrderLineItem(quantity, product));
        }
        public decimal GetTotal()
        {
            return orderLineItems.Sum(t => t.GetTotal());
        }
        public decimal Total1()
        {
            return orderLineItems.Sum(t => t.GetTotal()) - 20;
        }
    }
}

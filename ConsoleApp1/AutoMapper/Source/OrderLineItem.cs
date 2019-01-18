using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.AutoMapper.Source
{
    class OrderLineItem
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public OrderLineItem(int quantity,Product product)
        {
            Quantity = quantity;
            Product = product;
        }
        public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }
    }
}

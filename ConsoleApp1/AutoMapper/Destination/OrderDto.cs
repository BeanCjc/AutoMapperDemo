using ConsoleApp1.AutoMapper.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.AutoMapper.Destination
{
    class OrderDto
    {
        public string CustomerName { get; set; }//区分大小写,普通字段是不区分大小写，但类属性最好按照原来的大小写写，(CustomerName,CustomerNAME,CUSTOMERName，customername)匹配映射，(CUSTOMERNAME)不匹配映射
        public decimal ToTaL { get; set; }
        public int Total1 { get; set; }

        public int StringToInt { get; set; }//源是string，可以显示转换成功的才可以映射成功，否则会报异常
        public string IntToString { get; set; }//源是int，可以隐式转换的也可以映射成功
        public string UnnecessaryField { get; set; }//多余的字段不会映射
    }
}

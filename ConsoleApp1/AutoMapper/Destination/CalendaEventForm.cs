using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.AutoMapper.Destination
{
    class CalendaEventForm
    {
        public DateTime EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string EventTitle { get; set; }
        public string IgnoreField { get; set; }
    }
}

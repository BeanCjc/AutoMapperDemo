using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using ConsoleApp1.AutoMapper.Source;
using ConsoleApp1.AutoMapper.Destination;
using AutoMapper.QueryableExtensions;
using ConsoleApp1.AutoMapper;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            Mapper.Initialize(config =>
            {
                //config.RecognizePrefixes("Get", "get", "GET");//默认识别源成员名称中的前缀
                config.CreateMap<Order, OrderDto>();
                config.CreateMap<CalendaEvent, CalendaEventForm>()
                  .ForMember(dest => dest.EventDate, option => option.MapFrom(source => source.EventDate))//将源EventDate映射到目的EventDate
                  .ForMember("EventHour", option => option.MapFrom(source => source.EventDate.Hour))//将EventDate的分钟映射到EventHour
                  .ForMember(dest => dest.EventMinute, option => option.MapFrom(source => source.EventDate.Minute))//将EventDate的分钟映射到EventMinute
                  .ForMember("EventTitle", option => option.MapFrom("Title"))//将Title映射到EventTitle
                  .ForMember("IgnoreField", option => option.Ignore());//忽略该字段的映射
                config.CreateMap<Source, Destination>().ForMember(dest => dest.Item2, option => option.MapFrom(source => source.Item1)).BeforeMap((s, d) => d.Item2 = s.Item1 + "before").AfterMap((s, d) => d.Item2 = s.Item1 + "after");
                config.CreateMap<Parent, ToParent>().Include<AutoMapper.Source.Child, ToParent>();
                config.CreateMap<AutoMapper.Source.Child, ToParent>().ForMember(dest => dest.ParentName, option => option.MapFrom(source => source.ChildName));
                config.CreateMap<User, UserDto>();
                config.CreateMap<Dept, UserDto>().ForMember(dest => dest.Name, option => option.Condition((s, d) => s.Id == d.Id));
            });

            #region demo1 常规映射，相同字段，或者映射同名（get+同名）方法到属性
            var order = new Order() { Customer = new Customer() { Name = "Bean" }, IntToString = 5, StringToInt = "7", UnNecessaryField1 = "这是多余的字段" };
            order.AddOrderLineItem(new Product() { Name = "product1", Price = 20 }, 6);
            var dto = Mapper.Map<Order, OrderDto>(order);
            #endregion

            #region demo2 不同字段之间的映射
            var calendaEvent = new CalendaEvent() { EventDate = DateTime.Now, Title = "Cute", IgnoreField = "Ignore" };
            var calendaEventForm = Mapper.Map<CalendaEvent, CalendaEventForm>(calendaEvent);
            #endregion

            #region demo3 集合映射
            var sources = new Source[] { new Source() { Item1 = "1" }, new Source() { Item1 = "2" }, new Source() { Item1 = "3" } };
            var sourceList = new List<Source>() { new Source() { Item1 = "4" }, new Source() { Item1 = "5" } };
            IEnumerable<Destination> destinationIEnumerable = Mapper.Map<IEnumerable<Destination>>(sources);
            Destination[] destinationArray = Mapper.Map<Source[], Destination[]>(sources);
            IEnumerable<Destination> destinationIEnumerable1 = Mapper.Map<Source[], IEnumerable<Destination>>(sources);
            ICollection<Destination> destinationICollection = Mapper.Map<Source[], ICollection<Destination>>(sources);
            IList<Destination> destinationIList = Mapper.Map<Source[], IList<Destination>>(sources);
            List<Destination> destinationList = Mapper.Map<Source[], List<Destination>>(sources);

            IEnumerable<Destination> destinationIEnumerableX = Mapper.Map<IEnumerable<Destination>>(sourceList);
            Destination[] destinationArrayX = Mapper.Map<List<Source>, Destination[]>(sourceList);
            IEnumerable<Destination> destinationIEnumerable1X = Mapper.Map<List<Source>, IEnumerable<Destination>>(sourceList);
            ICollection<Destination> destinationICollectionX = Mapper.Map<List<Source>, ICollection<Destination>>(sourceList);
            IList<Destination> destinationIListX = Mapper.Map<List<Source>, IList<Destination>>(sourceList);
            List<Destination> destinationListX = Mapper.Map<List<Source>, List<Destination>>(sourceList);
            #endregion

            #region demo4 继承映射
            var sourceArrays = new Parent[]
            {
                new Parent(){ParentName="VS"},
                new AutoMapper.Source.Child(){ChildName="VSCode",ParentName="VSCodeBaby"},
                new Parent(){ParentName="C#"}
            };
            var destinations = Mapper.Map<Parent[], List<ToParent>>(sourceArrays);

            #endregion

            var user = new User() { Id = 12, Name = "Bean" };
            var dept = new Dept() { Id = 13, DeptId = 20 };
            var userDto = ManyToOneMap.NToOneMap<UserDto>(user, dept);

            Console.WriteLine(destinationList);
            Console.ReadKey();

















            Parallel.For(0, 10, i =>
            {
                A a = A.GetInstance();
            });
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread thread = new Thread(() =>
            //      {
            //          A a = A.GetInstance();
            //      });
            //    thread.Start();
            //}
            Console.ReadKey();
            var inttime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));

            #region 
            //Console.WriteLine($"this");
            //log4net.Repository.ILoggerRepository loggerRepository = log4net.LogManager.CreateRepository("NETCOREREPOSITORY");
            //log4net.Config.BasicConfigurator.Configure(loggerRepository);//注册log4net
            //log4net.ILog log = log4net.LogManager.GetLogger(loggerRepository.Name, "netcorelog4net");
            //log.Info("asdga");
            //Console.ReadKey();
            //Child c = new Child();
            //var b = c as Base;

            //Base bb = new Base();
            //var y = bb.GetResult(1);
            //var x = b.GetResult(1);
            //var ii = 5f;
            //var result = c.GetResult(ii);
            //Console.WriteLine(result);
            //Console.WriteLine("Hello World!");
            //int i = 0;
            //var lst = new List<int>(){
            //        i++,
            //        i++,
            //        i++,
            //        i++,
            //        i++,
            //        i++,
            //        i++,
            //        i++,
            //    };
            //foreach (var item in lst)
            //{
            //    Console.WriteLine(i);
            //}
            #endregion
        }
    }

    class A
    {
        private static volatile A x;
        private static readonly object locker = new object();
        private A()
        {
            Console.WriteLine("创建实例");
        }
        public static A GetInstance()
        {
            //双重判断，减少锁的使用，提升性能
            if (x == null)
            {
                lock (locker)
                {
                    if (x == null)
                    {
                        x = new A();
                    }
                }
            }
            return x;
        }
    }

    public class Base
    {
        public float GetResult(float input)
        {
            return input + 10f;
        }
        public virtual int GetResult(int input)
        {
            return input + 1;
        }
    }
    public class Child : Base
    {
        public override int GetResult(int input)
        {
            return input + 2;
        }
    }

}

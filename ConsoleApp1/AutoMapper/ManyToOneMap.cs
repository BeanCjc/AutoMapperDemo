using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;

namespace ConsoleApp1.AutoMapper
{
    class ManyToOneMap
    {
        public static T NToOneMap<T>(params object[] sources) where T : new()
        {
            if (!sources.Any())
            {
                return default(T);
            }
            T destination = new T();
            foreach (var source in sources)
            {
                Mapper.Map(source, destination, source.GetType(), typeof(T));
            }
            return destination;
        }
    }
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Dept
    {
        public int Id { get; set; }
        public int DeptId { get; set; }
    }
    class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeptId { get; set; }
    }
}

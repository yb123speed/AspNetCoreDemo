using DapperExtensions.Mapper;
using SimpleTaskApp.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTaskApp.Dapper.Mappers
{
    public class PersonMapper : ClassMapper<Person>
    {
        public PersonMapper()
        {
            Table("AppPersons");
            AutoMap();
        }
    }
}

using Abp.Dapper.Repositories;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTaskApp.People
{
    public class PersonAppService: ITransientDependency,IPersonAppService
    {
        private readonly IDapperRepository<Person,Guid> _personDapperRepository;
        private readonly IRepository<Person,Guid> _personRepository;

        public PersonAppService(
            IRepository<Person,Guid> personRepository,
            IDapperRepository<Person,Guid> personDapperRepository)
        {
            _personRepository = personRepository;
            _personDapperRepository = personDapperRepository;
        }

        public void DoSomeStuff()
        {

            var people = _personDapperRepository.Query("select * from AppPersons");
            _personDapperRepository.Execute("Insert into AppPersons (Id,Name,CreationTime) values ('" + Guid.NewGuid() + "','Name_" + Guid.NewGuid().ToString().Substring(10) + "','" + Clock.Now.ToString() + "')");
            _personDapperRepository.Execute("UPDATE AppPersons set Name=@name where Id=@Id", new { name = "Name_" + Guid.NewGuid().ToString().Substring(10), Id = people.FirstOrDefault()?.Id });
            //var person = people.FirstOrDefault();
            //person.Name = "Yebin_" + Guid.NewGuid().ToString().Substring(15);
            //_personDapperRepository.Update(person);
            //person = _personDapperRepository.Single(people.Skip(1).Take(1).First().Id);
            //_personDapperRepository.Insert(new Person { Name= "Yebin_" + Guid.NewGuid().ToString().Substring(15),Id= Guid.NewGuid(),CreationTime=Clock.Now});
        }
    }
}

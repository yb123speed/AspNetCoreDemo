using Abp.Dapper.Repositories;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
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
        }
    }
}

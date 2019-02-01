using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTaskApp.Web
{
    public class MyClass:ITransientDependency
    {
        public IAbpSession AbpSession { get; set; }

        public MyClass()
        {
            AbpSession = NullAbpSession.Instance;
        }

        public void MyMethod()
        {
            var currentUserId = AbpSession.UserId;
            //...
        }
    }
}

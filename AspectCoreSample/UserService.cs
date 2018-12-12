using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspectCoreSample
{
    public class UserService : IUserService
    {
        public void Call()
        {
            
        }
    }

    public interface IUserService
    {
        void Call();
    }
}

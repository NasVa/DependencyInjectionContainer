using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    class Configuration
    {
        public Dictionary<Type, List<Dependency>> registeredTypes;

        public Configuration()
        {
            registeredTypes = new Dictionary<Type, List<Dependency>>();
        }

        public void RegisterType (Type Interface, Type Implementation)
        {
            //registeredTypes.Add(Interface, new List<Dependency>() { Implementation };
        }
    }
}

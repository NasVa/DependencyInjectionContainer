using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class Configuration
    {
        public Dictionary<Type, List<Dependency>> registeredTypes;

        public Configuration()
        {
            registeredTypes = new Dictionary<Type, List<Dependency>>();
        }

        public void RegisterPair<TInterface, TImplementation> (bool isSinglton)
        {
            var newRegisterPair = new Dependency(typeof(TInterface), typeof(TImplementation), isSinglton);
            registeredTypes.Add(typeof(TInterface), new List<Dependency>() { newRegisterPair });
        }

        public void RegisterPair<TInterface>(bool isSinglton)
        {
            var newRegisterPair = new Dependency(typeof(TInterface), typeof(TInterface), isSinglton);
            registeredTypes.Add(typeof(TInterface), new List<Dependency>() { newRegisterPair });
        }
    }
}

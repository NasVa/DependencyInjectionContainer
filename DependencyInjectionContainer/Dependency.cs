using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    class Dependency
    {
        public bool isSingleton;

        public Type interfaceType { get; }

        public Type implementationType { get; }

        public object instance;

        public Dependency(Type interfaceType, Type implementationType, bool isSinglton)
        {
            this.isSingleton = isSinglton;
            this.interfaceType = interfaceType;
            this.implementationType = implementationType;
            instance = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    class Dependency
    {
        public int lifeTime;

        public Type interfaceType { get; }

        public Type implementationType { get; }

        public object instance;

        public Dependency(int lifeTime, Type interfaceType, Type implementationType)
        {
            this.lifeTime = lifeTime;
            this.interfaceType = interfaceType;
            this.implementationType = implementationType;
            instance = null;
        }
    }
}

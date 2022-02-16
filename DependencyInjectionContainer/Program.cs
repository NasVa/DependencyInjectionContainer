using System;
using System.Collections.Generic;

namespace DependencyInjectionContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration configuration = new Configuration();
            Container container = new Container(configuration);
            //configuration.RegisterPair<ITestInterface1, TestNAbstractClass2>(false);
            //configuration.RegisterPair<TestNAbstractClass2>(false);
            //configuration.registeredTypes.TryGetValue(typeof(IService), out List<Dependency> types);
        }
    }
}

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
            configuration.RegisterPair<ITestInterface1, TestNonAbstractClass1>(false);
            configuration.RegisterPair<ITestInterface1, TestNonAbstractClass2>(false);

            var allImpls = container.ResolveAll<ITestInterface1>();

        }
    }
}

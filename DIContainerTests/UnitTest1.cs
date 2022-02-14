using DependencyInjectionContainer;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DIContainerTests
{
    public class Tests
    {
        public Configuration configuration;
        public Container container;
        [SetUp]
        public void Setup()
        {
            configuration = new Configuration();
            container = new Container(configuration);
        }

        [Test]
        public void ResolutionTest()
        {
            configuration.RegisterPair<TestNonAbstractClass1>(false);
            TestNonAbstractClass1 tnac1 = container.Resolve<TestNonAbstractClass1>();
            Assert.IsNotNull(tnac1);
        }

        [Test]
        public void AddNewPairTest()
        {
            configuration.RegisterPair<IService, ServiceImpl>(false);
            configuration.registeredTypes.TryGetValue(typeof(IService), out List<Dependency> types);
            Assert.AreEqual(types[0].implementationType, typeof(ServiceImpl));
        }

        [Test]
        public void InstancePerDependencyTest()
        {
            configuration.RegisterPair<TestNonAbstractClass1>(false);
            TestNonAbstractClass1 tnac1 = container.Resolve<TestNonAbstractClass1>();
            TestNonAbstractClass1 tnac2 = container.Resolve<TestNonAbstractClass1>();
            Assert.AreNotSame(tnac1, tnac2);
        }

        [Test]
        public void SingletonTest()
        {
            configuration.RegisterPair<TestNonAbstractClass1>(true);
            TestNonAbstractClass1 tnac1 = container.Resolve<TestNonAbstractClass1>();
            TestNonAbstractClass1 tnac2 = container.Resolve<TestNonAbstractClass1>();
            Assert.AreSame(tnac1, tnac2);
        }

        [Test]
        public void AbstractRegistrationTest()
        {
            configuration.RegisterPair<TestAbstractClass1>(false);
            Assert.AreEqual(configuration.registeredTypes.Count, 0);
        }

        [Test]
        public void ResolveGenericTypeTest()
        {
            configuration.RegisterPair<ITestInterface1, TestNonAbstractClass1>(false);
            configuration.RegisterPair<TestGenericClass1<ITestInterface1>, TestGenericClass1<ITestInterface1>>(false);
            Console.WriteLine(configuration.registeredTypes.Keys);
            TestGenericClass1<ITestInterface1> genericTestClass = container.Resolve<TestGenericClass1<ITestInterface1>>();
            Assert.IsNotNull(genericTestClass);
        }

        [Test]
        public void OpenGenericTest()
        {
            configuration.RegisterPair<ITestInterface1, TestNonAbstractClass1>(false);
            configuration.RegisterPair(typeof(TestGenericClass2<ITestInterface1>), typeof(TestGenericClass2<ITestInterface1>), false);

            TestGenericClass2<ITestInterface1> test = container.Resolve<TestGenericClass2<ITestInterface1>>();

            Assert.IsNotNull(test);
        }

        [Test]
        public void ResolveAllTest()
        {
            configuration.RegisterPair<ITestInterface1, TestNonAbstractClass1>(false);
            configuration.RegisterPair<ITestInterface1, TestNonAbstractClass2>(false);

            var allImpls = container.ResolveAll<ITestInterface1>();
            Assert.IsNotNull(allImpls);
        }
    }
}
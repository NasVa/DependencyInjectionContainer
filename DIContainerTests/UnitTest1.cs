using DependencyInjectionContainer;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DIContainerTests
{
    public class Tests
    {
        public Configuration configuration;
        [SetUp]
        public void Setup()
        {
            configuration = new Configuration();
        }

        [Test]
        public void AddNewPairTest()
        {
            configuration.RegisterPair<IService, ServiceImpl>(false);
            configuration.registeredTypes.TryGetValue(typeof(IService), out List<Dependency> types);
            Assert.AreEqual(types[0].implementationType, typeof(ServiceImpl));
        }

        [Test]
        public void addNewPairAsSelfTest()
        {
            configuration.RegisterPair<IService>(false);
            configuration.registeredTypes.TryGetValue(typeof(IService), out List<Dependency> types);
            Assert.AreEqual(types[0].implementationType, typeof(IService));
        }
        
        [Test]

        public void addNewPairTypesTest()
        {
            configuration.RegisterPair(typeof(Class1), typeof(Class2), false);
            configuration.registeredTypes.TryGetValue(typeof(Class1), out List<Dependency> types);
            Console.WriteLine(types[0].implementationType);
            Console.WriteLine(typeof(Class2));
            Assert.AreEqual(types[0].implementationType, typeof(Class2));

        }
    }
}
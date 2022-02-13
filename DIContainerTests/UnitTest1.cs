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
            configuration.RegisterType<IService, ServiceImpl> (false);
            configuration.registeredTypes.TryGetValue(typeof(IService), out List<Dependency> types);
            Assert.AreEqual(types[0].implementationType, typeof(ServiceImpl));
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public interface ITestInterface1 { }
    public abstract class TestAbstractClass1 : ITestInterface1 { }

    public interface ITestInterface2 { }
    public abstract class TestAbstractClass2 : ITestInterface2 { }

    public class TestNonAbstractClass1 : ITestInterface1 { }

    public class TestAbstractInheritance : TestAbstractClass1 { }

    public class TestNonAbstractClass2 : ITestInterface1 { }

    public class TestNonAbstracClass3 : ITestInterface2
    {
        public TestAbstractClass2 test;
        public TestNonAbstracClass3(TestAbstractClass2 ntest)
        {
            test = ntest;
        }
    }

    public class TestGenericClass1<T>
    {
        T type;
    }

    public class TestGenericClass2<T> where T : class
    {
        public T _type;

        public TestGenericClass2(T type)
        {
            _type = type;
        }

    }
}

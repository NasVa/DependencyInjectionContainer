using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainerTests
{
    public interface ITestInterface1 { }
    public abstract class TestAbstractClass1 : ITestInterface1 { }

    public interface ITestInterface2 { }
    public class TestNAbstractClass1 : ITestInterface1 {
        public ITestInterface2 test2 { get; set; }

        public TestNAbstractClass1(ITestInterface2 ntest)
        {
            test2 = ntest; 
        }
    }

    public class TestNonAbstracClass2 : ITestInterface2
    {
        public ITestInterface1 test { get; set; }
        public TestNonAbstracClass2(ITestInterface1 ntest)
        {
            test = ntest;
        }
    }

    public class TestNonAbstractClass1 : ITestInterface1 { }

    public class TestAbstractInheritance : TestAbstractClass1 { }


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

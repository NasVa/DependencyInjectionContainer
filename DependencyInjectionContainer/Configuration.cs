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
            if (!typeof(TImplementation).IsInterface && !typeof(TImplementation).IsAbstract && typeof(TInterface).IsAssignableFrom(typeof(TImplementation)))
            {
                var newRegisterPair = new Dependency(typeof(TInterface), typeof(TImplementation), isSinglton);
                if (!registeredTypes.TryGetValue(typeof(TInterface), out List<Dependency> types))
                {
                    registeredTypes.Add(typeof(TInterface), new List<Dependency>() { newRegisterPair });
                }
                else
                {
                    Console.WriteLine($"TInterface {typeof(TInterface).Name} is already registered.");
                }
            }
            else
            {
                Console.WriteLine($"The pair <{typeof(TInterface).Name}, {typeof(TImplementation).Name}> can't be registered");
            }
        }

        public void RegisterPair<TInterface>(bool isSinglton)
        {
            if (!typeof(TInterface).IsInterface && !typeof(TInterface).IsAbstract && typeof(TInterface).IsAssignableFrom(typeof(TInterface)))
            {
                var newRegisterPair = new Dependency(typeof(TInterface), typeof(TInterface), isSinglton);
                if (!registeredTypes.TryGetValue(typeof(TInterface), out List<Dependency> types))
                {
                    registeredTypes.Add(typeof(TInterface), new List<Dependency>() { newRegisterPair });
                }
                else
                {
                    Console.WriteLine($"TInterface {typeof(TInterface).Name} is already registered.");
                }
            }
            else
            {
                Console.WriteLine($"The pair <{typeof(TInterface).Name}, {typeof(TInterface).Name}> can't be registered");
            }
            if (typeof(TInterface).IsAssignableFrom(typeof(TInterface)))
            {
                Console.WriteLine("IsAssignableFrom problem");
            }

        }

        public void RegisterPair(Type TInterface, Type TImplementation, bool isSinglton)
        {
            if (!TImplementation.IsInterface && !TImplementation.IsAbstract && TInterface.IsAssignableFrom(TImplementation))
            {
                var newRegisterPair = new Dependency(TInterface, TImplementation, isSinglton);
                if (!registeredTypes.TryGetValue(TInterface, out List<Dependency> types))
                {
                    registeredTypes.Add(TInterface, new List<Dependency>() { newRegisterPair });
                }
                else
                {
                    Console.WriteLine($"TInterface {TInterface.Name} is already registered.");
                }
            }
            else
            {
                Console.WriteLine($"The pair <{TInterface.Name}, {TInterface.Name}> can't be registered");
            }
            if (!TInterface.IsAssignableFrom(TImplementation))
            {
                Console.WriteLine("IsAssignableFrom problem");
            }
            else
            {
                Console.WriteLine("IsAssignableFrom not problem");
            }
        }
    }
}

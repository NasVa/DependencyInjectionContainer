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

        public void RegisterPair<TInterface, TImplementation> (bool isSingleton)
        {
            if (!typeof(TImplementation).IsInterface && !typeof(TImplementation).IsAbstract && typeof(TInterface).IsAssignableFrom(typeof(TImplementation)))
            {
                var newRegisterPair = new Dependency(typeof(TInterface), typeof(TImplementation), isSingleton);
                if (!registeredTypes.TryGetValue(typeof(TInterface), out List<Dependency> types))
                {
                    registeredTypes.Add(typeof(TInterface), new List<Dependency>() { newRegisterPair });
                }
                else
                {
                    if (!types.Contains(newRegisterPair))
                    {
                        types.Add(newRegisterPair);
                    }
                    else
                    {
                        Console.WriteLine($"TInterface {typeof(TImplementation).Name} is already implements by {typeof(TInterface).Name}.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"The pair <{typeof(TInterface).Name}, {typeof(TImplementation).Name}> can't be registered");
            }
        }

        public void RegisterPair<TInterface>(bool isSingleton)
        {
            if (!typeof(TInterface).IsInterface && !typeof(TInterface).IsAbstract && typeof(TInterface).IsAssignableFrom(typeof(TInterface)))
            {
                var newRegisterPair = new Dependency(typeof(TInterface), typeof(TInterface), isSingleton);
                if (!registeredTypes.TryGetValue(typeof(TInterface), out List<Dependency> types))
                {
                    registeredTypes.Add(typeof(TInterface), new List<Dependency>() { newRegisterPair });
                }
                else
                {
                    if (!types.Contains(newRegisterPair))
                    {
                        types.Add(newRegisterPair);
                    }
                    else {
                        Console.WriteLine($"TInterface {typeof(TInterface).Name} is already implements by {typeof(TInterface).Name}.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"The pair <{typeof(TInterface).Name}, {typeof(TInterface).Name}> can't be registered");
            }

        }

        public void RegisterPair(Type TInterface, Type TImplementation, bool isSingleton)
        {
            if (!TImplementation.IsInterface && !TImplementation.IsAbstract && TInterface.IsAssignableFrom(TImplementation))
            {
                var newRegisterPair = new Dependency(TInterface, TImplementation, isSingleton);
                if (!registeredTypes.TryGetValue(TInterface, out List<Dependency> types))
                {
                    registeredTypes.Add(TInterface, new List<Dependency>() { newRegisterPair });
                }
                else
                {
                    if (!types.Contains(newRegisterPair))
                    {
                        types.Add(newRegisterPair);
                    }
                    else
                    {
                        Console.WriteLine($"TInterface {TImplementation.Name} is already implements by {TInterface.Name}.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"The pair <{TInterface.Name}, {TInterface.Name}> can't be registered");
            }
        }

        public Dependency GetImplementation(Type TInterface)
        {
            return (registeredTypes.TryGetValue(TInterface, out var list)) ? list.Last() : null;
        }

        public IEnumerable<Dependency> GetAllImplementations(Type _interface)
        {
            if (registeredTypes.TryGetValue(_interface, out List<Dependency> typesAlreadyRegistered))
            {
                return typesAlreadyRegistered;
            }
            else
            {
                return null;
            }
        }
    }
}

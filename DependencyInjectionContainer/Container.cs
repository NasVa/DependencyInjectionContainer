using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class Container
    {
        public Configuration configuration;

        public Stack<Type> stack;
        public Container(Configuration configuration)
        {
            this.configuration = configuration;
            this.stack = new Stack<Type>();
        }

        public T Resolve<T>() where T : class
        {
            var type = typeof(T);
            var curType = type;
            Dependency dependency = configuration.GetImplementation(type);
            if(type.IsGenericType && dependency == null)
            {
                dependency = configuration.GetImplementation(type);
            }
            else
            {
                dependency = configuration.GetImplementation(type);
            }

            if(dependency == null)
            {
                throw new Exception("No such type dependency");
            }

            return (T)GetInstance(dependency, curType);
        }

        public object GetInstance(Dependency dependency, Type curtype)
        {
            if (dependency.isSingleton)
            {
                if(dependency.instance == null)
                {
                    dependency.instance = Instantiate(dependency.interfaceType, curtype);
                }
                return dependency.instance;
            }
            else
            {
                object instance = Instantiate(dependency.interfaceType, curtype);
                return instance;
            }
        }

        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            return (IEnumerable<T>)InstantiateEnumerable(typeof(T), typeof(T));
        }

        private object InstantiateEnumerable(Type type, Type currType)
        {
            Dependency dependency = configuration.GetImplementation(type);
            if (dependency != null)
            {
                IList collection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                IEnumerable<Dependency> registeredTypes = configuration.GetAllImplementations(type);
                foreach (Dependency item in registeredTypes)
                {
                    collection.Add(GetInstance(item, currType));
                }
                return collection;
            }
            else
            {
                throw new Exception("No such type registered");
            }
        }


        public object Instantiate(Type type, Type curtype)
        {
            Dependency dependency = configuration.GetImplementation(type);
            if (dependency != null)
            {
                if (!stack.Contains(dependency.interfaceType))
                {
                    stack.Push(dependency.interfaceType);
                    Type instanceType = dependency.implementationType;
                    if (instanceType.IsGenericTypeDefinition)
                    {
                        instanceType = instanceType.MakeGenericType(curtype.GenericTypeArguments);
                    }
                    ConstructorInfo[] constructors = instanceType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).ToArray();

                    int currentConstructor = 1;
                    bool isCreated = false;
                    object result = null;
                    while (!isCreated && currentConstructor <= constructors.Length)
                    {
                        try
                        {
                            ConstructorInfo constructorInfo = constructors[currentConstructor - 1];
                            object[] constructorParam = GetConstructorParam(constructorInfo, curtype);
                            result = Activator.CreateInstance(instanceType, constructorParam);
                            isCreated = true;
                        }
                        catch
                        {
                            isCreated = true;
                            currentConstructor++;
                        }
                    }
                    stack.Pop();
                    if (isCreated)
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("Could not create instance type");
                    }
                }
                else
                {
                    throw new Exception("Could not resolve type");
                }
            }
            else
            {
                throw new Exception("No such type registered");
            }
        }
        private object[] GetConstructorParam(ConstructorInfo constructorInfo, Type currType)
        {
            ParameterInfo[] parametersInfo = constructorInfo.GetParameters();
            object[] parameters = new object[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                parameters[i] = GetInstance(configuration.GetImplementation(parametersInfo[i].ParameterType), currType);
            }
            return parameters;
        }

    }

}


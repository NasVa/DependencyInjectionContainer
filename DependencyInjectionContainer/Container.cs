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

        public List<Dependency> stack;
        public List<object> objects;
        bool isCyclic;
        public Container(Configuration configuration)
        {
            this.configuration = configuration;
            this.stack = new List<Dependency>();
            objects = new List<object>(); 
            isCyclic = false;
        }

        public T Resolve<T>() where T : class
        {
            var type = typeof(T);
            Dependency dependency = configuration.GetImplementation(type);

            if(dependency == null)
            {
                throw new Exception("No such type dependency");
            }

            var result = (T)GetInstance(dependency);
            if (isCyclic)
            {
                resolveCyclic();   
            }
            return result;
        }

        public void resolveCyclic()
        {
            foreach(object obj in objects)
            {
                Type t = obj.GetType();
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                Type ttttttt = obj.GetType();
                foreach(PropertyInfo property in properties)
                {
                    var impl = configuration.GetImplementation(property.PropertyType);
                    property.SetValue(obj, GetInstance(impl));
                }
            }
        }

        public object GetInstance(Dependency dependency)
        {
            if (dependency.isSingleton)
            {
                if(dependency.instance != null)
                {
                    return dependency.instance;
                }

                if (!stack.Contains(dependency))
                {
                    stack.Add(dependency);
                    dependency.instance = Instantiate(dependency.interfaceType);
                }
                else
                {
                    isCyclic = true;
                    return null;
                }
                if (dependency.instance != null)
                {
                    objects.Add(dependency.instance);
                }
                return dependency.instance;
            }
            else
            {
                object instance = Instantiate(dependency.interfaceType);
                return instance;
            }
        }

        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            return (IEnumerable<T>)InstantiateEnumerable(typeof(T));
        }

        private object InstantiateEnumerable(Type type)
        {
            Dependency dependency = configuration.GetImplementation(type);
            if (dependency != null)
            {
                IList collection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                IEnumerable<Dependency> registeredTypes = configuration.GetAllImplementations(type);
                foreach (Dependency item in registeredTypes)
                {
                    collection.Add(GetInstance(item));
                }
                return collection;
            }
            else
            {
                throw new Exception("No such type registered");
            }
        }


        public object Instantiate(Type type)
        {
            Dependency dependency = configuration.GetImplementation(type);
            if (dependency != null)
            {
                Type instanceType = dependency.implementationType;
                if (instanceType.IsGenericTypeDefinition)
                {
                    instanceType = instanceType.MakeGenericType(type.GenericTypeArguments);
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
                        object[] constructorParam = GetConstructorParam(constructorInfo);
                        result = Activator.CreateInstance(instanceType, constructorParam);
                        isCreated = true;
                    }
                    catch
                    {
                        isCreated = true;
                        currentConstructor++;
                    }
                }
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
                throw new Exception("No such type registered");
            }
        
        }
        private object[] GetConstructorParam(ConstructorInfo constructorInfo)
        {
            ParameterInfo[] parametersInfo = constructorInfo.GetParameters();
            object[] parameters = new object[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                if (!isCyclic){
                    parameters[i] = GetInstance(configuration.GetImplementation(parametersInfo[i].ParameterType));
                }
                else
                {
                    foreach(Dependency dep in stack) {
                        parameters[i] = configuration.GetImplementation(dep.interfaceType).instance;
                    }
                }
            
            }
            return parameters;
        }

    }

}


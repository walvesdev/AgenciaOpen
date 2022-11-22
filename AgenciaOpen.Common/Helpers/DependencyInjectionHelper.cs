using AgenciaOpen.Common.Attributes;
using AgenciaOpen.Common.DependencyInjection;
using AgenciaOpen.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgenciaOpen.Common.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void AddByConvention(IServiceCollection services, Assembly assemblie)
        {
            var domain = AppDomain.CurrentDomain.GetOrLoad("AgenciaOpen.Domain");
            var service = AppDomain.CurrentDomain.GetOrLoad("AgenciaOpen.Services");
            var common = AppDomain.CurrentDomain.GetOrLoad("AgenciaOpen.Common");
            var database = AppDomain.CurrentDomain.GetOrLoad("AgenciaOpen.Database");

            var assemblies = new List<Assembly>();

            assemblies = new List<Assembly>();
            assemblies.Add(assemblie);
            assemblies.Add(domain);
            assemblies.Add(service);
            assemblies.Add(common);
            assemblies.Add(database);

            assemblies.ForEach(assembly =>
            {
                var types = AssemblyHelper.GetAllTypes(assembly).Where(type => type != null && type.IsClass && !type.IsAbstract && !type.IsGenericType).ToList();



                types.ForEach(type =>
                {
                    var attribute = type.GetCustomAttribute<DependencyInjectionAttribute>(true);

                    if (attribute.IsNotNull())
                    {
                        AddIfHasAttribute(services, type, attribute);
                    }
                    else
                    {
                        AddIfHasNoAttribute(services, type);
                    }
                });
            });
        }
        public static void AddIfHasAttribute(IServiceCollection services, Type typeImplement, DependencyInjectionAttribute attribute)
        {
            var exposeService = typeImplement.GetCustomAttribute<ExposeServicesAttribute>(true);

            if (attribute.IsNotNull() && exposeService.IsNotNull())
            {
                if (attribute.ImplementType.IsNotNull() && attribute.ServiceType.IsNull())
                {
                    if (typeImplement.IsAssignableTo(attribute.ImplementType))
                    {
                        AddType(services, typeImplement, attribute.Lifetime, attribute.ImplementType);
                    }
                }
                else
                {
                    var exposeServices = exposeService.GetExposedServiceTypes(typeImplement);

                    if (!exposeServices.IsNullOrEmpty())
                    {
                        exposeServices.ForEach(exServivice =>
                        {
                            if (typeImplement.IsAssignableTo(exServivice))
                            {
                                AddType(services, typeImplement, attribute.Lifetime, exServivice);
                            }
                        });
                    }
                }
            }
            else if (attribute.IsNotNull() && exposeService.IsNull())
            {
                var typeIterface = GetTypeIterface(typeImplement);

                if (typeIterface.IsNotNull())
                {
                    AddType(services, typeImplement, attribute.Lifetime, typeIterface);
                }
                else if (attribute.ServiceType.IsNotNull())
                {
                    AddType(services, attribute.ImplementType, attribute.Lifetime, attribute.ServiceType);
                }
                else
                {
                    AddType(services, typeImplement, attribute.Lifetime);
                }
            }
        }

        public static void AddIfHasNoAttribute(IServiceCollection services, Type type)
        {
            if (type.IsAssignableTo(typeof(ITransientDependency)))
            {
                AddDependency(services, type, ServiceLifetime.Transient);
            }

            if (type.IsAssignableTo(typeof(IScopedDependency)))
            {
                AddDependency(services, type, ServiceLifetime.Scoped);
            }

            if (type.IsAssignableTo(typeof(ISingletonDependency)))
            {
                AddDependency(services, type, ServiceLifetime.Singleton);
            }
        }

        public static void AddDependency(IServiceCollection services, Type typeImplement, ServiceLifetime serviceLifetime)
        {
            AddType(services, typeImplement, serviceLifetime, GetTypeIterface(typeImplement));
        }

        public static Type GetTypeIterface(Type type)
        {
            return type.GetInterfaces().FirstOrDefault(i => i.Name.Equals($"I{type.Name}"));
        }

        public static void AddType(IServiceCollection services, Type typeImplement, ServiceLifetime serviceLifetime, Type typeIterface = null)
        {
            if (typeIterface.IsNotNull() && typeIterface.IsAssignableFrom(typeImplement))
            {
                switch (serviceLifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(typeIterface, typeImplement);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(typeIterface, typeImplement);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(typeIterface, typeImplement);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (serviceLifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(typeImplement);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(typeImplement);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(typeImplement);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

using AgenciaOpen.Common.Helpers;
using System.Reflection;

namespace AgenciaOpen.Common.Extensions
{
    public static class AppDomainExtensions
    {
        public static Assembly GetOrLoad(this AppDomain appDomain, string assemblyName)
        {
            Check.NotNullOrWhiteSpace(assemblyName, "O nome do assembly deve ser fornecido");

            Assembly assembly = null;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.Contains("PayService")).ToList();

            if (!assemblies.IsNullOrEmpty())
            {
                assembly = assemblies.FirstOrDefault(a => a.GetName().Name.Equals(assemblyName));

                if (assembly.IsNotNull())
                {
                    return assembly;
                }
            }

            return AppDomain.CurrentDomain.Load(assemblyName);
        }
    }
}

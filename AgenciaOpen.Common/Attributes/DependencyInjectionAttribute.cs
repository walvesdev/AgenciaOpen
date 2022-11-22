using Microsoft.Extensions.DependencyInjection;

namespace AgenciaOpen.Common.Attributes
{
    public class DependencyInjectionAttribute : Attribute
    {
        public virtual ServiceLifetime Lifetime { get; set; }

        public virtual bool TryRegister { get; set; }

        public virtual bool ReplaceServices { get; set; }
        public virtual Type ServiceType { get; set; }
        public virtual Type ImplementType { get; set; }

        public DependencyInjectionAttribute()
        {
            Lifetime = ServiceLifetime.Transient;
        }

        public DependencyInjectionAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}

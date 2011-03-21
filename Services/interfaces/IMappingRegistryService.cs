using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Prism.Extensions.FluentNH.Services.interfaces
{
    public interface IMappingRegistryService
    {
        List<Assembly> Assemblies { get; }
        List<Type> Types { get; }
        void AddMap(Type type);
        void AddMapsFromAssemblyOf<T>();
        void AddMapsFromAssembly(Assembly assembly);
    }
}
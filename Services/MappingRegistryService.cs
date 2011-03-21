using System;
using System.Collections.Generic;
using System.Reflection;
using Prism.Extensions.FluentNH.Services.interfaces;

namespace Prism.Extensions.FluentNH.Services
{
    public class MappingRegistryService: IMappingRegistryService
    {
        readonly List<Assembly> _assemblies;
        readonly List<Type> _types;

        public MappingRegistryService()
        {
            _assemblies = new List<Assembly>();
            _types = new List<Type>();
        }

        public List<Assembly> Assemblies
        {
            get { return _assemblies; }
        }

        public List<Type> Types
        {
            get { return _types; }
        }

        public void AddMap(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!_types.Contains(type))
            {
                _types.Add(type);
            }
        }

        public void AddMapsFromAssemblyOf<T>()
        {
            AddMapsFromAssembly(typeof (T).Assembly);
        }

        public void AddMapsFromAssembly(Assembly assembly)
        {
            if (!_assemblies.Contains(assembly))
            {
                _assemblies.Add(assembly);
            }
        }
    }
}

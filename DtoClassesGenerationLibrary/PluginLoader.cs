using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;

namespace DtoClassesGenerationLibrary
{
    internal class PluginLoader<T>
    {

        private string path;
        private AppDomain domain;

        internal PluginLoader(string path)
        {
            this.path = path;
            domain = CreateDomain();
        }

        internal IEnumerable<T> Load()
        {
            var fileNames = Directory.EnumerateFiles(path, ".dll");
            if (fileNames != null)
            {
                return LoadFromFiles(fileNames);
            }
            return new List<T>();
        }

        private IEnumerable<T> LoadFromFiles(IEnumerable<string> fileNames)
        {
            var list = new List<T>();
            foreach (var dllName in fileNames)
            {
                list.AddRange(LoadFromDll(dllName));
            }
            return list;
        }

        private IEnumerable<T> LoadFromDll(string dllName)
        {
            foreach (string typeName in GetTypes(dllName, typeof(T)))
            {
                ObjectHandle handle;
                try
                {
                    handle = domain.CreateInstanceFrom(dllName, typeName);
                }
                catch (MissingMethodException)
                {
                    continue;
                }
                yield return (T)handle.Unwrap();
            }
        }

        private IEnumerable<string> GetTypes
            (string dllFileName, Type interfaceFilter)
        {
            foreach (Type type in domain.
                Load(AssemblyName.GetAssemblyName(dllFileName)).GetTypes())
            {
                if (type.GetInterface(interfaceFilter.Name) != null)
                {
                    yield return type.FullName;
                }
            }
        }

        private AppDomain CreateDomain()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = path;
            return AppDomain.CreateDomain("Domain", null, setup);
        }

        internal void UnloadDomain()
        {
            AppDomain.Unload(domain);
        }


    }
}

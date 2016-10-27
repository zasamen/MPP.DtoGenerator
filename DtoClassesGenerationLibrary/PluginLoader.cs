using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;

namespace DtoClassesGenerationLibrary
{
    internal class PluginLoader
    { 
        private string path;

        [ImportMany(typeof(ITypeDictionary))]
        private List<ITypeDictionary> Plugins { get; set; }

        internal PluginLoader(string path)
        {
            this.path = path;
        }

        public IEnumerable<ITypeDictionary> FindPlugins()
        {
            CompositionContainer container = new CompositionContainer(
                new DirectoryCatalog(path));
            container.ComposeParts(this);
            if (Plugins == null)
            {
                return new List<ITypeDictionary>();
            }
            else
            {
                return Plugins;
            }
        }

    }
}

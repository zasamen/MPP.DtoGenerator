using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;

namespace DtoClassesGenerationLibrary
{
    public class DtoClassGenerated
    {

        private string NamespaceName;

        public DtoClassGenerated(string NamespaceName)
        {
            this.NamespaceName = NamespaceName;
        }

        public DtoClassGenerated(string NamespaceName, string PluginPath)
            : this(NamespaceName)
        {
            ApplyPluginsLoading(new PluginLoader<ITypeDictionary>(PluginPath));
            
        }

        private void ApplyPluginsLoading(PluginLoader<ITypeDictionary> pluginLoader)
        {
            TypeTable table = TypeTable.Instance;
            foreach (var plugin in pluginLoader.Load())
            {
                table.Apply(plugin);
            }
        }


        public IEnumerable<DtoUnitDescription> GenerateUnits
            (IEnumerable<DtoClassDescription> Classes)
        {
            var units = new List<DtoUnitDescription>();
            foreach (DtoClassDescription Class in Classes)
            {
                units.Add(new DtoUnitDescription(Class.className, 
                    Class.GenerateCodeUnit(NamespaceName)));
            }
            return units;
        }
        
        

    }
}

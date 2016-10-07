using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

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
            LoadPlugins(PluginPath);
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
        
        private void LoadPlugins(string PluginPath)
        {

            //TODO DINAMIC PLUGIN LOADING
        }

    }
}

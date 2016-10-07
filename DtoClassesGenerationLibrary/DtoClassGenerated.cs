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
            
        }

        public DtoClassGenerated(string NamespaceName, string PluginPath)
        {
            LoadPlugins(PluginPath);
        }


        
        private void LoadPlugins(string PluginPath)
        {

            //TODO DINAMIC PLUGIN LOADING
        }

    }
}

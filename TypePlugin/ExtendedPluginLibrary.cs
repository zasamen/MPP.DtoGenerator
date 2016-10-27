using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoClassesGenerationLibrary;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;

namespace TypePlugin
{
    [Export(typeof(ITypeDictionary))]
    public class ExtendedPluginLibrary : ITypeDictionary
    {
        public IList<string> ImportList { get; private set; }

        public Dictionary<string, Type> TypeDictionary { get; private set; }

        public ExtendedPluginLibrary()
        {
            TypeDictionary = new Dictionary<string, Type>();
            TypeDictionary.Add("int64", typeof(Int64));
            TypeDictionary.Add("double", typeof(Double));
            TypeDictionary.Add("short", typeof(short));
            TypeDictionary.Add("Object", typeof(Object));
            TypeDictionary.Add("boolean", typeof(Boolean));
            TypeDictionary.Add("List<object>", typeof(List<object>));
            ImportList = new List<string>();
            ImportList.Add("System");
            ImportList.Add("System.Collections.Generic");
        }
    }
}

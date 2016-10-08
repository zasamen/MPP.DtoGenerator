using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoClassesGenerationLibrary;
using System.Collections.Concurrent;
namespace TypePlugin
{
    public class ExtendedPluginLibrary : ITypeDictionary
    {
        public IList<string> ImportList { get; private set; }

        public Dictionary<string, Type> TypeDictionary { get; private set; }

        public ExtendedPluginLibrary()
        {
            TypeDictionary.Add("int64", typeof(Int64));
            TypeDictionary.Add("double", typeof(Double));
            TypeDictionary.Add("short", typeof(short));
            TypeDictionary.Add("Object", typeof(Object));
            TypeDictionary.Add("List<object>", typeof(List<object>));
            ImportList.Add("System");
            ImportList.Add("System.Collections.Generic");
        }
    }
}

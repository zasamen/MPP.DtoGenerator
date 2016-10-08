using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoClassesGenerationLibrary
{
    internal class TypeTable
    {
        Dictionary<string, Type> TypeDictionary = new Dictionary<string, Type>();
        public List<string> Imports { get; private set;} = new List<string>();
        private static readonly TypeTable instance = new TypeTable();
        internal static TypeTable Instance
        {
            get
            {
                return instance;
            }
        }
        
        private TypeTable()
        {
            TypeDictionary.Add("int32", typeof(Int32));
            TypeDictionary.Add("float", typeof(Single));
            TypeDictionary.Add("byte", typeof(Byte));
            TypeDictionary.Add("string", typeof(string));
            Imports.Add("System");
        }

        internal void AddTypes(IDictionary<string,Type> dictionary)
        {
            TypeDictionary = TypeDictionary.
                Concat(dictionary).GroupBy(d => d.Key).
                ToDictionary(d => d.Key, d => d.First().Value);
        }


        internal Type GetType(string format)
        {
            return TypeDictionary[format];
        }

        internal void AddImports(IEnumerable<string> ImportList)
        {
            Imports.AddRange(ImportList);
        }

        internal void Apply(ITypeDictionary plugin)
        {
            AddImports(plugin.ImportList);
            AddTypes(plugin.TypeDictionary);
            
        }

    }
}

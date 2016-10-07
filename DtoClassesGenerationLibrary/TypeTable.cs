using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoClassesGenerationLibrary
{
    internal class TypeTable 
    {
        private Dictionary<string, Type> TypeDictionary;
        private static TypeTable instance = new TypeTable();
        public static TypeTable Instance
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
        }

        internal void AddTypes(IDictionary<string,Type> dictionary)
        {
            TypeDictionary = TypeDictionary.
                Concat(dictionary).GroupBy(d => d.Key).
                ToDictionary(d => d.Key, d => d.First().Value);
        }


        internal Type getType(string format)
        {
            return TypeDictionary[format];
        }



    }
}

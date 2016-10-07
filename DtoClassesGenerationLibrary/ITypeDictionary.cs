using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoClassesGenerationLibrary
{
    interface ITypeDictionary
    { 
        Dictionary<string, Type> TypeDictionary { get; }

        IList<string> ImportsList { get; }
    }
}

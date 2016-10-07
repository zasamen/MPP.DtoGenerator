using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoClassesGenerationLibrary;

namespace DtoClassesGeneratorFromJSON
{
    class Program
    {
        static string ClassPath;
        static string NameSpace = "MyNameSpace";

        static void Main(string[] args)
        {
            DtoClassGenerator generator = new DtoClassGenerator(NameSpace);
            generator.generateCodeUnit(convertJsonToCustomDescription(
                JsonReader<JsonClassDescription>.ReadJsonToTypeT("./json.json")));
            //TODO: GENERATE CODE FROM CLASSDESCRIPTIONS
        }

        private static IEnumerable<DtoClassDescription> convertJsonToCustomDescription(IEnumerable<JsonClassDescription> descriptions)
        {
            var DtoDescriptions = new LinkedList<DtoClassDescription>();
            foreach (var description in descriptions)
            {
                DtoDescriptions.AddLast(description.ConvertClassToDto());
            }
            return DtoDescriptions;
        }
    }
}

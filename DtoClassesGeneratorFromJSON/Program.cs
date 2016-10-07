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
        static string ClassPath= "./";
        static string NameSpace = "MyNameSpace";
        static string JsonPath = "./json.json";
        static void Main(string[] args)
        {
            new CodeWriter(ClassPath).WriteCodeToDir(
                new DtoClassGenerated(NameSpace).
                GenerateUnits(ConvertJsonToCustomDescription(
                        JsonReader<JsonClassDescription>.
                        ReadJsonToTypeT(JsonPath))));
        }

        private static IEnumerable<DtoClassDescription> 
            ConvertJsonToCustomDescription(
            IEnumerable<JsonClassDescription> descriptions)
        {
            var DtoDescriptions = new List<DtoClassDescription>();
            foreach (var description in descriptions)
            {
                DtoDescriptions.Add(description.ConvertClassToDto());
            }
            return DtoDescriptions;
        }

        
    }
}

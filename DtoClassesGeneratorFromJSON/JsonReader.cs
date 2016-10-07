using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace DtoClassesGeneratorFromJSON
{
    public class JsonReader<T>
    {

        public static T[] ReadJsonToTypeT(string FilePath)
        {
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
            {
                DataContractJsonSerializer JsonFormatter =
                new DataContractJsonSerializer(typeof(T[]));
                T[] t = (T[])JsonFormatter.ReadObject(fileStream);
                return t;
            }
        }
    }
}

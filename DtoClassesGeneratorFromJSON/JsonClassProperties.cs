using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DtoClassesGenerationLibrary;

namespace DtoClassesGeneratorFromJSON
{
    [DataContract]
    public class JsonClassProperties
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string format { get; set; }

        public DtoPropertyDescription ConvertPropertyToDto()
        {
            return new DtoPropertyDescription(name, format, type);
        }

        public static DtoPropertyDescription[] ConvertPropertiesToDto(JsonClassProperties[] properties)
        {
            DtoPropertyDescription[] DtoProperties = new DtoPropertyDescription[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                DtoProperties[i] = properties[i].ConvertPropertyToDto();
            }
            return DtoProperties;
        }
    }
}

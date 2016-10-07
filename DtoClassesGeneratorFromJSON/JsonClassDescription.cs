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
    public class JsonClassDescription
    {
        [DataMember]
        public string className { get; set; }

        [DataMember]
        public JsonClassProperties[] properties { get; set; }

        public DtoClassDescription ConvertClassToDto()
        {
            return new DtoClassDescription(className, JsonClassProperties.ConvertPropertiesToDto(properties));
        }
    }
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoClassesGenerationLibrary
{
    public class DtoPropertyDescription
    {
        private string name;
        private string format;
        private string type;

        public DtoPropertyDescription(string name, string format)
        {
            this.name = name;
            this.format = format;
        }

        internal CodeMemberProperty CreateProperty()
        {
            CodeMemberProperty ThisProperty = new CodeMemberProperty();
            ThisProperty.Attributes = 
                MemberAttributes.Public | MemberAttributes.Final;
            ThisProperty.Name = name;
            ThisProperty.HasGet = ThisProperty.HasSet = true;
            ThisProperty.Type = 
                new CodeTypeReference(TypeTable.Instance.getType(format));
            return ThisProperty;
        }
    }
}

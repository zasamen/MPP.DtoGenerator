using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace DtoClassesGenerationLibrary
{
    public class DtoFieldDescription
    {
        public string name { get; private set; }
        private string format;

        public DtoFieldDescription(string name, string format)
        {
            this.name = name.Remove(0, 1).
                Insert(0, name[0].ToString().ToLower());
            this.format = format;
        }

        internal CodeMemberField CreateField()
        {
            CodeMemberField ThisField = new CodeMemberField();
            ThisField.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            ThisField.Name = name;
            ThisField.Type = new CodeTypeReference(
                TypeTable.Instance.GetType(format).FullName);
            return ThisField;
        }
    }
}

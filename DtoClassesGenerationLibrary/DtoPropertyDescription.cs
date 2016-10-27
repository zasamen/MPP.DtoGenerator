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
        //private string type;
        public DtoFieldDescription field { get; private set; }
        public DtoPropertyDescription(string name, string format)
        {
            this.name = name;
            this.format = format;
            field = new DtoFieldDescription(name, format);
        }

        internal CodeMemberProperty CreateProperty()
        {
            return GeneratePropertyStatements(GeneratePropertyType(GeneratePropertyAttributes()));
        }

        private CodeMemberProperty GeneratePropertyStatements(CodeMemberProperty property)
        {
            property.HasGet = property.HasSet = true;
            property.GetStatements.
                Add(new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), field.name)));
            property.SetStatements.
                Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), field.name),
                    new CodePropertySetValueReferenceExpression()));
            return property;
        }

        private CodeMemberProperty GeneratePropertyType(CodeMemberProperty property)
        {
            property.Type = new CodeTypeReference(TypeTable.Instance.GetType(format).FullName);
            return property;
        }

        private CodeMemberProperty GeneratePropertyAttributes()
        {
            CodeMemberProperty property= new CodeMemberProperty();
            property.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            property.Name = name;
            return property;
        }
    }
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DtoClassesGenerationLibrary
{
    public class DtoClassDescription
    {
        public string className { get; }
        private DtoPropertyDescription[] properties;

        public DtoClassDescription(string className, 
            DtoPropertyDescription[] properties)
        {
            this.className = className;
            this.properties = properties;
        }

        private CodeTypeDeclaration CreateClass()
        {
            CodeTypeDeclaration ThisClass = new CodeTypeDeclaration(className);
            ThisClass.TypeAttributes =
                TypeAttributes.Public | TypeAttributes.Sealed;
            ThisClass.IsClass = true;
            AddPropertiesToClass(ref ThisClass, properties);
            return ThisClass;
        }

        internal CodeCompileUnit GenerateCodeUnit(string namespaceName)
        {
            var currentNamespace = new DtoNamespaceDescription(namespaceName);
            var unit = new CodeCompileUnit();
            currentNamespace.AddDtoClass(CreateClass());
            currentNamespace.AddImports();
            unit.Namespaces.Add(currentNamespace.CreatedNamespace);
            return unit;
        }

        private static void AddPropertiesToClass(ref CodeTypeDeclaration ctd,
            IEnumerable<DtoPropertyDescription> propertyList)
        {
            foreach (DtoPropertyDescription property in propertyList)
            {
                ctd.Members.Add(property.field.CreateField());
                ctd.Members.Add(property.CreateProperty());
            }
        }
    }

}


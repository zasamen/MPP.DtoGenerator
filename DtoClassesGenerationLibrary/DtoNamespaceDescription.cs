using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace DtoClassesGenerationLibrary
{
    public class DtoNamespaceDescription
    {
        private string NameOfNamespace;
        internal CodeNamespace CreatedNamespace { get; }


        internal DtoNamespaceDescription(string NameOfNamespace)
        {
            this.NameOfNamespace = NameOfNamespace;
            CreatedNamespace = new CodeNamespace(NameOfNamespace);
            CreatedNamespace.Imports.Add(new CodeNamespaceImport("System"));                        
        }

        internal void AddDtoClass(CodeTypeDeclaration Class)
        {
            CreatedNamespace.Types.Add(Class);
        }

        internal void AddImports(IEnumerable<string> imports)
        {
            if (imports != null)
            {
                foreach (var import in imports)
                {
                    CreatedNamespace.Imports.Add(new CodeNamespaceImport(import));
                }
            }
            else
            {
                throw new ArgumentNullException("Imports list","Can't be null");
            }
        }

    }
}

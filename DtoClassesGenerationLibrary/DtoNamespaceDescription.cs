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
        }

        internal void AddDtoClass(CodeTypeDeclaration Class)
        {
            CreatedNamespace.Types.Add(Class);
        }

        internal void AddImports()
        {
            var imports = TypeTable.Instance.GetImports();
            if (imports != null)
            {
                foreach (var import in imports)
                {
                    CreatedNamespace.Imports.Add(new CodeNamespaceImport(import));
                }
            }
        }

    }
}

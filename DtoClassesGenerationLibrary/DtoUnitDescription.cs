using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace DtoClassesGenerationLibrary
{
    public class DtoUnitDescription
    {
        public string filename { get; }
        public CodeCompileUnit unit { get; }
        private static readonly string fileExtenstion = ".cs";

        public DtoUnitDescription(string ClassName, CodeCompileUnit unit)
        {
            filename = ClassName + fileExtenstion;
            this.unit = unit;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using DtoClassesGenerationLibrary;
using System.IO;

namespace DtoClassesGeneratorFromJSON
{
    class CodeWriter
    {

        private static CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        private string CodePath;
        private static CodeGeneratorOptions options;

        internal CodeWriter(string CodePath)
        {
            this.CodePath = CodePath;
            if (!Directory.Exists(CodePath))
            {
                Directory.CreateDirectory(CodePath);
            }
        }
        public void WriteCodeToDirectory(IEnumerable<DtoUnitDescription> units)
        {
            List<NotImplementedException> ExceptionList = new List<NotImplementedException>();
            foreach (var unit in units)
            {
                try
                {
                    WriteCodeToFile(unit.unit, CodePath + unit.filename);
                }
                catch(NotImplementedException e)
                {
                    ExceptionList.Add(e);
                }
            }
            if (ExceptionList.Count > 0)
            {
                throw new NotImplementedException(ExceptionList.ToString());
            }
        }

        private void WriteCodeToFile(CodeCompileUnit unit, string path)
        {
            using (var streamwriter = new StreamWriter(path))
            {
                provider.GenerateCodeFromCompileUnit(unit, streamwriter, options);
            }
        }

        private static CodeGeneratorOptions generateOptions()
        {
            options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            return options;
        }
    }
}

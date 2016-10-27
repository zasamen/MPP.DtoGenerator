using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DtoClassesGenerationLibrary
{
    internal class ThreadData
    {
        internal ConcurrentBag<DtoUnitDescription> compiledUnits { get; private set; }
        internal ManualResetEvent eventWaiter { get; private set; }
        internal DtoClassDescription ClassDescription { get; private set; }
        internal string NamespaceName { get; private set; }

        internal ThreadData(ConcurrentBag<DtoUnitDescription> resultUnits, 
            ManualResetEvent eventNumber, DtoClassDescription currentClass,
            string NamespaceName)
        {
            compiledUnits = resultUnits;
            eventWaiter = eventNumber;
            ClassDescription = currentClass;
            this.NamespaceName = NamespaceName;
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Threading;

namespace DtoClassesGenerationLibrary
{
    public class DtoClassGenerated
    {

        private string NamespaceName;
        private int ThreadCount;
        private SemaphoreSlim Semaphore; 

        public DtoClassGenerated(string NamespaceName,int threadCount)
        {
            this.NamespaceName = NamespaceName;
            ThreadCount = threadCount;
        }

        public DtoClassGenerated(string NamespaceName,int threadCount, string PluginPath)
            : this(NamespaceName,threadCount)
        {
            var loader = new PluginLoader(PluginPath);
            ApplyPluginsLoading(loader);
        }

        private void ApplyPluginsLoading(PluginLoader pluginLoader)
        {
            TypeTable table = TypeTable.Instance;
            var plugins = pluginLoader.FindPlugins();
            if (plugins.Count() > 0)
            {
                foreach (var plugin in plugins)
                {
                    table.Apply(plugin);
                }
            }
        }


        public IEnumerable<DtoUnitDescription> GenerateUnits
            (IEnumerable<DtoClassDescription> Classes)
        {
            ManualResetEvent[] manualResetEvents = new ManualResetEvent[Classes.Count()];
            var units = new ConcurrentBag<DtoUnitDescription>();
            Semaphore = new SemaphoreSlim(ThreadCount);
            int i = 0;
            foreach (DtoClassDescription Class in Classes)
            {
                Semaphore.Wait();
                ThreadPool.QueueUserWorkItem(GenerateUnitCallBack, 
                    new ThreadData(units, 
                    manualResetEvents[i++] = new ManualResetEvent(false),
                    Class, NamespaceName));
            }
            WaitHandlersAndDisposeSemaphore(manualResetEvents);
            return units;
        }

        private void WaitHandlersAndDisposeSemaphore(ManualResetEvent[] manualResetEvents)
        {
            WaitAndDisposeHandlers(manualResetEvents);
            Semaphore.Dispose();
        }

        private void WaitAndDisposeHandlers(ManualResetEvent[] manualResetEvents)
        {
            if ((manualResetEvents != null) && (manualResetEvents.Length>0))
            {
                WaitHandle.WaitAll(manualResetEvents);
                foreach (var manualResetEvent in manualResetEvents)
                {
                    manualResetEvent.Dispose();
                }
            }
        }

        private void GenerateUnitCallBack(object context)
        {
            //Console.WriteLine("+");
            ThreadData data = (ThreadData)context;
            var Class = data.ClassDescription;
            data.compiledUnits.Add(new DtoUnitDescription(Class.className, 
                Class.GenerateCodeUnit(data.NamespaceName)));
            //Console.WriteLine("-");
            Semaphore.Release();
            data.eventWaiter.Set();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoClassesGenerationLibrary;
using System.Configuration;
using System.IO;

namespace DtoClassesGeneratorFromJSON
{
    class Program
    {
        internal class ConfigurationValues
        {
            string classPath;
            public string ClassPath
            {
                get
                {
                    return classPath;
                }
                private set
                {
                    if (Directory.Exists(value))
                    {
                        classPath = value;
                    }
                    else
                    {
                        try
                        {
                            classPath =
                                ConfigurationManager.
                                AppSettings["default_classes_directory"];
                            if (!Directory.Exists(classPath))
                            {
                                Directory.CreateDirectory(classPath);
                            }
                        }
                        catch (ConfigurationException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Default Generated Class Directory switched to program's");
                            classPath = "./";
                        }
                    }
                }
            }
            string nameSpace = string.Empty;
            public string NameSpace
            {
                get
                {
                    if (nameSpace.Length == 0)
                    {
                        try
                        {
                            nameSpace =
                                ConfigurationManager.
                                AppSettings["code_namespace"];
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Namespace Switched to 'Default'");
                            nameSpace = "Default";
                        }
                    }
                    return nameSpace;
                }
            }
            string jsonPath;
            public string JsonPath
            {
                get
                {
                    if (File.Exists(jsonPath))
                    {
                        return jsonPath;
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                set
                {
                    if (!File.Exists(value))
                    {
                        try
                        {
                            jsonPath =
                                ConfigurationManager.
                                AppSettings["json_source_file"];
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Json Source Switched to 'json.json'");
                            jsonPath = "./json.json";
                        }
                    }
                    else
                    {
                        jsonPath = value;
                    }
                }
            }
            string pluginPath=string.Empty;
            public string PluginPath
            {
                get
                {
                    if (pluginPath.Length == 0)
                    {
                        try
                        {
                            pluginPath =
                                ConfigurationManager.
                                AppSettings["plugins_directory"];
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Plugin Path Switched to './PLUGIN'");
                            pluginPath = "./PLUGIN";
                        }

                    }
                    if (Directory.Exists(pluginPath))
                    {
                        return pluginPath;
                    }
                    else
                    {
                        throw new DirectoryNotFoundException();
                    }
                }
            }
            int maxThreadCount = 0;
            public int MaxThreadCount
            {
                get
                {
                    if (maxThreadCount == 0)
                    {
                        try
                        {
                            maxThreadCount = Int32.Parse(ConfigurationManager.AppSettings["threads_max_count"]);
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("ThreadCount switched to 1");
                            maxThreadCount = 1; 
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("ThreadCount switched to 1");
                            maxThreadCount = 1;
                        }
                    }
                    return maxThreadCount;
                }
            }

            internal ConfigurationValues(string[] args)
            {
                if (args.Length == 0)
                {
                    JsonPath = string.Empty;
                    ClassPath = string.Empty;
                }
                else if (args.Length == 1)
                {
                    JsonPath = args[0];
                    ClassPath = string.Empty;
                }
                else
                {
                    JsonPath = args[0];
                    ClassPath = args[1];
                }
            }
        }
        static void Main(string[] args)
        {
            ConfigurationValues values = new ConfigurationValues(args);
            new CodeWriter(values.ClassPath).
                WriteCodeToDirectory(new DtoClassGenerated(
                    values.NameSpace,values.MaxThreadCount,values.PluginPath).
                GenerateUnits(ConvertJsonToCustomDescription(
                    JsonReader<JsonClassDescription>.ReadJsonToTypeT(
                        values.JsonPath))));
            Console.ReadLine();
        }

        private static IEnumerable<DtoClassDescription>
            ConvertJsonToCustomDescription(
            IEnumerable<JsonClassDescription> descriptions)
        {
            var DtoDescriptions = new List<DtoClassDescription>();
            foreach (var description in descriptions)
            {
                DtoDescriptions.Add(description.ConvertClassToDto());
            }
            return DtoDescriptions;
        }
        
    }
}

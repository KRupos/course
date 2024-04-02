using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace course
{
    public class PluginInfo
    {
        public string State { get; set; } //Добавить подгрузку состояния из конфигурации
        public string Version { get; private set; }
        public string LibPath { get; private set; }
        public string Developer { get; private set; }
        public string PluginDescription { get; private set; }
        public string FunctionDescription { get; private set; }

        public PluginInfo()
        {
        }

        public List<IBookFormatPlugin> LoadPlugins(string pluginsDirectory)
        {
            List<IBookFormatPlugin> plugins = new List<IBookFormatPlugin>();
            List<IBookFormatPlugin> plugins2 = new List<IBookFormatPlugin>();
            // Получение всех файлов .dll из папки pluginsDirectory
            string[] dllFiles = Directory.GetFiles(pluginsDirectory, "*.dll");

            foreach (var file in Directory.EnumerateFiles(pluginsDirectory, "*.dll", SearchOption.AllDirectories))
                try
                {
                    var ass = Assembly.LoadFile(file);
                    foreach (var type in ass.GetTypes())
                    {
                        var i = type.GetInterface("IBookFormatPlugin");
                        if (i != null)
                            plugins2.Add(ass.CreateInstance(type.FullName) as IBookFormatPlugin);
                    }
                }
                catch { }

            foreach (string dllFile in dllFiles)
            {
                try
                {
                    // Загрузка сборки
                    Assembly pluginAssembly = Assembly.LoadFrom(dllFile);
                    string pluginName = pluginAssembly.GetName().Name;
                    Console.WriteLine($"Имя сборки DLL: {pluginName}");

                    // Получение версии сборки
                    Version version = pluginAssembly.GetName().Version;
                    Console.WriteLine($"Версия: {version}");
                    object[] titleAttributes = pluginAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    if (titleAttributes.Length > 0)
                    {
                        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)titleAttributes[0];
                        Console.WriteLine($"Название: {titleAttribute.Title}");
                    }

                    object[] descriptionAttributes = pluginAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                    if (descriptionAttributes.Length > 0)
                    {
                        AssemblyDescriptionAttribute descriptionAttribute = (AssemblyDescriptionAttribute)descriptionAttributes[0];
                        Console.WriteLine($"Описание: {descriptionAttribute.Description}");
                    }
                    string iMyInterfaceName = typeof(IBookFormatPlugin).ToString().Substring(7);
                    Type[] defaultConstructorParametersTypes = Array.Empty<Type>();
                    object[] defaultConstructorParameters = Array.Empty<object>();

                    foreach (Type type in pluginAssembly.GetTypes())
                    {
                        string ns = type.Namespace;
                        if (type.GetInterface(iMyInterfaceName) != null)
                        {
                            Console.WriteLine(type.Name);
                            ConstructorInfo defaultConstructor = type.GetConstructor(defaultConstructorParametersTypes);
                            object instance = defaultConstructor.Invoke(defaultConstructorParameters);
                            //plugins.Add(instance as IBookFormatPlugin);
                            IBookFormatPlugin pluginInstance = instance as IBookFormatPlugin;
                            if (pluginInstance != null)
                            {
                                plugins2.Add(pluginInstance);
                            }
                        }
                    }
                    

                }
                catch (Exception ex)
                {
                    // Обработка ошибок загрузки плагина
                    Console.WriteLine($"Ошибка загрузки плагина из файла {dllFile}: {ex.Message}");
                }
            }

            return plugins2;
        }

        //static public List<PluginInfo> GetPluginsInfo()
        //{
        //    List<PluginConfiguration> pluginConfigurations = PluginConfiguration.GetPluginsConfigurations();
        //    Dictionary<string, bool> blockedDlls = new Dictionary<string, bool>();

        //    for (int i = 0; i < pluginConfigurations.Count; i++)
        //    {
        //        //Буду записывать в конфигурацию только выключенные плагины.
        //        blockedDlls.Add(pluginConfigurations[i].libPath, false);
        //    }

        //    List<PluginInfo> pluginInfos = new List<PluginInfo>();
        //    string[] files = Directory.GetFiles(Configuration.pluginsFolder, "*.dll");

        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        if (DefaultPluginHost<PluginBase>.VerificatePlugin(files[i]) == true)
        //        {
        //            IntPtr libPtr = NativeMethods.LoadLibrary(files[i]);

        //            PluginType pluginType = DefaultPluginHost<PluginBase>.IdentificatePluginType(files[i]);
        //            PluginInfo pluginInfo = new PluginInfo();

        //            pluginInfo.LibPath = files[i];
        //            pluginInfo.Version = GetString(libPtr, PluginLiteralNames.Version);
        //            pluginInfo.Developer = GetString(libPtr, PluginLiteralNames.Developer);
        //            pluginInfo.PluginDescription = GetString(libPtr, PluginLiteralNames.PluginDescription);

        //            IntPtr tmpFPtr = NativeMethods.GetProcAddress(libPtr, PluginLiteralNames.PluginDescription);
        //            pluginInfo.FunctionDescription = DefaultPluginHost<PluginBase>.GetFuncDescription(libPtr, DefaultPluginHost<PluginBase>.GetFuncNames(libPtr)[0]);

        //            if (blockedDlls.ContainsKey(files[i]) == true)
        //            {
        //                pluginInfo.State = "Выключено";
        //            }
        //            else
        //            {
        //                pluginInfo.State = "Включено";
        //            }

        //            pluginInfos.Add(pluginInfo);

        //            NativeMethods.FreeLibrary(libPtr);
        //        }
        //    }

        //    return pluginInfos;
        //}
        //static private string GetString(IntPtr libPtr, string pluginFuncName)
        //{
        //    IntPtr tmpFPtr = NativeMethods.GetProcAddress(libPtr, pluginFuncName);
        //    PluginFunctions someFunction = Marshal.GetDelegateForFunctionPointer<PluginFunctions>(tmpFPtr);

        //    IntPtr message = someFunction();
        //    return Marshal.PtrToStringAnsi(message);
        //}
    }
    public enum PluginState
    {
        ON,
        OFF
    }
}

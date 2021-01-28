using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace PluginLister
{
    class Program
    {
        static void Main()
        {
            string pluginFile = Directory.GetCurrentDirectory() + @"\Pluginlist.txt";
            string gameVersion = File.ReadAllText("BeatSaberVersion.txt");
            gameVersion = $"Game version: {gameVersion}" + '\n' + Environment.NewLine;
            if (File.Exists(pluginFile))
            {
                File.Delete(pluginFile);
                File.WriteAllText(pluginFile,"");
            }
            string pluginPath = Directory.GetCurrentDirectory() + @"\plugins";
            int pluginAmount = 0;
            try
            {
                var modFilesDLL = Directory.EnumerateFiles(pluginPath, "*.dll");
                var modFilesManifest = Directory.EnumerateFiles(pluginPath, "*.manifest");
                var plugins = new List<string>();
                foreach (string currentFile in modFilesDLL)
                {
                    string fileName = currentFile.Substring(pluginPath.Length + 1);
                    pluginAmount = +1;
                    FileVersionInfo.GetVersionInfo(Path.Combine(pluginPath, fileName));
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo($"{pluginPath}\\{fileName}");
                    string fullFileDescVersion = "Mod: " + fileName + '\n' + "Version: " + myFileVersionInfo.FileVersion + '\n';
                    plugins.Add(fullFileDescVersion);
                }
                File.AppendAllText(pluginFile, gameVersion);
                File.AppendAllLines(pluginFile, plugins);
                File.AppendAllText(pluginFile, $"Plugins found: {pluginAmount}"); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

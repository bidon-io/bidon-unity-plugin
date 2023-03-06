using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Bidon.Mediation.Editor.Utilities;
using Bidon.Mediation.Editor.DataContainers;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.AssetExtractors
{
    internal static class BidonDependenciesExtractor
    {
        public static bool ExtractDependencies()
        {
            return !PluginPreferences.Instance.AreDependenciesExtracted && CopyDependenciesFromPackageDirectory();
        }

        private static bool CopyDependenciesFromPackageDirectory()
        {
            var depsDir = new DirectoryInfo(EditorConstants.PackageDepsDirectory);
            if (!depsDir.Exists)
            {
                Debug.LogError($"[BidonPlugin] Directory not found: '{depsDir}'.");
                return false;
            }

            var deps = depsDir.GetFiles(EditorConstants.PackageDepsSearchPattern, SearchOption.AllDirectories);
            if (deps.Length < 1)
            {
                Debug.LogError($"[BidonPlugin] No Dependencies were found on path '{depsDir}'.");
                return false;
            }

            string outputDir = EditorConstants.PluginDepsDirectory;

            FileUtil.DeleteFileOrDirectory(outputDir);
            Directory.CreateDirectory(outputDir);

            deps.ToList().ForEach(dep => FileUtil.ReplaceFile(dep.FullName, $"{outputDir}/{dep.Name.Replace(".txt", ".xml")}"));

            PluginPreferences.Instance.AreDependenciesExtracted = true;
            PluginPreferences.SaveAsync();

            return true;
        }
    }
}

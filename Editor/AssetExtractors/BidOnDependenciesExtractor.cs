using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using AppodealStack.BidOnEngine.Editor.Utilities;
using AppodealStack.BidOnEngine.Editor.DataContainers;

// ReSharper Disable CheckNamespace
namespace AppodealStack.BidOnEngine.Editor.AssetExtractors
{
    internal static class BidOnDependenciesExtractor
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
                Debug.LogError($"[BidOn] Directory not found: '{depsDir}'.");
                return false;
            }

            var deps = depsDir.GetFiles(EditorConstants.PackageDepsSearchPattern, SearchOption.AllDirectories);
            if (deps.Length < 1)
            {
                Debug.LogError($"[BidOn] No Dependencies were found on path '{depsDir}'.");
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

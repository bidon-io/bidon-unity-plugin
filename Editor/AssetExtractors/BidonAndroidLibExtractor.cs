using System.IO;
using UnityEditor;
using UnityEngine;
using Bidon.Mediation.Editor.Utilities;
using Bidon.Mediation.Editor.DataContainers;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.AssetExtractors
{
    internal static class BidonAndroidLibExtractor
    {
        public static bool ExtractAndroidLibrary()
        {
            return !PluginPreferences.Instance.IsAndroidLibraryExtracted && CopyAndroidLibraryFromPackageDirectory();
        }

        private static bool CopyAndroidLibraryFromPackageDirectory()
        {
            string source = $"{EditorConstants.PackageRootDirectory}/{EditorConstants.PackageAndroidLibPath}";
            string destination = $"{EditorConstants.ProjectAndroidDirectory}/{EditorConstants.PluginAndroidLibName}";

            if (!Directory.Exists(source))
            {
                Debug.LogError($"[BidonPlugin] Directory not found: '{source}'.");
                return false;
            }

            Directory.CreateDirectory(EditorConstants.ProjectAndroidDirectory);

            if (Directory.Exists(destination))
            {
                FileUtil.DeleteFileOrDirectory(destination);
                FileUtil.DeleteFileOrDirectory($"{destination}.meta");
            }

            FileUtil.CopyFileOrDirectory(source, destination);

            PluginPreferences.Instance.IsAndroidLibraryExtracted = true;
            PluginPreferences.SaveAsync();

            return true;
        }
    }
}

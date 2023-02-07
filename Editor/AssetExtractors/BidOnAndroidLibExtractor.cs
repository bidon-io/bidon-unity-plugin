using System.IO;
using UnityEditor;
using UnityEngine;
using AppodealStack.BidOnEngine.Editor.Utilities;
using AppodealStack.BidOnEngine.Editor.DataContainers;

// ReSharper Disable CheckNamespace
namespace AppodealStack.BidOnEngine.Editor.AssetExtractors
{
    internal static class BidOnAndroidLibExtractor
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
                Debug.LogError($"[BidOn] Directory not found: '{source}'.");
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

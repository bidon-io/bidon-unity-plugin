// ReSharper disable CheckNamespace

using System.IO;
using UnityEditor;
using UnityEngine;
using Bidon.Mediation.Utilities.Editor;

namespace Bidon.Mediation.AssetExtractors.Editor
{
    internal static class BidonAndroidLibraryInstaller
    {
        public static bool Deploy()
        {
            if (Directory.Exists(EditorConstants.PluginAndroidLibraryDirectory)) return false;

            if (!Directory.Exists(EditorConstants.PackageAndroidLibraryDirectory))
            {
                Debug.LogError($"[BidonPlugin] Directory not found: '{EditorConstants.PackageAndroidLibraryDirectory}'");
                return false;
            }

            Directory.CreateDirectory(EditorConstants.ProjectAndroidDirectory);
            FileUtil.CopyFileOrDirectory(EditorConstants.PackageAndroidLibraryDirectory, EditorConstants.PluginAndroidLibraryDirectory);

            return true;
        }
    }
}

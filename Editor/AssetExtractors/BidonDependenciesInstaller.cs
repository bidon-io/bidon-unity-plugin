// ReSharper disable CheckNamespace

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Bidon.Mediation.Utilities.Editor;

namespace Bidon.Mediation.AssetExtractors.Editor
{
    internal static class BidonDependenciesInstaller
    {
        public static bool Deploy()
        {
            if (File.Exists(EditorConstants.PluginDependenciesFilePath)) return false;

            try
            {
                var depsFileInfo = new FileInfo(EditorConstants.PackageDependenciesFilePath);
                if (!depsFileInfo.Exists)
                {
                    Debug.LogError($"[BidonPlugin] File not found: '{depsFileInfo.FullName}'");
                    return false;
                }

                Directory.CreateDirectory(EditorConstants.PluginDependenciesDirectory);
                FileUtil.ReplaceFile(depsFileInfo.FullName, EditorConstants.PluginDependenciesFilePath);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[BidonPlugin] Failed to deploy dependencies. Exception: {e.Message}");
                return false;
            }
        }
    }
}

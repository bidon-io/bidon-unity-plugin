using System.IO;
using UnityEditor;
using AppodealStack.BidOnEngine.Editor.Utilities;
using AppodealStack.BidOnEngine.Editor.DataContainers;

// ReSharper Disable CheckNamespace
namespace AppodealStack.BidOnEngine.Editor.AssetExtractors
{
    internal class BidOnAssetPostprocessor : AssetPostprocessor
    {
#if UNITY_2021_3_OR_NEWER
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
#else
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            string prefsPath = $"{EditorConstants.PluginEditorDirectory}/{EditorConstants.PluginPrefsFileName}";
            if (File.Exists(prefsPath) && AssetDatabase.LoadAssetAtPath<PluginPreferences>(prefsPath) == null) return;
#endif
            if (BidOnAndroidLibExtractor.ExtractAndroidLibrary() | BidOnDependenciesExtractor.ExtractDependencies())
            {
                AssetDatabase.Refresh();
            }
#if UNITY_2020_3_16_OR_NEWER
            AssetDatabase.SaveAssetIfDirty(PluginPreferences.Instance);
#endif
        }
    }
}

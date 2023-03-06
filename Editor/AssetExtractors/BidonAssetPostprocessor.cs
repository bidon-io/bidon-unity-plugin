using System.IO;
using UnityEditor;
using Bidon.Mediation.Editor.Utilities;
using Bidon.Mediation.Editor.DataContainers;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.AssetExtractors
{
    internal class BidonAssetPostprocessor : AssetPostprocessor
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
            if (BidonAndroidLibExtractor.ExtractAndroidLibrary() | BidonDependenciesExtractor.ExtractDependencies())
            {
                AssetDatabase.Refresh();
            }
#if UNITY_2020_3_16_OR_NEWER
            AssetDatabase.SaveAssetIfDirty(PluginPreferences.Instance);
#endif
        }
    }
}

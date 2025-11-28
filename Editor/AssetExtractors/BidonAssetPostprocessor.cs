// ReSharper disable CheckNamespace

using System.Linq;
using UnityEditor;
using Bidon.Mediation.Utilities.Editor;

namespace Bidon.Mediation.AssetExtractors.Editor
{
    internal class BidonAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
        {
            if (deletedAssets.Any(asset => asset.Contains(EditorConstants.PackageRootDirectory))) return;

            if (BidonAndroidLibraryInstaller.Deploy() | BidonDependenciesInstaller.Deploy())
            {
                AssetDatabase.Refresh();
            }
        }
    }
}

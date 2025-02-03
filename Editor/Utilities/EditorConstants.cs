// ReSharper disable CheckNamespace

namespace Bidon.Mediation.Utilities.Editor
{
    public static class EditorConstants
    {
        public const string PackageName = "org.bidon.mediation";
        public const string PackageRootDirectory = "Packages/" + PackageName;

        public const string AndroidLibraryDirectoryName = "bidon.androidlib";
        public const string DependenciesFileName = "BidonDependencies";
        public const string RemoveListFileName = "remove_list";

        public const string PackageAndroidLibraryDirectory = PackageRootDirectory + "/Runtime/Plugins/Android/" + AndroidLibraryDirectoryName + "~";
        public const string PackageDependenciesFilePath = PackageRootDirectory + "/Editor/Dependencies/" + DependenciesFileName + ".txt";
        public const string PackageRemoveListFilePath = PackageRootDirectory + "/Editor/PluginRemover/" + RemoveListFileName + ".xml";

        public const string ProjectAndroidDirectory = "Assets/Plugins/Android";

        public const string PluginEditorDirectory = "Assets/Bidon/Editor";
        public const string PluginDependenciesDirectory = PluginEditorDirectory + "/Dependencies";

        public const string PluginAndroidLibraryDirectory = ProjectAndroidDirectory + "/" + AndroidLibraryDirectoryName;
        public const string PluginDependenciesFilePath = PluginDependenciesDirectory + "/" + DependenciesFileName + ".xml";

        public const string PluginDocumentationLink = "https://docs.bidon.org/docs/sdk/unity/get-started";
    }
}

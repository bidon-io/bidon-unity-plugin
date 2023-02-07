// ReSharper Disable CheckNamespace
namespace AppodealStack.BidOnEngine.Editor.Utilities
{
    public static class EditorConstants
    {
        public const string ProjectAndroidDirectory = "Assets/Plugins/Android";

        public const string PluginAndroidLibName = "bidon.androidlib";
        public const string PluginPrefsFileName = "PluginPreferences.asset";
        public const string PluginEditorDirectory = "Assets/BidOn/Editor";
        public const string PluginDepsDirectory = PluginEditorDirectory + "/Dependencies";

        public const string PackageName = "com.appodealstack.bidon";
        public const string PackageDepsSearchPattern = "*Dependencies.txt";
        public const string PackageRootDirectory = "Packages/" + PackageName;
        public const string PackageAndroidLibPath = "Runtime/Plugins/Android/" + PluginAndroidLibName + "~";
        public const string PackageDepsDirectory = PackageRootDirectory + "/Editor/Dependencies";
        public const string PackageRemoveListFilePath = PackageRootDirectory + "/Editor/PluginRemover/remove_list.xml";

        public const string RepositoryReadmeLink = "https://github.com/bidon-io/bidon-unity-plugin/blob/main/README.md";
    }
}

using UnityEditor;
using UnityEngine;
using AppodealStack.BidOnEngine.Editor.Utilities;
using AppodealStack.BidOnEngine.Editor.PluginRemover;

// ReSharper Disable CheckNamespace
namespace AppodealStack.BidOnEngine.Editor.TopBarMenu
{
    internal class BidOnTopBarMenu : ScriptableObject
    {
        [MenuItem("BidOn/Plugin Documentation")]
        public static void OpenDocumentation()
        {
            Application.OpenURL(EditorConstants.RepositoryReadmeLink);
        }

        [MenuItem("BidOn/Remove Plugin")]
        public static void RemovePlugin()
        {
            RemoveHelper.RemovePlugin();
        }
    }
}

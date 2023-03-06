using UnityEditor;
using UnityEngine;
using Bidon.Mediation.Editor.Utilities;
using Bidon.Mediation.Editor.PluginRemover;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.TopBarMenu
{
    internal class BidonTopBarMenu : ScriptableObject
    {
        [MenuItem("Bidon/Plugin Documentation")]
        public static void OpenDocumentation()
        {
            Application.OpenURL(EditorConstants.RepositoryReadmeLink);
        }

        [MenuItem("Bidon/Remove Plugin")]
        public static void RemovePlugin()
        {
            RemoveHelper.RemovePlugin();
        }
    }
}

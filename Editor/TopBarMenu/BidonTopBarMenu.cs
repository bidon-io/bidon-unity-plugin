// ReSharper disable CheckNamespace

using UnityEditor;
using UnityEngine;
using Bidon.Mediation.PluginRemover.Editor;
using Bidon.Mediation.Utilities.Editor;

namespace Bidon.Mediation.TopBarMenu.Editor
{
    internal class BidonTopBarMenu : ScriptableObject
    {
        [MenuItem("Bidon/Plugin Documentation")]
        public static void OpenDocumentation()
        {
            Application.OpenURL(EditorConstants.PluginDocumentationLink);
        }

        [MenuItem("Bidon/Remove Plugin")]
        public static void RemovePlugin()
        {
            RemoveHelper.RemovePlugin();
        }
    }
}

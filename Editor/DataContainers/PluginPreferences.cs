using System.IO;
using UnityEditor;
using UnityEngine;
using Bidon.Mediation.Editor.Utilities;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.DataContainers
{
    public class PluginPreferences : ScriptableObject
    {
        [HideInInspector] [SerializeField] private bool areDependenciesExtracted;
        [HideInInspector] [SerializeField] private bool isAndroidLibraryExtracted;

        private static PluginPreferences _instance;
        public static PluginPreferences Instance
        {
            get
            {
                if (_instance) return _instance;

                Directory.CreateDirectory(EditorConstants.PluginEditorDirectory);

                string prefsFilePath = $"{EditorConstants.PluginEditorDirectory}/{EditorConstants.PluginPrefsFileName}";

                _instance = AssetDatabase.LoadAssetAtPath<PluginPreferences>(prefsFilePath);
                if (_instance) return _instance;
                _instance = CreateInstance<PluginPreferences>();
                AssetDatabase.CreateAsset(_instance, prefsFilePath);

                return _instance;
            }
        }

        public bool AreDependenciesExtracted
        {
            get => areDependenciesExtracted;
            set => areDependenciesExtracted = value;
        }

        public bool IsAndroidLibraryExtracted
        {
            get => isAndroidLibraryExtracted;
            set => isAndroidLibraryExtracted = value;
        }

        public static void SaveAsync()
        {
            EditorUtility.SetDirty(_instance);
        }
    }
}

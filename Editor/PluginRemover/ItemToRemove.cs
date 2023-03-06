// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.PluginRemover
{
    [System.Serializable]
    internal class ItemToRemove
    {
        public string name;
        public string path;
        public string filter;
        public string description;
        public bool checkIfEmpty;
        public bool isConfirmationRequired;
        public bool performOnlyIfTotalRemove;
    }
}

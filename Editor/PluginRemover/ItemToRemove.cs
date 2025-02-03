// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;

namespace Bidon.Mediation.PluginRemover.Editor
{
    [Serializable]
    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
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

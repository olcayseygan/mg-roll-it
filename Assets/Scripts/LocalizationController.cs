using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;
using UnityEngine.Localization;

namespace Assets.Scripts
{

    public class LocalizationController : SingletonProvider<LocalizationController>
    {
        public string GetLocalizedString(string entryReference, params string[] arguments)
        {
            var localizedString = new LocalizedString { TableReference = "Table", TableEntryReference = entryReference, Arguments = arguments };
            return localizedString.GetLocalizedString();
        }
    }
}

using UnityEngine;

namespace Code.PlanetEditorV2.Settings.Containers
{
    public abstract class SettingsScriptableContainer<TSettings> : ScriptableObject
        where TSettings : EditorSettingsBase
    {
        public TSettings Settings;
    }
}
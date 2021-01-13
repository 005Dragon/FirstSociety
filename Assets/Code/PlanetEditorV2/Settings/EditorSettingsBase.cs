using System;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public abstract class EditorSettingsBase
    {
        [HideInInspector]
        public bool Foldout;

        public Action OnUpdate;

        public void Update()
        {
            UpdateCore();
            OnUpdate?.Invoke();
        }

        protected virtual void UpdateCore()
        {
        }
    }
}
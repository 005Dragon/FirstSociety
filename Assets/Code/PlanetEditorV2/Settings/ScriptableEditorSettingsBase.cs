using System;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    public abstract class ScriptableEditorSettingsBase : ScriptableObject
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
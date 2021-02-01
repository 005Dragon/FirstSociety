using System;
using Code.PlanetEditorV2.Settings;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators
{
    public abstract class GeneratorBase<TSettings>
        where TSettings : EditorSettingsBase
    {
        public Action<GameObject> DestroyImmediate;
        
        public TSettings Settings;
        
        public GameObject Generate()
        {
            if (Validate())
            {
                InitializeSettings();
                return GenerateCore();
            }
            
            Debug.LogWarning($"{GetType()} validation failed!");

            return null;
        }
        
        protected virtual void InitializeSettings()
        {
            Settings.OnUpdate = () => Generate();
        }

        protected abstract GameObject GenerateCore();

        protected virtual bool ValidateCore()
        {
            return true;
        }

        private bool Validate()
        {
            if (Settings == null)
            {
                return false;
            }

            if (DestroyImmediate == null)
            {
                return false;
            }

            return ValidateCore();
        }
    }
}
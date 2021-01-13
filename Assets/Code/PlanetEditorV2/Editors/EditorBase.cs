using Code.PlanetEditorV2.Settings;
using Code.PlanetEditorV2.Settings.Containers;
using UnityEngine;

namespace Code.PlanetEditorV2.Editors
{
    #if UNITY_EDITOR
    public class EditorBase<T> : UnityEditor.Editor
        where T : MonoBehaviour
    {
        protected T CustomObject;
        
        protected void DrawSettingsEditor<TSettings>(
            SettingsScriptableContainer<TSettings> settingsContainer,
            ref UnityEditor.Editor editor)
            where TSettings : EditorSettingsBase
        {
            if (settingsContainer == null)
            {
                return;
            }

            TSettings settings = settingsContainer.Settings;

            settingsContainer.Settings.Foldout = UnityEditor.EditorGUILayout.InspectorTitlebar(settings.Foldout, settingsContainer);
                
            if (!settings.Foldout)
            {
                return;
            }
            
            using (var check = new UnityEditor.EditorGUI.ChangeCheckScope())
            {
                CreateCachedEditor(settingsContainer, null, ref editor);
                editor.OnInspectorGUI(); 

                if (check.changed)
                {
                    settings.Update();
                }
            }
        }

        private void OnEnable()
        {
            CustomObject = (T) target;
        }
    }
    #endif
}
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.PlanetEditor
{
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PlanetGenerator))]
    public class PlanetEditor : UnityEditor.Editor
    {
        private PlanetGenerator _planetGenerator;
        private UnityEditor.Editor _shapeEditor;
        private UnityEditor.Editor _colourEditor;

        public override void OnInspectorGUI()
        {
            using (var check = new UnityEditor.EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (check.changed)
                {
                    _planetGenerator.Generate();
                }
            }

            if (GUILayout.Button("Apply settings"))
            {
                _planetGenerator.Generate();
            }
            
            DrawSettingsEditor(
                _planetGenerator.ShapeSettings,
                _planetGenerator.OnShapeSettingsUpdate,
                ref _shapeEditor,
                ref _planetGenerator.ShapeSettingsFoldout
            );

            DrawSettingsEditor(
                _planetGenerator.ColourSettings,
                _planetGenerator.OnColourSettingsUpdated,
                ref _colourEditor,
                ref _planetGenerator.ColourSettingsFoldout
            );
        }

        private void DrawSettingsEditor(Object settings, Action onSettingsUpdated, ref UnityEditor.Editor editor, ref bool foldout)
        {
            if (settings == null)
            {
                return;
            }
            
            foldout = UnityEditor.EditorGUILayout.InspectorTitlebar(foldout, settings);
                
            if (!foldout)
            {
                return;
            }
            
            using (var check = new UnityEditor.EditorGUI.ChangeCheckScope())
            {
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();

                if (check.changed)
                {
                    onSettingsUpdated?.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            _planetGenerator = (PlanetGenerator) target;
        }
    }
    #endif
}

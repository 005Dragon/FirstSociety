using Code.PlanetEditorV2.Generators;
using Code.PlanetEditorV2.Generators.Behaviours;
using UnityEngine;

namespace Code.PlanetEditorV2.Editors
{
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PlanetGeneratorBehaviour))]
    public class PlanetEditor : EditorBase<PlanetGeneratorBehaviour>
    {
        public override void OnInspectorGUI()
        {
            using (var check = new UnityEditor.EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (check.changed)
                {
                    CustomObject.Generate();
                }
            }

            if (GUILayout.Button("Initialize"))
            {
                CustomObject.Initialize();
            }

            if (GUILayout.Button("Apply settings"))
            {
                CustomObject.Generate();
            }

            if (GUILayout.Button("Save"))
            {
                CustomObject.SavePrefab();
            }
        }
    }
    #endif
}

using Code.PlanetEditorV2.Generators.Behaviours;
using Code.PlanetEditorV2.Settings;

namespace Code.PlanetEditorV2.Editors
{
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PlanetWaterGeneratorBehaviour))]
    public class PlanetWaterEditor : EditorBase<PlanetWaterGeneratorBehaviour>
    {
        private UnityEditor.Editor _sphereMeshEditor;

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
            
            DrawSettingsEditor(CustomObject.SettingsContainer, ref _sphereMeshEditor);
        }
    }
    #endif
}
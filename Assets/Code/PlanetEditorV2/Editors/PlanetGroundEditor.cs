using Code.PlanetEditorV2.Generators.Behaviours;

namespace Code.PlanetEditorV2.Editors
{
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PlanetGroundGeneratorBehaviour))]
    public class PlanetGroundEditor : EditorBase<PlanetGroundGeneratorBehaviour>
    {
        private UnityEditor.Editor _settingsMeshEditor;
        
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
            
            DrawSettingsEditor(CustomObject.SettingsContainer, ref _settingsMeshEditor);
        }
    }
    #endif
}
using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Settings;
using Code.PlanetEditorV2.Settings.Containers;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators.Behaviours
{
    public class PlanetGroundGeneratorBehaviour : MonoBehaviour
    {
        public SettingsScriptableContainer<PlanetGroundSettings> SettingsContainer;

        public PlanetGroundSettings Settings => SettingsContainer.Settings;

        public readonly PlanetGroundGenerator Generator = new PlanetGroundGenerator();

        public void Initialize(GameObject planet)
        {
            Generator.Initialize(planet, DestroyImmediate);
        }

        public void Generate()
        {
            Generator.Settings = SettingsContainer.Settings;
            Generator.Generate();
        }
    }
}
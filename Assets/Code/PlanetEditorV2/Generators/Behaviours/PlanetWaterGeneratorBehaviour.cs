using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Settings;
using Code.PlanetEditorV2.Settings.Containers;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators.Behaviours
{
    public class PlanetWaterGeneratorBehaviour : MonoBehaviour
    {
        public SettingsScriptableContainer<PlanetWaterSettings> SettingsContainer;

        public PlanetWaterSettings Settings => SettingsContainer.Settings;
        
        public readonly PlanetWaterGenerator Generator = new PlanetWaterGenerator();

        public void Initialize(GameObject planet, PlanetGroundGenerator planetGroundGenerator)
        {
            Generator.Initialize(planet, planetGroundGenerator, DestroyImmediate);
        }
        
        public void Generate()
        {
            Generator.Settings = SettingsContainer.Settings;
            Generator.Generate();
        }
    }
}
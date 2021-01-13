using Code.PlanetEditorV2.Factories;
using Code.PlanetEditorV2.Settings;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators.Behaviours
{
    #if UNITY_EDITOR
    public class PlanetGeneratorBehaviour : MonoBehaviour
    {
        public PlanetGeneratorSettings Settings;
        
        [HideInInspector]
        public GameObject Planet;
        
        private PlanetGroundGeneratorBehaviour _planetGroundGeneratorBehaviour;
        private PlanetWaterGeneratorBehaviour _planetWaterGeneratorBehaviour;

        private PlanetModelFactory _planetModelFactory;
        
        public void Initialize()
        {
            InitializePlanet();
            InitializePlanetGroundGenerator();
            InitializePlanetWaterGenerator();
            InitializePlanetModelFactory();
        }
        
        public void Generate()
        {
            Initialize();
            _planetGroundGeneratorBehaviour.Generate();
            _planetWaterGeneratorBehaviour.Generate();
            _planetModelFactory.CreateModel(Planet);
        }
        
        public void SavePrefab()
        {
            PrefabHelper.Save(Planet, Settings.PlanetName);
            
            DestroyImmediate(Planet);
            
            Generate();
        }

        private void InitializePlanet()
        {
            if (Planet == null)
            {
                Planet = new GameObject(Settings.PlanetName);
                Planet.transform.parent = transform;
            }
        }

        private void InitializePlanetGroundGenerator()
        {
            if (_planetGroundGeneratorBehaviour == null)
            {
                _planetGroundGeneratorBehaviour = gameObject.GetComponent<PlanetGroundGeneratorBehaviour>();

                if (_planetGroundGeneratorBehaviour == null)
                {
                    _planetGroundGeneratorBehaviour = gameObject.AddComponent<PlanetGroundGeneratorBehaviour>();
                }
            }
            
            _planetGroundGeneratorBehaviour.Initialize(Planet);
        }

        private void InitializePlanetWaterGenerator()
        {
            if (_planetWaterGeneratorBehaviour == null)
            {
                _planetWaterGeneratorBehaviour = gameObject.GetComponent<PlanetWaterGeneratorBehaviour>();

                if (_planetWaterGeneratorBehaviour == null)
                {
                    _planetWaterGeneratorBehaviour = gameObject.AddComponent<PlanetWaterGeneratorBehaviour>();
                }
            }
            
            _planetWaterGeneratorBehaviour.Initialize(Planet, _planetGroundGeneratorBehaviour.Generator);
        }

        private void InitializePlanetModelFactory()
        {
            _planetModelFactory = new PlanetModelFactory(
                _planetGroundGeneratorBehaviour.Generator,
                _planetWaterGeneratorBehaviour.Generator
            );
        }
    }
    #endif
}

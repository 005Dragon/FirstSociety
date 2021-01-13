﻿using System;
using Code.Metrics;
using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Settings;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators
{
    #if UNITY_EDITOR
    public class PlanetGroundGenerator : GeneratorBase<PlanetGroundSettings>
    {
        public GameObject Planet;
        
        public GameObject Ground;
        
        [ReadOnly]
        public Range<float> Altitude;
        
        public readonly NoiseShapeFilter LowLevelNoiseFilter;
        public readonly NoiseShapeFilter MiddleLevelNoiseFilter;
        public readonly NoiseShapeFilter HighLevelNoiseShapeFilter;
        public readonly StepGradientShapeFilter StepGradientShapeFilter;

        private readonly SphereGenerator _sphereGenerator;

        private Material _material;

        public PlanetGroundGenerator()
        {
            _sphereGenerator = new SphereGenerator();
            
            LowLevelNoiseFilter = new NoiseShapeFilter();
            MiddleLevelNoiseFilter = new NoiseShapeFilter();
            HighLevelNoiseShapeFilter = new NoiseShapeFilter();
            StepGradientShapeFilter = new StepGradientShapeFilter();

            _sphereGenerator.SphereFilters = new SphereShapeFilterBase[]
            {
                LowLevelNoiseFilter,
                MiddleLevelNoiseFilter,
                HighLevelNoiseShapeFilter,
                StepGradientShapeFilter
            };
        }
        
        public void Initialize(GameObject planet, Action<GameObject> destroyImmediate)
        {
            Planet = planet;
            DestroyImmediate = destroyImmediate;
            _sphereGenerator.Initialize(DestroyImmediate);
        }

        protected override void InitializeSettings()
        {
            base.InitializeSettings();

            Settings.ColourSettings.OnUpdate = UpdateColours;
        }

        protected override GameObject GenerateCore()
        {
            UpdateSettings();
            
            Ground = _sphereGenerator.Generate();
            Ground.transform.parent = Planet.transform;

            Altitude = _sphereGenerator.Magnitude;
            
            UpdateColours();

            return Ground;
        }

        protected override bool ValidateCore()
        {
            if (Planet == null)
            {
                return false;
            }
            
            if (Settings.ColourSettings == null)
            {
                return false;
            }

            return true;
        }

        private void UpdateSettings()
        {
            _sphereGenerator.Settings = Settings.SphereSettings;
            LowLevelNoiseFilter.Settings = Settings.LowLevelNoiseSettings;
            MiddleLevelNoiseFilter.Settings = Settings.MiddleLevelNoiseSettings;
            HighLevelNoiseShapeFilter.Settings = Settings.HighLevelNoiseSettings;
            StepGradientShapeFilter.Settings = Settings.StepGradientSettings;
        }
        
        private void UpdateColours()
        {
            _sphereGenerator.Material.color = Settings.ColourSettings.Color;
        }
    }
    #endif
}
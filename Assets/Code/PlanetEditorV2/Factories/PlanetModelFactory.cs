using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Generators;
using Code.PlanetEditorV2.Models;
using UnityEngine;

namespace Code.PlanetEditorV2.Factories
{
    public class PlanetModelFactory
    {
        private readonly PlanetGroundGenerator _groundGenerator;
        private readonly PlanetWaterGenerator _waterGenerator;
    
        public PlanetModelFactory(PlanetGroundGenerator groundGenerator, PlanetWaterGenerator waterGenerator)
        {
            _groundGenerator = groundGenerator;
        }
    
        public void CreateModel(GameObject planet)
        {
            PlanetModel model = planet.GetComponent<PlanetModel>();
    
            if (model == null)
            {
                model = planet.AddComponent<PlanetModel>();
            }
    
            model.Altitude = _groundGenerator.Altitude;

            StepGradientShapeFilter stepGradientShapeFilter = _groundGenerator.StepGradientShapeFilter;
    
            model.GroundLevels = new PlanetHeightLevel[stepGradientShapeFilter.Settings.StepsCount];
    
            for (int i = 0; i < model.GroundLevels.Length; i++)
            {
                model.GroundLevels[i] = new PlanetHeightLevel
                {
                    Index = i,
                    Altitude = stepGradientShapeFilter.StepHeight * i + model.Altitude.Min
                };
            }
        }
    }
}
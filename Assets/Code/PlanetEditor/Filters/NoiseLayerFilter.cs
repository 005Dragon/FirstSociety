using Code.PlanetEditor.Settings;

namespace Code.PlanetEditor.Filters
{
    public class NoiseLayerFilter
    {
        public SimpleNoiseFilter Filter { get; }
        
        public bool Enabled => _settings.Enabled;
        
        private readonly NoiseLayerSettings _settings;

        public NoiseLayerFilter(NoiseLayerSettings settings)
        {
            _settings = settings;
            
            Filter = new SimpleNoiseFilter(settings.NoiseSettings);
        }
    }
}
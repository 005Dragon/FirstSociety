using Code.Metrics;
using Code.PlanetEditorV2.Settings;
using UnityEngine;

namespace Code.PlanetEditorV2.Filters
{
    public class ColourFilter
    {
        private ColourSettings _settings;
        
        private readonly int _textureResolution = 150;
        
        private Range<float> _altitudeRange;
        private Texture2D _texture;

        public void UpdateSettings(ColourSettings settings, Range<float> altitude)
        {
            _settings = settings;
            _altitudeRange = altitude;

            _texture = new Texture2D(_textureResolution, height: 1);
        }
        
        public void ApplyFilter()
        {
            for (int i = 0; i < _textureResolution; i++)
            {
                _texture.SetPixel(i, 1, _settings.Gradient.Evaluate(i / (float)_textureResolution));
            }
            
            _texture.Apply();
            
            _settings.Material.SetVector(nameof(_altitudeRange), new Vector4(_altitudeRange.Min, _altitudeRange.Max));
            _settings.Material.SetTexture(nameof(_texture), _texture);
        }
    }
}
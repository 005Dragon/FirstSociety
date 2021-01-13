using UnityEngine;
using UnityEngine.UI;

namespace Code.Benchmark
{
    [RequireComponent(typeof(Text))]
    public class BenchmarkFpsCounter : MonoBehaviour
    {
        public float Fps;

        const float FpsMeasurePeriod = 0.5f;
        const string DisplayFormat = "{0} FPS";

        private int _fpsAccumulator = 0;
        private float _fpsNextPeriod = 0;

        private Text _text;

        private void Start()
        {
            _fpsNextPeriod = Time.realtimeSinceStartup + FpsMeasurePeriod;
            _text = GetComponent<Text>();
        }


        private void Update()
        {
            // measure average frames per second
            _fpsAccumulator++;
            if (Time.realtimeSinceStartup > _fpsNextPeriod)
            {
                Fps = _fpsAccumulator / FpsMeasurePeriod;
                _fpsAccumulator = 0;
                _fpsNextPeriod += FpsMeasurePeriod;
                _text.text = string.Format(DisplayFormat, Fps.ToString("N1"));
            }
        }
    }
}
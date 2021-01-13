using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Benchmark
{
    public class BenchmarkFpsMonitor : MonoBehaviour
    {
        public Canvas ResultFpsCanvas;

        public Text AverageFpsValueText;

        public Text MinimumFpsValueText;

        public Text MaximumFpsValueText;

        public BenchmarkFpsCounter FpsCounter;

        private readonly List<float> fpsValues = new List<float>();

        public void ShowResult()
        {
            AverageFpsValueText.text = fpsValues.Average().ToString("N1");
            MinimumFpsValueText.text = fpsValues.Min().ToString("N1");
            MaximumFpsValueText.text = fpsValues.Max().ToString("N1");

            ResultFpsCanvas.enabled = true;
        }
        
        private void Update()
        {
            if (Time.time > 10 && !ResultFpsCanvas.enabled)
            {
                fpsValues.Add(FpsCounter.Fps);
            }
        }
    }
}
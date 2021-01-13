using UnityEngine;

namespace Code.Benchmark
{
    public class BenchmarkPlayer : MonoBehaviour
    {
        public PlanetWalker Walker;

        public BenchmarkFpsMonitor FpsMonitor;

        public EndBenchmarkTrigger EndTrigger;

        public float Speed = 0.01f;

        private bool _isStop;

        public void QuitGame()
        {
            Application.Quit();
        }
        
        private void Update()
        {
            if (EndTrigger.EndBenchmark)
            {
                Stop();
            }
            
            if (!_isStop)
            {
                Walker.Move(Speed);
            }
        }

        private void Stop()
        {
            _isStop = true;
            
            FpsMonitor.ShowResult();
        }
    }
}
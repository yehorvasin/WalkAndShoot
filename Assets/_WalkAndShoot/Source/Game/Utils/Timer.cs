using UnityEngine;

namespace Source.Game.Utils
{
    public sealed class Timer : MonoBehaviour
    {
        private float _time = 0f;
        private bool _isStarted = false;

        public float GetTimeValue => _time;

        private void FixedUpdate()
        {
            if (!_isStarted)
                return;
            
            _time += Time.fixedDeltaTime;
        }

        public void StartTimer()
        {
            _isStarted = true;
        }

        public void StopTimer()
        {
            _isStarted = false;
        }

        public void ResetTimer()
        {
            _time = 0;
        }
    }
}

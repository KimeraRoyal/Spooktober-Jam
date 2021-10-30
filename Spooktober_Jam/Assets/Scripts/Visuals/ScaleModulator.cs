using UnityEngine;
namespace Spooktober.Visuals
{
    public class ScaleModulator : MonoBehaviour, IIsSeeded, IHasAdjustableRate
    {
        [SerializeField] private Vector3 m_scaleAmount;
        [SerializeField] private float m_modulationSpeed = 1;

        private Vector3 m_startingScale;

        private float m_seed;
        private float m_rate = 1;

        private void Awake()
        {
            m_startingScale = transform.localScale;
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            transform.localScale = m_startingScale + m_scaleAmount * Mathf.Sin(m_seed + Time.time / m_modulationSpeed * m_rate);
        }
        
        public void SetSeed(float _seed)
            => m_seed = _seed;

        public void SetRate(float _rate)
            => m_rate = _rate;
    }
}

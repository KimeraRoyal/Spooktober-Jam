using UnityEngine;
namespace Spooktober.Transform
{
    public class PositionModulator : MonoBehaviour, IIsSeeded, IHasAdjustableRate
    {
        [SerializeField] private Vector3 m_movementAmount;
        [SerializeField] private float m_modulationSpeed = 1;

        private Vector3 m_startingPosition;

        private float m_seed;
        private float m_rate = 1;

        private void Awake()
        {
            m_startingPosition = transform.localPosition;
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            transform.localPosition = m_startingPosition + m_movementAmount * Mathf.Sin(m_seed + Time.time / m_modulationSpeed * m_rate);
        }
        
        public void SetSeed(float _seed)
            => m_seed = _seed;

        public void SetRate(float _rate)
            => m_rate = _rate;
    }
}

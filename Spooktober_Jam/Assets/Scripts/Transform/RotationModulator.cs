using UnityEngine;
namespace Spooktober.Transform
{
    public class RotationModulator : MonoBehaviour, IIsSeeded, IHasAdjustableRate
    {
        [SerializeField] private float m_rotationAmount;
        [SerializeField] private float m_modulationSpeed = 1;

        private float m_startingRotation;

        private float m_seed;
        private float m_rate = 1;

        private void Awake()
        {
            m_startingRotation = transform.localEulerAngles.z;
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            transform.localRotation = Quaternion.Euler(0, 0, m_startingRotation + m_rotationAmount * Mathf.Sin(m_seed + Time.time / m_modulationSpeed * m_rate));
        }
        
        public void SetSeed(float _seed)
            => m_seed = _seed;

        public void SetRate(float _rate)
            => m_rate = _rate;
    }
}

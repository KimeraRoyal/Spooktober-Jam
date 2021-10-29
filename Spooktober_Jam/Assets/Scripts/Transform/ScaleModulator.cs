using UnityEngine;
namespace Spooktober.Transform
{
    public class ScaleModulator : MonoBehaviour
    {
        [SerializeField] private Vector3 m_scaleAmount;
        [SerializeField] private float m_modulationSpeed = 1;

        private Vector3 m_startingScale;

        private void Awake()
        {
            m_startingScale = transform.localScale;
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            transform.localScale = m_startingScale + m_scaleAmount * Mathf.Sin(Time.time / m_modulationSpeed);
        }
    }
}

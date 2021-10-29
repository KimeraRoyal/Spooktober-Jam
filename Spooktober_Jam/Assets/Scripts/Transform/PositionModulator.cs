using UnityEngine;
namespace Spooktober.Transform
{
    public class PositionModulator : MonoBehaviour
    {
        [SerializeField] private Vector3 m_movementAmount;
        [SerializeField] private float m_modulationSpeed = 1;

        private Vector3 m_startingPosition;

        private void Awake()
        {
            m_startingPosition = transform.localPosition;
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            transform.localPosition = m_startingPosition + m_movementAmount * Mathf.Sin(Time.time / m_modulationSpeed);
        }
    }
}

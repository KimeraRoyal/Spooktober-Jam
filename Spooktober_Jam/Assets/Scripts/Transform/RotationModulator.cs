using UnityEngine;
namespace Spooktober.Transform
{
    public class RotationModulator : MonoBehaviour
    {
        [SerializeField] private float m_rotationAmount;
        [SerializeField] private float m_modulationSpeed = 1;

        private float m_startingRotation;

        private void Awake()
        {
            m_startingRotation = transform.localEulerAngles.z;
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            transform.localRotation = Quaternion.Euler(0, 0, m_startingRotation + m_rotationAmount * Mathf.Sin(Time.time / m_modulationSpeed));
        }
    }
}

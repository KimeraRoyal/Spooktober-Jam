using UnityEngine;
namespace Spooktober.Visuals
{
    public class PositionShake : MonoBehaviour
    {
        [SerializeField] private Vector3 m_shakeDistance;
        [SerializeField] private float m_shakeAmount = 1;
        [SerializeField] private int m_shakeLength = 1;

        private Vector3 m_startingPosition;

        private int m_currentFrame;

        private void Awake()
        {
            m_startingPosition = transform.localPosition;
        }
    
        private void Update()
        {
            if (m_shakeLength <= 0 || m_shakeAmount <= 0 || m_currentFrame++ % m_shakeLength != 0) { return; }

            var shake = new Vector3();
            for (var i = 0; i < 3; i++)
            {
                shake[i] = Random.Range(-m_shakeDistance[i], m_shakeDistance[i]);
            }
            transform.localPosition = m_startingPosition + shake * m_shakeAmount;
        }
    }
}

using System;
using UnityEngine;
namespace Spooktober.Visuals
{
    public class LerpPositionToTarget : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        [SerializeField] private Vector3 m_followAmount;
        [SerializeField] private float m_smoothTime;

        private Vector3 m_startingPosition;
        
        private Vector3 m_velocity;

        public Transform Target
        {
            get => m_target;
            set => m_target = value;
        }

        private void Awake()
        {
            m_startingPosition = transform.position;
        }

        public void SetTarget(Transform _target)
            => Target = _target;

        private void Update()
        {
            Vector3 targetPosition;
            if (!m_target) { targetPosition = m_startingPosition; }
            else
            {
                targetPosition = new Vector3();
                for (var i = 0; i < 3; i++)
                {
                    targetPosition[i] = Mathf.Lerp(m_startingPosition[i], m_target.position[i], m_followAmount[i]);
                }
            }
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, m_smoothTime);
        }
    }
}

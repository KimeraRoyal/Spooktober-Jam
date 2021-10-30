using UnityEngine;
namespace Spooktober.Visuals
{
    public class SpriteColourModulator : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;
        
        [SerializeField] private Gradient m_gradient;
        [SerializeField] private float m_modulationSpeed = 1;

        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
        private void Update()
        {
            if (m_modulationSpeed <= 0) { return; }
            m_spriteRenderer.color = m_gradient.Evaluate(Mathf.Clamp(Mathf.Sin(Time.time / m_modulationSpeed) / 2.0f + 0.5f, 0.0f, 1.0f));
        }
    }
}

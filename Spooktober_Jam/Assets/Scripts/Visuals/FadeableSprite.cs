using DG.Tweening;
using UnityEngine;
namespace Spooktober.Visuals
{
    public class FadeableSprite : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        [SerializeField] private Color m_closedColor, m_openColor;
        [SerializeField] private float m_fadeTime;
        
        [SerializeField] private bool m_enabled;
        private bool m_wasEnabled;

        private Sequence m_sizeSequence;
        private Color m_targetColor;

        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        protected virtual void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            m_wasEnabled = m_enabled;
        }

        private void Update()
        {
            if(m_enabled != m_wasEnabled) Resize(m_enabled? m_openColor : m_closedColor);
            m_wasEnabled = m_enabled;
        }

        private void Resize(Color _newColor)
        {
            if (m_sizeSequence is { active: true })
            {
                m_spriteRenderer.color = m_targetColor;
                m_sizeSequence.Kill();
            }
            m_targetColor = _newColor;
            
            m_sizeSequence = DOTween.Sequence();
            m_sizeSequence.Append(m_spriteRenderer.DOColor(_newColor, m_fadeTime));
            m_sizeSequence.Play();
        }

        public void SetEnabled(bool _enabled)
            => Enabled = _enabled;
    }
}

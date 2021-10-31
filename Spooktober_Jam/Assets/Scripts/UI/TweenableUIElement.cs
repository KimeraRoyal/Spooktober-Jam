using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Spooktober.UI
{
    public class TweenableUIElement : MonoBehaviour
    {
        private RectTransform m_rectTransform;

        [SerializeField] private Vector2 m_closedHeight, m_openHeight;
        [SerializeField] private float m_resizeTime;
        
        [SerializeField] private bool m_enabled;
        private bool m_wasEnabled;

        private Sequence m_sizeSequence;
        private Vector2 m_targetSize;

        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        protected virtual void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();

            m_wasEnabled = m_enabled;
        }

        private void Update()
        {
            if(m_enabled != m_wasEnabled) Resize(m_enabled? m_openHeight : m_closedHeight);
            m_wasEnabled = m_enabled;
        }

        private void Resize(Vector2 _newSize)
        {
            if (m_sizeSequence is { active: true })
            {
                m_rectTransform.sizeDelta = m_targetSize;
                m_sizeSequence.Kill();
            }
            m_targetSize = _newSize;
            
            m_sizeSequence = DOTween.Sequence();
            m_sizeSequence.Append(m_rectTransform.DOSizeDelta(_newSize, m_resizeTime));
            m_sizeSequence.Play();
        }

        public void SetEnabled(bool _enabled)
            => Enabled = _enabled;
    }
}

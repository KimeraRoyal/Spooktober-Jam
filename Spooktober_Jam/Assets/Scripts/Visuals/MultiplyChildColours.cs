using System;
using Spooktober.Character;
using UnityEngine;
namespace Spooktober.Visuals
{
    public class MultiplyChildColours : MonoBehaviour
    {
        private SpriteRenderer[] m_spriteRenderers;

        private Color[] m_startingColours;

        [SerializeField] private Color m_multiplyColor;

        public Color MultiplyColour
        {
            get => m_multiplyColor;
            set => m_multiplyColor = value;
        }
        
        private void Awake()
        {
            m_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            m_startingColours = new Color[m_spriteRenderers.Length];
        }

        private void Start()
        {
            for (var i = 0; i < m_spriteRenderers.Length; i++)
            {
                m_startingColours[i] = m_spriteRenderers[i].color;
            }
        }

        private void Update()
        {
            for (var i = 0; i < m_spriteRenderers.Length; i++)
            {
                m_spriteRenderers[i].color = m_startingColours[i] * m_multiplyColor;
            }
        }
    }
}

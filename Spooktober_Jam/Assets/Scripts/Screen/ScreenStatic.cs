using System;
using System.Collections;
using System.Collections.Generic;
using Spooktober.Screen;
using UnityEngine;

namespace Spooktober
{
    public class ScreenStatic : SimpleBlit
    {
        private static readonly int s_slopeProperty = Shader.PropertyToID("_Slope");
        private static readonly int s_amountProperty = Shader.PropertyToID("_Amount");
        
        [SerializeField] private Texture2D m_slopeTexture;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_amount;

        public Texture2D SlopeTexture
        {
            get => m_slopeTexture;
            set => m_slopeTexture = value;
        }

        public float Amount
        {
            get => m_amount;
            set => m_amount = value;
        }

        private int m_currentInstanceId;

        private void Update()
        {
            UpdateSlopeTexture();
            UpdateAmount();
        }

        private void UpdateSlopeTexture()
        {
            var instanceId = m_slopeTexture.GetInstanceID();
            if (instanceId == m_currentInstanceId) { return; }
            m_currentInstanceId = instanceId;
            
            m_material.SetTexture(s_slopeProperty, m_slopeTexture);
        }

        private void UpdateAmount()
        {
            m_material.SetFloat(s_amountProperty, m_amount);
        }
    }
}

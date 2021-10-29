using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober.Character
{
    public class Person : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_bodyRenderer, m_headRenderer, m_eyeRenderer, m_noseRenderer, m_mouthRenderer, m_hairRenderer;

        public Sprite Body
        {
            set  => m_bodyRenderer.sprite = value;
        }
        
        public Sprite Head
        {
            set  => m_headRenderer.sprite = value;
        }
        
        public Sprite Eyes
        {
            set  => m_eyeRenderer.sprite = value;
        }
        
        public Sprite Nose
        {
            set  => m_noseRenderer.sprite = value;
        }
        
        public Sprite Mouth
        {
            set  => m_mouthRenderer.sprite = value;
        }
        
        public Sprite Hair
        {
            set  => m_hairRenderer.sprite = value;
        }
    }
}

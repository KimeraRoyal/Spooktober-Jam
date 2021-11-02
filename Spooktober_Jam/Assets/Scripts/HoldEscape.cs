using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spooktober
{
    public class HoldEscape : MonoBehaviour
    {
        private Image m_image;
        private AudioSource[] m_audioSources;

        [SerializeField] private float m_escapeDuration;

        private float m_holdTime;

        private void Awake()
        {
            m_image = GetComponent<Image>();

            m_audioSources = FindObjectsOfType<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                m_holdTime += Time.deltaTime;
            }
            else
            {
                m_holdTime = 0;
            }

            var progress = Mathf.Clamp(m_holdTime / m_escapeDuration, 0, 1);
            var color = m_image.color;
            color.a = progress;
            m_image.color = color;

            foreach (var audioSource in m_audioSources)
            {
                audioSource.volume = 1 - progress;
            }

            if (m_holdTime < m_escapeDuration) { return; }
            Application.Quit(0);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Spooktober
{
    public class QuestionCount : MonoBehaviour
    {
        private QuestionManager m_questionManager;

        private TMP_Text m_textMeshProText;

        [SerializeField] private string m_counterText;

        private void Awake()
        {
            m_questionManager = FindObjectOfType<QuestionManager>();

            m_textMeshProText = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            m_textMeshProText.text = string.Format(m_counterText, m_questionManager.RemainingQuestions);
        }
    }
}

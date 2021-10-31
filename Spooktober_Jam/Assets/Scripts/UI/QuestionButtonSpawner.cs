using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spooktober.UI;
using UnityEngine;

namespace Spooktober
{
    public class QuestionButtonSpawner : MonoBehaviour
    {
        private QuestionManager m_questionManager;
        
        private Queue<QuestionButton> m_inactiveButtons;
        
        [SerializeField] private GameObject m_questionButtonPrefab;

        [SerializeField] private float m_questionButtonOffset;
        [SerializeField] private float m_questionButtonOpenDelay;

        private QuestionButton[] m_currentButtonElements;
        
        private Sequence m_buttonSequence;

        private void Awake()
        {
            m_questionManager = FindObjectOfType<QuestionManager>();
            
            m_inactiveButtons = new Queue<QuestionButton>();
        }

        public void SpawnQuestionButtons(int _count)
        {
            m_currentButtonElements = new QuestionButton[_count];
            
            var startingOffset = m_questionButtonOffset * (_count - 1) / 2.0f;
            for (var i = 0; i < _count; i++)
            {
                m_currentButtonElements[i] = GetQuestionButton(new Vector2(0, startingOffset - m_questionButtonOffset * i));
            }
            PlayButtonSequence(true);
        }

        public void PlayButtonSequence(bool _enabled)
        {
            if(m_buttonSequence is {active:true}) {m_buttonSequence.Kill();}
            
            var sequence = DOTween.Sequence();
            for(var i = 0; i < m_currentButtonElements.Length; i++)
            {
                var buttonElement = m_currentButtonElements[i];
                var questionText = m_questionManager.GetQuestion(i).Text;
                
                sequence.AppendCallback(() => buttonElement.Enabled = _enabled);
                sequence.AppendCallback(() => buttonElement.DialogueBox.WriteText(questionText));
                
                if (i >= m_currentButtonElements.Length - 1) break;
                sequence.AppendInterval(m_questionButtonOpenDelay);
            }
            sequence.Play();
            
            foreach (var buttonElement in m_currentButtonElements)
            {
                buttonElement.Interactable = _enabled;
                
                if (_enabled) { continue; }
                m_inactiveButtons.Enqueue(buttonElement);
            }
        }
        
        private QuestionButton GetQuestionButton(Vector2 _position)
        {
            var button = m_inactiveButtons.Count > 0 ? m_inactiveButtons.Dequeue() : Instantiate(m_questionButtonPrefab, transform).GetComponent<QuestionButton>();
            
            var buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = _position;

            return button;
        }

    }
}

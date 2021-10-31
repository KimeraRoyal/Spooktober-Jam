using System;
using System.Collections;
using System.Collections.Generic;
using Spooktober.Character;
using Spooktober.Dialogue;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spooktober
{
    public class QuestionManager : MonoBehaviour
    {
        private DialogueManager m_dialogueManager;
        
        [SerializeField] private int m_maxQuestions;
        [SerializeField] private int m_offerCount;
        
        [SerializeField] private float m_offeredStatMultiplier;
        [SerializeField] private float m_chosenStatMultiplier;

        [SerializeField] private int m_maxRerolls;

        private Dialogue.Types.Dialogue[] m_currentQuestions;
        
        private float[] m_statWeights;
        
        private int m_count;

        public int RemainingQuestions => m_maxQuestions - m_count;

        private void Awake()
        {
            m_dialogueManager = FindObjectOfType<DialogueManager>();
            
            m_statWeights = new float[Enum.GetNames(typeof(Stat)).Length];
            for (var i = 0; i < m_statWeights.Length; i++)
            {
                m_statWeights[i] = 1.0f;
            }
        }

        private void Start()
        {
            OfferQuestions();
        }

        public void OfferQuestions()
        {
            if (m_count >= m_maxQuestions) return;

            var stats = new List<Stat>();
            for (var i = 0; i < Enum.GetNames(typeof(Stat)).Length; i++)
            {
                stats.Add((Stat) i);
            }
            
            for (var i = 0; i < m_offerCount; i++)
            {
                RollStat(stats);
            }
            AdjustOfferedWeights(stats);

            m_currentQuestions = new Dialogue.Types.Dialogue[m_offerCount];
            for (var i = 0; i < m_offerCount; i++)
            {
                m_currentQuestions[i] = m_dialogueManager.GetRandomQuestion(stats[i]);
                Debug.Log(m_currentQuestions[i].Text);
            }
            
            m_count++;
        }

        public Dialogue.Types.Dialogue GetQuestion(int _index)
            => m_currentQuestions[_index];

        private void RollStat(IList<Stat> _stats)
        {
            for (var i = 0; i < m_maxRerolls; i++)
            {
                var statIndex = Random.Range(0, _stats.Count);
                
                var statWeight = m_statWeights[(int)_stats[statIndex]];
                if (i < m_maxRerolls - 1 && (statWeight >= 1.0f || Random.Range(0.0f, 1.0f) > statWeight)) continue;
                
                _stats.RemoveAt(statIndex);
                break;
            }
        }

        private void AdjustOfferedWeights(IList<Stat> _stats)
        {
            foreach (var stat in _stats)
            {
                m_statWeights[(int)stat] *= m_offeredStatMultiplier;
            }
        }
    }
}

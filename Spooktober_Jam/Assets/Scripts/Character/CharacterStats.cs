using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spooktober.Character
{
    public class CharacterStats
    {
        private readonly int[] m_statAmounts;
        private int m_statTotal;

        private Stat m_highestStat;

        public int StatTotal => m_statTotal;

        public Stat HighestStat => m_highestStat;
        
        public CharacterStats(int _statTotal = 100)
        {
            m_statAmounts = new int[Enum.GetNames(typeof(Stat)).Length];
            m_statTotal = _statTotal;
            
            var statWeights = PopulateUnbalancedStatValues(out var statTotal);
            SetStatAmounts(statWeights, statTotal);
        }

        public int GetStat(Stat _stat)
            => m_statAmounts[(int) _stat];

        private float[] PopulateUnbalancedStatValues(out float _total)
        {
            var statWeights = new float[m_statAmounts.Length];
            
            _total = 0.0f;
            for (var i = 0; i < m_statAmounts.Length; i++)
            {
                statWeights[i] = Random.Range(0.0f, 1.0f);
                _total += statWeights[i];
            }

            return statWeights;
        }

        private void SetStatAmounts(IReadOnlyList<float> _statWeights, float _weightTotal)
        {
            var highestStatValue = 0;

            var statTotal = 0;
            for (var i = 0; i < m_statAmounts.Length; i++)
            {
                m_statAmounts[i] = Mathf.RoundToInt((_statWeights[i] / _weightTotal) * m_statTotal);
                statTotal += m_statAmounts[i];

                if (m_statAmounts[i] <= highestStatValue) { continue; }
                highestStatValue = m_statAmounts[i];
                m_highestStat = (Stat) i;
            }

            if (statTotal == m_statTotal) { return; }
            m_statAmounts[Random.Range(0, m_statAmounts.Length)] += m_statTotal - statTotal;
        }
    }
}

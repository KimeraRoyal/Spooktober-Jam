using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spooktober.Character
{
    public class CharacterStats : MonoBehaviour
    {   
        private float[] m_statAmounts;
        
        private void Awake()
        {
            var statCount = Enum.GetNames(typeof(Stats)).Length;
            m_statAmounts = new float[statCount];

            PopulateUnbalancedStatValues(statCount, out var statTotal);
            SetStatAmounts(statTotal);
        }

        private void PopulateUnbalancedStatValues(int _count, out float _total)
        {
            _total = 0.0f;
            for (var i = 0; i < _count; i++)
            {
                m_statAmounts[i] = Random.Range(0.0f, 1.0f);
                _total += m_statAmounts[i];
            }
        }

        private void SetStatAmounts(float _statTotal)
        {
            for (var i = 0; i < _statTotal; i++)
            {
                m_statAmounts[i] /= _statTotal;
            }
        }
    }
}

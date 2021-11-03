using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober
{
    [Serializable]
    public class SaveData
    {
        [SerializeField] private int m_totalScore;
        [SerializeField] private int m_highestScore;
        
        [SerializeField] private bool[] m_hasSeenMonsters;

        [SerializeField] private int m_lostToEntity;

        public int TotalScore => m_totalScore;
        public int HighestScore => m_highestScore;
        
        public bool[] HasSeenMonsters => m_hasSeenMonsters;

        public int LostToEntity => m_lostToEntity;

        public SaveData(int _totalScore, int _highestScore, bool[] _hasSeenMonsters, int _lostToEntity)
        {
            m_totalScore = _totalScore;
            m_highestScore = _highestScore;

            m_hasSeenMonsters = _hasSeenMonsters;

            m_lostToEntity = _lostToEntity;
        }
    }
}

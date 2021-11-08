using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober
{
    [Serializable]
    public class SaveData
    {
        [SerializeField] private int m_version;
        
        [SerializeField] private int m_totalScore;
        [SerializeField] private int m_highestScore;
        
        [SerializeField] private bool[] m_hasSeenMonsters;

        [SerializeField] private int m_lostToEntity;

        public int Version => m_version;

        public int TotalScore => m_totalScore;
        public int HighestScore => m_highestScore;
        
        public bool[] HasSeenMonsters => m_hasSeenMonsters;

        public int LostToEntity => m_lostToEntity;

        public SaveData(int _version, int _totalScore, int _highestScore, bool[] _hasSeenMonsters, int _lostToEntity)
        {
            m_version = _version;
            
            m_totalScore = _totalScore;
            m_highestScore = _highestScore;

            m_hasSeenMonsters = _hasSeenMonsters;

            m_lostToEntity = _lostToEntity;
        }
    }
}

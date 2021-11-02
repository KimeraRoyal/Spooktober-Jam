using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Spooktober.UI
{
    public class ScoreCount : MonoBehaviour
    {
        [Serializable]
        private struct RankInfo
        {
            [SerializeField] private string m_name;
            [SerializeField] private int m_minScore;

            public string Name => m_name;
            public int MinScore => m_minScore;
        }
        
        [SerializeField] private TMP_Text m_scoreText;
        [SerializeField] private TMP_Text m_highScoreText;

        [SerializeField] private string m_scoreString;
        [SerializeField] private string m_highScoreString;

        [SerializeField] private RankInfo[] m_ranks;

        private int m_lastScore = -1;
        private int m_lastHighScore = -1;

        private void Update()
        {
            m_lastScore = UpdateScore(m_scoreText, GameManager.totalScore, m_lastScore, m_scoreString);
            m_lastHighScore = UpdateRank(m_highScoreText, GameManager.highScore, m_lastHighScore, m_highScoreString);
        }

        private int UpdateScore(TMP_Text _display, int _value, int _lastValue, string _scoreString)
        {
            if (_lastValue != _value) { _display.text = string.Format(_scoreString, _value); }
            return _value;
        }

        private int UpdateRank(TMP_Text _display, int _value, int _lastValue, string _scoreString)
        {
            if (_lastValue != _value) { _display.text = string.Format(_scoreString, GetRankName(_value)); }
            return _value;
        }

        private string GetRankName(int _score)
        {
            var highestRank = -1;
            var rankName = "";
            
            foreach (var rank in m_ranks)
            {
                if (rank.MinScore > _score || rank.MinScore <= highestRank) continue;
                
                highestRank = rank.MinScore;
                rankName = rank.Name;
            }

            return rankName;
        }
    }
}

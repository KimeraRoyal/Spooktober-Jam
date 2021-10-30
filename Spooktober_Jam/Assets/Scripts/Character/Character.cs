using System;
using UnityEngine;
namespace Spooktober.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string m_characterName;

        public string CharacterName => m_characterName;

        public float cool;
        public float cute;
        public float serious;
        public float childish;
        public float cynical;
        public float father;

        public CharacterStats CharacterStats
        {
            get;
            private set;
        }

        private void Awake()
        {
            CharacterStats = new CharacterStats();
            cool = CharacterStats.GetStat(Stat.Cool);
            cute = CharacterStats.GetStat(Stat.Cute);
            serious = CharacterStats.GetStat(Stat.Serious);
            childish = CharacterStats.GetStat(Stat.Childish);
            cynical = CharacterStats.GetStat(Stat.Cynical);
            father = CharacterStats.GetStat(Stat.Father);
        }
    }
}
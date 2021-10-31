using System;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Spooktober.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string m_characterName;

        public string CharacterName => m_characterName;

        [ShowInInspector]
        public int Cool => CharacterStats?.GetStat(Stat.Cool) ?? 0;
        
        [ShowInInspector]
        public int Cute => CharacterStats?.GetStat(Stat.Cute) ?? 0;
        
        [ShowInInspector]
        public int Serious => CharacterStats?.GetStat(Stat.Serious) ?? 0;
        
        [ShowInInspector]
        public int Childish => CharacterStats?.GetStat(Stat.Childish) ?? 0;
        
        [ShowInInspector]
        public int Cynical => CharacterStats?.GetStat(Stat.Cynical) ?? 0;

        [ShowInInspector]
        public int Father => CharacterStats?.GetStat(Stat.Father) ?? 0;

        public CharacterStats CharacterStats
        {
            get;
            private set;
        }

        private void Awake()
        {
            Generate();
        }

        private void Generate()
        {
            CharacterStats = new CharacterStats();
        }
    }
}
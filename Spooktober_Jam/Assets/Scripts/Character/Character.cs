using System;
using UnityEngine;
namespace Spooktober.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string m_characterName;

        public string CharacterName => m_characterName;

        public CharacterStats CharacterStats
        {
            get;
            private set;
        }

        private void Awake()
        {
            CharacterStats = new CharacterStats();
        }
    }
}
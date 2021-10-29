using UnityEngine;
namespace Spooktober.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string m_characterName;

        public string CharacterName => m_characterName;
    }
}
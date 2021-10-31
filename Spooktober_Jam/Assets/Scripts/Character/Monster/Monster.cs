using UnityEngine;
namespace Spooktober.Character.Monster
{
    public class Monster : Character
    {
        [SerializeField] private int m_introDialogueLines;

        public int IntroDialogueLines => m_introDialogueLines;
    }
}

using System;
using Spooktober.Dialogue;
using UnityEngine;
namespace Spooktober.Character.People
{
    public class Person : Character
    {
        private DialogueManager m_dialogueManager;

        public DialogueBox m_dialogueBox;

        protected override void Awake()
        {
            base.Awake();

            m_dialogueManager = FindObjectOfType<DialogueManager>();
        }

        public string GetAnswer(Dialogue.Types.Dialogue _question, Stat _stat)
            => m_dialogueManager.GetAnswer(_stat, _question.Type, CharacterStats).Text;
        
    }
}

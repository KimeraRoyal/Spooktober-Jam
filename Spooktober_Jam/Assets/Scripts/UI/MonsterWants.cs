using System;
using System.Runtime.CompilerServices;
using Spooktober.Character;
using Spooktober.Dialogue;
using TMPro;
using UnityEngine;
namespace Spooktober.UI
{
    public class MonsterWants : MonoBehaviour
    {
        private DialogueManager m_dialogueManager;
        
        private MonsterScript m_monsterScript;

        private DialogueBox m_dialogueBox;

        private void Awake()
        {
            m_dialogueManager = FindObjectOfType<DialogueManager>();
            
            m_monsterScript = FindObjectOfType<MonsterScript>();

            m_dialogueBox = GetComponentInChildren<DialogueBox>();
        }

        public void UpdateText(string _id, int _count = 0, Action _onFinished = null)
        {
            if(_count < 1)
            {
                m_dialogueBox.WriteText(FormatString(_id));
            }
            else
            {
                var lines = new string[_count];
                for (var i = 0; i < _count; i++)
                {
                    lines[i] = FormatString(_id + "_" + i);
                }
                m_dialogueBox.WriteText(lines, -1, _onFinished );
            }
        }

        private string FormatString(string _id)
        {
            var monsterText = m_dialogueManager.TryGetDialogue(_id);
            var loveName = m_dialogueManager.GetStatName(m_monsterScript.loves);
            var likeName = m_dialogueManager.GetStatName(m_monsterScript.likes);
            var hateName = m_dialogueManager.GetStatName(m_monsterScript.hates);
            var specialName = m_dialogueManager.GetStatName(m_monsterScript.special);

            return string.Format(monsterText, loveName, likeName, hateName, specialName);
        }
    }
}

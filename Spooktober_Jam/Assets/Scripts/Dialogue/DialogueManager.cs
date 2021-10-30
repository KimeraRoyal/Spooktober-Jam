using System;
using System.Collections.Generic;
using Spooktober.Character;
using Spooktober.Dialogue.Types;
using UnityEngine;
namespace Spooktober.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextAsset m_dialogueTextAsset;

        private Dictionary<Stat, Questions> m_questions;
        private Dictionary<Stat, Answers> m_answers;
        private Dictionary<string, string> m_generalDialogue;

        private void Awake()
        {
            m_questions = new Dictionary<Stat, Questions>();
            m_answers = new Dictionary<Stat, Answers>();
            m_generalDialogue = new Dictionary<string, string>();
            
            var serializedDialogueFile = JsonUtility.FromJson<SerializedDialogueFile>(m_dialogueTextAsset.text);

            LoadTexts(serializedDialogueFile);
            LoadGroups(serializedDialogueFile);
        }

        public Types.Dialogue GetRandomQuestion(Stat _stat)
            => m_questions[_stat].GetRandomQuestion();

        public string TryGetDialogue(string _id)
            => m_generalDialogue.TryGetValue(_id, out var dialogue) ? dialogue : "";

        private void LoadTexts(SerializedDialogueFile _serializedDialogueFile)
        {
            if (_serializedDialogueFile.Texts == null) { return;}
            
            foreach (var text in _serializedDialogueFile.Texts)
            {
                m_generalDialogue.Add(text.ID, text.Text);
            }
        }

        private void LoadGroups(SerializedDialogueFile _serializedDialogueFile)
        {
            if (_serializedDialogueFile.Groups == null) { return;}
            
            foreach (var group in _serializedDialogueFile.Groups)
            {
                var groupName = group.Group.Split('_');
                
                var statNames = Enum.GetNames(typeof(Stat));
                var groupStat = -1;

                for (var i = 0; i < statNames.Length; i++)
                {
                    if (groupName[groupName.Length - 1] != statNames[i].ToLower()) { continue; }
                    
                    groupStat = i;
                    break;
                }

                if (groupStat < 0) { continue; }
                
                switch (groupName[0])
                {
                    case "questions":
                        m_questions.Add((Stat) groupStat, new Questions(group.Texts));
                        break;
                    case "answers":
                        m_answers.Add((Stat) groupStat, new Answers(group.Texts, groupName[1]));
                        break;
                }
            }
        }
    }
}
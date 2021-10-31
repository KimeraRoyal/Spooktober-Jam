using System;
using System.Collections.Generic;
using Spooktober.Character;
using Spooktober.Dialogue.Types;
using UnityEngine;
namespace Spooktober.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextAsset[] m_dialogueTextAssets;

        private Dictionary<Stat, Types.Questions> m_questions;
        private Dictionary<Stat, Answers> m_answers;
        
        private Dictionary<Stat, Types.Questions> m_statNames;

        private Dictionary<string, WinDialogues> m_winDialogues;
        private Dictionary<string, string> m_generalDialogue;

        private void Awake()
        {
            m_questions = new Dictionary<Stat, Types.Questions>();
            m_answers = new Dictionary<Stat, Answers>();
            m_statNames = new Dictionary<Stat, Types.Questions>();

            m_winDialogues = new Dictionary<string, WinDialogues>();
            m_generalDialogue = new Dictionary<string, string>();

            foreach (var dialogueTextAsset in m_dialogueTextAssets)
            {
                LoadDialogueAsset(dialogueTextAsset);
            }
        }

        public Types.Dialogue GetRandomQuestion(Stat _stat)
            => m_questions[_stat].GetRandomQuestion();

        public Answer GetAnswer(Stat _stat, string _type, CharacterStats _characterStats)
        {
            var answerStat = _type == "object" ? _characterStats.HighestStat : _stat;
            return m_answers[answerStat].GetAnswer(answerStat, _type, _characterStats);
        }

        public string GetWinDialogue(string _monster, int _score)
            => m_winDialogues[_monster].GetAnswer(_score).Text;

        public string GetStatName(Stat _stat)
            => m_statNames[_stat].GetRandomQuestion().Text;

        public string TryGetDialogue(string _id)
            => m_generalDialogue.TryGetValue(_id, out var dialogue) ? dialogue : "";

        private void LoadDialogueAsset(TextAsset _dialogueAsset)
        {
            var serializedDialogueFile = JsonUtility.FromJson<SerializedDialogueFile>(_dialogueAsset.text);

            LoadTexts(serializedDialogueFile);
            LoadGroups(serializedDialogueFile);
        }
        
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
                        m_questions.Add((Stat) groupStat, new Types.Questions(group.Texts));
                        break;
                    case "answers":
                        TryGetAnswers((Stat) groupStat).AddAnswers(group.Texts, groupName[1]);
                        break;
                    case "stat":
                        m_statNames.Add((Stat) groupStat, new Types.Questions(group.Texts));
                        break;
                    case "win":
                        m_winDialogues.Add(groupName[1], new WinDialogues(group.Texts));
                        break;
                }
            }
        }

        private Answers TryGetAnswers(Stat _stat)
        {
            if (m_answers.TryGetValue(_stat, out var value))
            {
                return value;
            }

            value = new Answers();
            m_answers.Add(_stat, value);
            return value;
        }
    }
}
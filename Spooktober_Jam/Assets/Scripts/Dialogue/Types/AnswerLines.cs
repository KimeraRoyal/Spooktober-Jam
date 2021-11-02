using System;
using System.Collections.Generic;
using System.Linq;
using Spooktober.Character;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Spooktober.Dialogue.Types
{
    public class AnswerLine : DialogueLine
    {
        private int m_value;

        public int Value => m_value;

        public AnswerLine(string _type, string _text, int _value) : base(_type, _text)
        {
            m_value = _value;
        }
    }
    
    public class AnswerLines
    {
        private class AnswerType
        {
            private string m_type;
        
            private AnswerLine[] m_answers;

            public string Type => m_type;

            public AnswerType(IReadOnlyList<SerializedDialogueFile.SerializedText> _serializedTexts, string _type)
            {
                m_answers = new AnswerLine[_serializedTexts.Count];
                m_type = _type;

                for (var i = 0; i < _serializedTexts.Count; i++)
                {
                    var serializedText = _serializedTexts[i];
                    m_answers[i] = new AnswerLine(_type, serializedText.Text, serializedText.Value);
                }
            }

            public AnswerLine GetAnswer(Stat _stat, CharacterStats _characterStats)
            {
                //Get the highest Value in the answer list, where the Value isn't greater than this character's stat.
                var highestValue = m_answers.Select(answer => answer.Value).Where(value => value <= _characterStats.GetStat(_stat)).Max();
                
                //Get all answers with the Value found above.
                var possibleAnswers = m_answers.Where(answer => answer.Value == highestValue).ToList();
                
                //Pick a random answer.
                return possibleAnswers[Random.Range(0, possibleAnswers.Count)];
            }

            public AnswerLine GetRandomAnswer()
                => m_answers[Random.Range(0, m_answers.Length)];
        }

        private Dictionary<string, AnswerType> m_answerTypes;

        public AnswerLines()
        {
            m_answerTypes = new Dictionary<string, AnswerType>();
        }

        public AnswerLine GetAnswer(Stat _stat, string _type, CharacterStats _characterStats)
            => m_answerTypes[_type].GetAnswer(_stat, _characterStats);

        public AnswerLine GetRandomAnswer(string _type)
            => m_answerTypes[_type].GetRandomAnswer();

        public void AddAnswers(IReadOnlyList<SerializedDialogueFile.SerializedText> _serializedTexts, string _type)
        {
            m_answerTypes.Add(_type, new AnswerType(_serializedTexts, _type));
        }
    }
}

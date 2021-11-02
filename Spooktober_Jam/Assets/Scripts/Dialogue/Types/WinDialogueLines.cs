using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Spooktober.Dialogue.Types
{
    public class WinDialogueLines
    {
        private AnswerLine[] m_dialogues;
        
        public WinDialogueLines(IReadOnlyList<SerializedDialogueFile.SerializedText> _serializedTexts)
        {
            m_dialogues = new AnswerLine[_serializedTexts.Count];

            for (var i = 0; i < _serializedTexts.Count; i++)
            {
                var serializedText = _serializedTexts[i];
                m_dialogues[i] = new AnswerLine("", serializedText.Text, serializedText.Value);
            }
        }

        public DialogueLine GetAnswer(int _score)
        {
            //Get the highest Value in the answer list, where the Value isn't greater than this character's stat.
            var highestValue = m_dialogues.Select(answer => answer.Value).Where(value => value <= _score).Max();
                
            //Get all answers with the Value found above.
            var possibleAnswers = m_dialogues.Where(answer => answer.Value == highestValue).ToList();
                
            //Pick a random answer.
            return possibleAnswers[Random.Range(0, possibleAnswers.Count)];
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Spooktober.Dialogue.Types
{
    public class WinDialogues
    {
        private Answer[] m_dialogues;
        
        public WinDialogues(IReadOnlyList<SerializedDialogueFile.SerializedText> _serializedTexts)
        {
            m_dialogues = new Answer[_serializedTexts.Count];

            for (var i = 0; i < _serializedTexts.Count; i++)
            {
                var serializedText = _serializedTexts[i];
                m_dialogues[i] = new Answer("", serializedText.Text, serializedText.Value);
            }
        }

        public Dialogue GetAnswer(int _score)
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

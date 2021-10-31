using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
namespace Spooktober.Dialogue.Types
{
    public class Questions
    {
        private Dialogue[] m_questions;

        public Questions(IReadOnlyList<SerializedDialogueFile.SerializedText> _serializedTexts)
        {
            m_questions = new Dialogue[_serializedTexts.Count];

            for (var i = 0; i < _serializedTexts.Count; i++)
            {
                var serializedText = _serializedTexts[i];
                m_questions[i] = new Dialogue(serializedText.Type, serializedText.Text);
            }
        }

        public Dialogue GetQuestion(int _index)
            => m_questions[_index];

        public Dialogue GetRandomQuestion()
            => GetQuestion(Random.Range(0, m_questions.Length));
    }
}

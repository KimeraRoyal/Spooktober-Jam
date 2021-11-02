using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
namespace Spooktober.Dialogue.Types
{
    public class QuestionLines
    {
        private DialogueLine[] m_questions;

        public QuestionLines(IReadOnlyList<SerializedDialogueFile.SerializedText> _serializedTexts)
        {
            m_questions = new DialogueLine[_serializedTexts.Count];

            for (var i = 0; i < _serializedTexts.Count; i++)
            {
                var serializedText = _serializedTexts[i];
                m_questions[i] = new DialogueLine(serializedText.Type, serializedText.Text);
            }
        }

        public DialogueLine GetQuestion(int _index)
            => m_questions[_index];

        public DialogueLine GetRandomQuestion()
            => GetQuestion(Random.Range(0, m_questions.Length));
    }
}

using System;
namespace Spooktober.Dialogue.Types
{
    [Serializable]
    public class Answer : Dialogue
    {
        private int m_value;

        public Answer(string _type, string _text, int _value) : base(_type, _text)
        {
            m_value = _value;
        }
    }
    
    [Serializable]
    public class Answers
    {
        private Answer[] m_answers;
        private string m_type;

        public string Type => m_type;

        public Answers(SerializedDialogueFile.SerializedText[] _serializedTexts, string _type)
        {
            m_answers = new Answer[_serializedTexts.Length];
            m_type = _type;

            for (var i = 0; i < _serializedTexts.Length; i++)
            {
                var serializedText = _serializedTexts[i];
                m_answers[i] = new Answer(_type, serializedText.Text, serializedText.Value);
            }
        }
    }
}

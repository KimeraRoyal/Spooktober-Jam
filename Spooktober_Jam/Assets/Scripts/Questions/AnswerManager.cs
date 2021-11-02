using System.Collections;
using DG.Tweening;
using Spooktober.Character;
using Spooktober.Character.People;
using Spooktober.Dialogue;
using Spooktober.UI;
using UnityEngine;
using UnityEngine.Events;
namespace Spooktober.Questions
{
    public class AnswerManager : MonoBehaviour
    {
        public DialogueBox m_peopleDialogueBox;
        private TweenableUIElement m_dialogueTween;
        
        public UnityEvent m_answersFinished;
        
        public Color m_multiplyInactiveSpeaker;

        private void Awake()
        {
            m_dialogueTween = m_peopleDialogueBox.GetComponent<TweenableUIElement>();
        }

        public void ShowAnswers(Stat _stat, Dialogue.Types.DialogueLine dialogueLine)
        {
            StartCoroutine(ShowAnswersCoroutine(_stat, dialogueLine));
        }

        private IEnumerator ShowAnswersCoroutine(Stat _stat, Dialogue.Types.DialogueLine dialogueLine)
        {
            var people = FindObjectsOfType<Person>();
            m_dialogueTween.SetEnabled(true);
            for (var i = 0; i < people.Length; i++)
            {
                var person = people[i];
                for (var j = 0; j < people.Length; j++)
                {
                    people[j].inDialogue = true;
                    var multiplyChildColours = people[j].m_multiplyChildColours;
                    var endColour = people[j].CurrentColor;
                    if (i != j) endColour *= m_multiplyInactiveSpeaker;
                    DOTween.To(() => multiplyChildColours.MultiplyColour, value => multiplyChildColours.MultiplyColour = value, endColour, 0.4f);
                }
                person.transform.DOJump(person.transform.position, 0.5f, 3, 1f);

                if(m_peopleDialogueBox.AudioSource) { m_peopleDialogueBox.AudioSource.pitch = person.VoicePitch; }
                m_peopleDialogueBox.WriteText(new[]{person.GetAnswer(dialogueLine, _stat)}, -1);
                yield return new WaitUntil(() => !m_peopleDialogueBox.Active);
            }
            foreach (var person in people)
            {
                person.inDialogue = false;
                var multiplyChildColours = person.m_multiplyChildColours;
                DOTween.To(() => multiplyChildColours.MultiplyColour, value => multiplyChildColours.MultiplyColour = value, person.CurrentColor, 0.4f);
            }
            m_dialogueTween.SetEnabled(false);
            m_answersFinished?.Invoke();
        }
    }
}

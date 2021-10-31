using System;
using System.Collections;
using DG.Tweening;
using Spooktober.Character;
using Spooktober.Character.People;
using Spooktober.Dialogue;
using Spooktober.UI;
using UnityEngine;
using UnityEngine.Events;
namespace Spooktober
{
    public class AnswerManager : MonoBehaviour
    {
        public DialogueBox m_peopleDialogueBox;
        private TweenableUIElement m_dialogueTween;
        
        public UnityEvent m_answersFinished;

        private void Awake()
        {
            m_dialogueTween = m_peopleDialogueBox.GetComponent<TweenableUIElement>();
        }

        public void ShowAnswers(Stat _stat, Dialogue.Types.Dialogue _dialogue)
        {
            StartCoroutine(ShowAnswersCoroutine(_stat, _dialogue));
        }

        private IEnumerator ShowAnswersCoroutine(Stat _stat, Dialogue.Types.Dialogue _dialogue)
        {
            var people = FindObjectsOfType<Person>();
            m_dialogueTween.SetEnabled(true);
            foreach (var person in people)
            {
                person.transform.DOJump(person.transform.position, 0.5f, 3, 1f);
                m_peopleDialogueBox.WriteText(new[]{person.GetAnswer(_dialogue, _stat)}, -1);
                yield return new WaitUntil(() => !m_peopleDialogueBox.Active);
            }
            m_dialogueTween.SetEnabled(false);
            m_answersFinished?.Invoke();
        }
    }
}

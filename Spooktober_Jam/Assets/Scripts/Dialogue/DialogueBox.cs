using System;
using System.Collections;
using Spooktober.Character;
using TMPro;
using UnityEngine;

namespace Spooktober.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        private DialogueManager m_dialogueManager;
        
        private TMP_Text m_textMeshProText;
    
        /// <summary>
        /// The default delay between characters when writing text to the dialogue box.
        /// </summary>
        [SerializeField] private float m_dialogueDelay = 0.08f;
    
        /// <summary>
        /// The multiplier for characters that shouldn't be skipped.
        /// </summary>
        [SerializeField] private float m_dialogueMultiplier = 1.0f;
    
        /// <summary>
        /// The multiplier for characters that should be "skipped".
        /// </summary>
        [SerializeField] private float m_skipMultiplier = 0.2f;

        /// <summary>
        /// The sound played per-word when writing dialogue.
        /// </summary>
        private string m_dialogueSound;

        private bool m_active;

        private void Awake()
        {
            m_dialogueManager = FindObjectOfType<DialogueManager>();
            
            m_textMeshProText = GetComponentInChildren<TMP_Text>();
        }

        public void WriteText(string _dialogue, bool _instant = false)
            => StartCoroutine(WriteTextCoroutine(_dialogue, _instant));

        private IEnumerator WriteTextCoroutine(string _dialogue, bool _instant)
        {
            m_active = true;
            
            if (!_instant && m_dialogueDelay > 0)
            {
                yield return StartCoroutine(TypewriteDialogue(_dialogue, m_skipMultiplier, m_dialogueMultiplier));
            }
            else
            {
                m_textMeshProText.text = _dialogue;
            }

            m_active = false;
        }
        
        private IEnumerator TypewriteDialogue(string _dialogue, float _letterMultiplier, float _skipMultiplier)
        {
            var charIsLetter = false;
            var hasSfx = m_dialogueSound != "";
            
            for (var i = 0; i < _dialogue.Length; i++)
            {
                var nextCharIndex = i + 1;
                if (nextCharIndex < _dialogue.Length)
                {
                    var visibleText = _dialogue.Substring(0, nextCharIndex);
                    var hiddenText = _dialogue.Substring(nextCharIndex);
                        
                    m_textMeshProText.text = visibleText + "<alpha=#00>" + hiddenText;

                    var charWasLetter = charIsLetter;
                    charIsLetter = char.IsLetter(_dialogue[i]);

                    var delayMultiplier = charIsLetter ? _letterMultiplier : _skipMultiplier;
                    var delay = m_dialogueDelay * delayMultiplier;
                    
                    //TODO: Audio
                    //if(hasSfx && charIsLetter && !charWasLetter) m_audioManager.PlayOneShot(m_dialogueSound);
                    if (delay <= 0) continue;
                    yield return new WaitForSeconds(delay);
                }
                else
                {
                    m_textMeshProText.text = _dialogue;
                    break;
                }
            }
        }
    }
}

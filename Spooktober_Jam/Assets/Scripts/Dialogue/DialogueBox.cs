using System;
using System.Collections;
using Spooktober.Character;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spooktober.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        private DialogueManager m_dialogueManager;
        
        private TMP_Text m_textMeshProText;

        private AudioSource m_audioSource;
    
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

        [SerializeField] private float m_minDialoguePitchVariance, m_maxDialoguePitchVariance;

        private bool m_active;

        public static bool m_activeSupreme;

        public AudioSource AudioSource => m_audioSource;

        public bool Active => m_active;

        private void Awake()
        {
            m_dialogueManager = FindObjectOfType<DialogueManager>();
            
            m_textMeshProText = GetComponentInChildren<TMP_Text>();
            m_audioSource = GetComponent<AudioSource>();
        }

        public void WriteText(string[] _dialogues, float _delay = 0.0f, Action _onFinished = null)
            => StartCoroutine(WriteTextCoroutine(_dialogues, _delay, _onFinished));
        
        public void WriteText(string _dialogue, bool _instant = false)
            => StartCoroutine(WriteTextCoroutine(_dialogue, _instant));

        private IEnumerator WriteTextCoroutine(string[] _dialogues, float _delay = 0.0f, Action _onFinished = null)
        {
            foreach (var dialogue in _dialogues)
            {
                yield return WriteTextCoroutine(dialogue, false);
                m_active = true;
                m_activeSupreme = true;
                if (_delay > 0.0f) yield return new WaitForSeconds(_delay);
                else yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
            }
            m_active = false;
            m_activeSupreme = false;
            _onFinished?.Invoke();
        }

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
            var hasSfx = m_audioSource != null && m_audioSource.clip != null;

            var dialoguePitch = 1.0f;
            if (hasSfx) { dialoguePitch = m_audioSource.pitch; }
            
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

                    if (hasSfx && charIsLetter && !charWasLetter)
                    {
                        m_audioSource.pitch = dialoguePitch + Random.Range(m_minDialoguePitchVariance, m_maxDialoguePitchVariance);
                        m_audioSource.Play();
                    }
                    if (delay <= 0) continue;
                    yield return new WaitForSeconds(delay);
                }
                else
                {
                    m_textMeshProText.text = _dialogue;
                    break;
                }
            }
            
            if (hasSfx) { m_audioSource.pitch = dialoguePitch; }
        }
    }
}

using Spooktober.Dialogue;
using UnityEngine;
namespace Spooktober.UI
{
    public class QuestionButton: TweenableUIElement
    {
        private QuestionButtonSpawner m_buttonSpawner;

        private DialogueBox m_dialogueBox;
        
        [SerializeField] private bool m_interactable;

        public DialogueBox DialogueBox => m_dialogueBox;

        public bool Interactable
        {
            get => m_interactable;
            set => m_interactable = value;
        }

        protected override void Awake()
        {
            base.Awake();
            
            m_buttonSpawner = FindObjectOfType<QuestionButtonSpawner>();

            m_dialogueBox = GetComponent<DialogueBox>();
        }

        public void Interact()
        {
            if (!m_interactable) { return; }
            m_buttonSpawner.PlayButtonSequence(false);
        }
    }
}

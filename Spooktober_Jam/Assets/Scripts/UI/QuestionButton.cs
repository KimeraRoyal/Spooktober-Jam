using Spooktober.Dialogue;
using UnityEngine;
namespace Spooktober.UI
{
    public class QuestionButton: TweenableUIElement
    {
        private QuestionManager m_questionManager;
        private QuestionButtonSpawner m_buttonSpawner;

        private DialogueBox m_dialogueBox;
        
        [SerializeField] private bool m_interactable;

        public int m_id;

        public DialogueBox DialogueBox => m_dialogueBox;

        public bool Interactable
        {
            get => m_interactable;
            set => m_interactable = value;
        }

        protected override void Awake()
        {
            base.Awake();
            
            m_questionManager = FindObjectOfType<QuestionManager>();
            m_buttonSpawner = FindObjectOfType<QuestionButtonSpawner>();

            m_dialogueBox = GetComponent<DialogueBox>();
        }

        public void Interact()
        {
            if (!m_interactable) { return; }
            m_questionManager.PickQuestion(m_id);
            m_buttonSpawner.PlayButtonSequence(false);
        }
    }
}

using System;
using Spooktober.Dialogue;
using Spooktober.Questions;
using Spooktober.UI;
using Spooktober.Visuals;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;
namespace Spooktober.Character.People
{
    public class Person : Character
    {
        public MultiplyChildColours m_multiplyChildColours;

        private SpriteShapeRenderer m_spotlightRenderer;
        
        private DialogueManager m_dialogueManager;
        private SacrificeCount m_sacrificeCount;

        public DialogueBox m_dialogueBox;

        [SerializeField] private Color m_enabledColor;
        [SerializeField] private Color m_disabledColor;

        [SerializeField] private float m_minVoicePitch, m_maxVoicePitch;

        private Vector3 m_startPosition;
        private bool m_hovered;
        private bool m_enabled = true;

        private float m_voicePitch;

        public bool Enabled => m_enabled;

        public Color CurrentColor => m_enabled? m_enabledColor : m_disabledColor;

        public float VoicePitch => m_voicePitch;

        protected override void Awake()
        {
            base.Awake();

            m_dialogueManager = FindObjectOfType<DialogueManager>();
            m_sacrificeCount = FindObjectOfType<SacrificeCount>();

            m_multiplyChildColours = GetComponent<MultiplyChildColours>();
            m_spotlightRenderer = GetComponentInChildren<SpriteShapeRenderer>();

            m_voicePitch = Random.Range(m_minVoicePitch, m_maxVoicePitch);
        }

        public bool inDialogue;

        private void Start()
        {
            m_startPosition = transform.position;
            m_sacrificeCount.AdjustCounter(1);
        }

        public string GetAnswer(Dialogue.Types.DialogueLine _question, Stat _stat)
            => m_dialogueManager.GetAnswer(_stat, _question.Type, CharacterStats).Text;

        private Vector3 m_velocity;
        
        private void OnMouseEnter()
        {
            m_hovered = true;
        }

        private void OnMouseExit()
        {
            m_hovered = false;
        }

        public bool illuminated = false;
        private void Update()
        {
            if(!inDialogue && illuminated && !QuestionManager.m_selectingQuestion) m_multiplyChildColours.MultiplyColour = CurrentColor;
            var targetPosition = !DialogueBox.m_activeSupreme && !QuestionManager.m_selectingQuestion && !inDialogue && m_hovered ? m_startPosition + Vector3.up * 0.5f : m_startPosition;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, 0.3f);
        }

        private void OnMouseDown()
        {
            if (DialogueBox.m_activeSupreme || inDialogue || !illuminated || QuestionManager.m_selectingQuestion) { return; }
            m_enabled = !m_enabled;
            m_spotlightRenderer.enabled = m_enabled;
            m_sacrificeCount.AdjustCounter(m_enabled ? 1 : -1);
        }
    }
}

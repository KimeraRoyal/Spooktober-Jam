using TMPro;
using UnityEngine;
namespace Spooktober.UI
{
    public class SacrificeCount : MonoBehaviour
    {
        private TMP_Text m_textMeshProText;

        [SerializeField] private string m_counterText;
        [SerializeField] private string m_readyText;
        [SerializeField] private string m_noLightsText;

        [SerializeField] private float m_minSpotlightPitch, m_maxSpotlightPitch;
        [SerializeField] private AudioSource m_spotlightOnSfx, m_spotlightOffSfx;

        private int m_lightsOff;

        public int LightsOff => m_lightsOff;

        private void Awake()
        {
            m_textMeshProText = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            UpdateCounter();
        }

        public void AdjustCounter(int _amount, bool _playSound = true)
        {
            m_lightsOff += _amount;
            UpdateCounter();

            if (!_playSound) { return; }
            var spotlightSfx = _amount > 0 ? m_spotlightOnSfx : m_spotlightOffSfx;
            spotlightSfx.pitch = Random.Range(m_minSpotlightPitch, m_maxSpotlightPitch);
            spotlightSfx.Play();
        }

        public void UpdateCounter()
        {
            m_textMeshProText.text = m_lightsOff > 1 ? string.Format(m_counterText, m_lightsOff - 1) : m_lightsOff < 1 ? m_noLightsText : m_readyText;
        }
    }
}

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

        public void AdjustCounter(int _amount)
        {
            m_lightsOff += _amount;
            UpdateCounter();
        }

        public void UpdateCounter()
        {
            m_textMeshProText.text = m_lightsOff > 1 ? string.Format(m_counterText, m_lightsOff - 1) : m_lightsOff < 1 ? m_noLightsText : m_readyText;
        }
    }
}

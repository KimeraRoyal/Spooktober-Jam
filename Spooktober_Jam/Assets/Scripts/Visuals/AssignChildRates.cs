using UnityEngine;
namespace Spooktober.Visuals
{
    public class AssignChildRates : MonoBehaviour
    {
        private IHasAdjustableRate[] m_children;

        [SerializeField] private float m_minRate, m_maxRate;
        [SerializeField] private float m_minDeviation, m_maxDeviation;

        private void Awake()
        {
            m_children = GetComponentsInChildren<IHasAdjustableRate>();

            var rate = Random.Range(m_minRate, m_maxRate);
            foreach (var child in m_children)
            {
                var rateDeviation = Random.Range(m_minDeviation, m_maxDeviation);
                child.SetRate(rate + rateDeviation);
            }
        }
    }
}

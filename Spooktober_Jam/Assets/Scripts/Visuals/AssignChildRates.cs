using UnityEngine;
namespace Spooktober.Visuals
{
    public class AssignChildRates : MonoBehaviour
    {
        private IHasAdjustableRate[] m_children;

        [SerializeField] private float m_minRate, m_maxRate;

        private void Awake()
        {
            m_children = GetComponentsInChildren<IHasAdjustableRate>();

            var rate = Random.Range(m_minRate, m_maxRate);
            foreach(var child in m_children)
                child.SetRate(rate);
        }
    }
}

using UnityEngine;
namespace Spooktober
{
    public class AssignChildSeeds : MonoBehaviour
    {
        private IIsSeeded[] m_children;

        [SerializeField] private float m_deviation;

        private void Awake()
        {
            m_children = GetComponentsInChildren<IIsSeeded>();

            var seed = Random.Range(0.0f, Mathf.PI * 2);
            foreach (var child in m_children)
            {
                var seedDeviation = Random.Range(0, m_deviation);
                child.SetSeed(seed + seedDeviation);
            }
        }
    }
}

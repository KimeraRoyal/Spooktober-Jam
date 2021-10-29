using UnityEngine;
namespace Spooktober
{
    public class AssignChildSeeds : MonoBehaviour
    {
        private IIsSeeded[] m_children;

        private void Awake()
        {
            m_children = GetComponentsInChildren<IIsSeeded>();

            var seed = Random.Range(0.0f, Mathf.PI * 2);
            foreach(var child in m_children)
                child.SetSeed(seed);
        }
    }
}

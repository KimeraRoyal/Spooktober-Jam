using UnityEngine;
namespace Spooktober.Visuals
{
    public class RandomiseChildColours : MonoBehaviour
    {
        private SpriteRenderer[] m_spriteRenderers;

        [SerializeField] private Gradient m_gradient;

        private void Awake()
        {
            m_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            var colour = m_gradient.Evaluate(Random.Range(0.0f, 1.0f));
            foreach (var spriteRenderer in m_spriteRenderers)
            {
                spriteRenderer.color = colour;
            }
        }
    }
}

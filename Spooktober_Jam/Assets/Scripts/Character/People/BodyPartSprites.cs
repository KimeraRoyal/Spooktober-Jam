using UnityEngine;
namespace Spooktober.Character.People
{
    [CreateAssetMenu(fileName = "New Body Part Sprites", menuName = "Spooktober/Body Part Sprites")]
    public class BodyPartSprites : ScriptableObject
    {
        [SerializeField] private Sprite[] m_sprites;
        
        public Sprite GetRandomPartSprite()
            => m_sprites[Random.Range(0, m_sprites.Length)];
    }
}

using UnityEngine;

namespace Spooktober.Character.People
{
    public class PersonMaker : MonoBehaviour
    {
        [SerializeField] private GameObject m_personPrefab;

        [SerializeField] private BodyPartSprites[] m_bodyPartSprites;

        public Sprite GetBodySprite(int _index)
            => m_bodyPartSprites[_index].GetRandomPartSprite();

        public Sprite GetBodySprite(BodyPart _bodyPart)
            => GetBodySprite((int)_bodyPart);
    }
}

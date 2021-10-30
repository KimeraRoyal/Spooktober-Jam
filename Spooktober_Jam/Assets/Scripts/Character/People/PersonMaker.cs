using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spooktober.Character
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

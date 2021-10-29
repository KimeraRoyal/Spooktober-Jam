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

        [SerializeField] private Sprite[] m_bodySprites, m_headSprites, m_eyeSprites, m_noseSprites, m_mouthSprites, m_hairSprites;

        private void Start()
        {
            for (var x = 0; x < 7; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var person = CreatePerson();
                    person.transform.position = new Vector3(x * 2 - 6f, y * 2 - 1.5f, 0);
                }
            }
        }

        public Person CreatePerson()
        {
            var personObject = Instantiate(m_personPrefab);
            var person = personObject.GetComponent<Person>();

            if (person == null)
            {
                Destroy(personObject);
                return null;
            }

            person.Body = GetRandomPartSprite(m_bodySprites);
            person.Head = GetRandomPartSprite(m_headSprites);
            person.Eyes = GetRandomPartSprite(m_eyeSprites);
            person.Nose = GetRandomPartSprite(m_noseSprites);
            person.Mouth = GetRandomPartSprite(m_mouthSprites);
            person.Hair = GetRandomPartSprite(m_hairSprites);

            return person;
        }

        private static Sprite GetRandomPartSprite(IReadOnlyList<Sprite> _sprites)
            => _sprites[Random.Range(0, _sprites.Count)];
    }
}

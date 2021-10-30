using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober
{
    public class GameManager : MonoBehaviour
    {
        public GameObject personPrefab;
        public Character.MonsterScript monster;
        GameObject[] people;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }

        private void Reset()
        {
            monster.InitiateMonster();

            people = GameObject.FindGameObjectsWithTag("Player");

            foreach(GameObject e in people)
            {
                Vector3 pos = e.transform.position;
                Vector3 sca = e.transform.localScale;
                var pers = Instantiate(personPrefab);
                pers.transform.position = pos;
                pers.transform.localScale = sca;
                Destroy(e);
            }
        }
    }
}

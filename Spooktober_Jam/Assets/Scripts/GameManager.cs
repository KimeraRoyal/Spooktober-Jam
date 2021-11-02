using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static int highScore = 0;
        public static int totalScore = 0;
        
        public GameObject personPrefab;
        public MonsterScript monster;
        GameObject[] people;

        public static bool monster0seen = false;
        public static bool monster1seen = false;
        public static bool monster2seen = false;

        private void Awake()
        {
            if (_instance) Destroy(gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /*void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }*/

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

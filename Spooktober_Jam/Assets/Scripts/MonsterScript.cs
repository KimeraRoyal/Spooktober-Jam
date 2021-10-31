using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Spooktober.Dialogue;
using Spooktober.UI;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Spooktober.Character
{
    public class MonsterScript : MonoBehaviour
    {
        private DialogueManager m_dialogueManager;
        
        public float randomness = 5;
        public GameObject monst;

        public int monsterIndex = 0;
        public GameObject monster0;
        public GameObject monster1;
        public GameObject monster2;

        public string monsterName;

        private GameObject[] people;
        public int cool;
        public int cute;
        public int serious;
        public int childish;
        public int cynical;
        public int father;

        public Stat loves;
        public Stat likes;
        public Stat hates;
        public Stat special;
        public bool hasSpecial;
        public bool mode;

        private bool monster0seen = false;
        private bool monster1seen = false;
        private bool monster2seen = false;

        public UnityEvent m_introBegin;
        public UnityEvent m_introOver;

        public MonsterWants introDialogue;
        public MonsterWants wantDialogue;

        public CharacterStats CharacterStats
        {
            get;
            private set;
        }

        private void Awake()
        {
            m_dialogueManager = FindObjectOfType<DialogueManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            monst = null;
            InitiateMonster();
        }

        public void InitiateMonster()
        {
            monsterIndex = Mathf.RoundToInt(Random.Range(-0.4f, 1.9f));

            if (monst != null) Destroy(monst);
            switch (monsterIndex)
            {
                case 0:
                    monst = Instantiate(monster0, transform);
                    monsterName = "gnome";
                    break;
                case 1:
                    monst = Instantiate(monster1, transform);
                    monsterName = "beholder";
                    break;
                case 2:
                    monst = Instantiate(monster2, transform);
                    monsterName = "entity";
                    break;
            }

            people = GameObject.FindGameObjectsWithTag("Player");
            CharacterStats = people[Random.Range(0,people.Length)].GetComponent<Character>().CharacterStats;

            cool = Mathf.RoundToInt(CharacterStats.GetStat(Stat.Cool) + Random.Range(randomness* -1.0f, randomness));
            cute = Mathf.RoundToInt(CharacterStats.GetStat(Stat.Cute) + Random.Range(randomness * -1.0f, randomness));
            serious = Mathf.RoundToInt(CharacterStats.GetStat(Stat.Serious) + Random.Range(randomness * -1.0f, randomness));
            childish = Mathf.RoundToInt(CharacterStats.GetStat(Stat.Childish) + Random.Range(randomness * -1.0f, randomness));
            cynical = Mathf.RoundToInt(CharacterStats.GetStat(Stat.Cynical) + Random.Range(randomness * -1.0f, randomness));
            father = Mathf.RoundToInt(CharacterStats.GetStat(Stat.Father) + Random.Range(randomness * -1.0f, randomness));

            mode = Random.Range(0, 2) == 1;
            SetPreference();

            hasSpecial = monsterIndex < 1;
            if (hasSpecial)
            {
                special = loves;
                while (special == loves || special == hates || special == likes)
                {
                    var specialIndex = Random.Range(0, 6);
                    special = specialIndex switch
                    {
                        0 => Stat.Cool,
                        1 => Stat.Cute,
                        2 => Stat.Serious,
                        3 => Stat.Childish,
                        4 => Stat.Cynical,
                        5 => Stat.Father,
                        _ => special
                    };
                }
            }

            var monster = monst.GetComponent<Monster.Monster>();
            
            var monsterSeen = false;
            if (!monster0seen && monsterIndex == 0)
            {
                monster0seen = true;
            }
            else if (!monster1seen && monsterIndex == 1)
            {
                monster1seen = true;
            }
            else if (!monster2seen && monsterIndex == 2)
            {
                monster2seen = true;
            }
            else
            {
                monsterSeen = true;
            }
            
            if (!monsterSeen)
            {
                m_introBegin?.Invoke();
                introDialogue.UpdateText("intro_" + monsterName, monster.IntroDialogueLines, IntroOver);
            }
        }

        private void IntroOver()
        {
            m_introOver?.Invoke();
            wantDialogue.UpdateText("type_" + (mode ? "b" : "a") + "_" + monsterName + Random.Range(0, 2));
        }

        void SetPreference()
        {
            var myArray = new int[] { cool,cute,serious,childish,cynical,father };
            var largest = int.MinValue;
            var second = int.MinValue;
            foreach (var i in myArray)
            {
                if (i > largest)
                {
                    second = largest;
                    largest = i;
                }
                else if (i > second)
                    second = i;
            }
            
            var smallest = Mathf.Min(cool,cute,serious,childish,cynical,father);
            loves = CompareStats(largest);
            likes = CompareStats(second);
            hates = CompareStats(smallest);
        }

        private Stat CompareStats(int _value)
        {
            if (_value == cool) return Stat.Cool;
            if (_value == cute) return Stat.Cute;
            if (_value == serious) return Stat.Serious;
            if (_value == childish) return Stat.Childish;
            if (_value == cynical) return Stat.Cynical;
            if (_value == father) return Stat.Father;
            return Stat.Cool;
        }
    }
}

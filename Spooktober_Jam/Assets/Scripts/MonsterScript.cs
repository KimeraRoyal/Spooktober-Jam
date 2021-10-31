using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober.Character
{
    public class MonsterScript : MonoBehaviour
    {
        public float randomness = 5;
        public GameObject monst;

        public float monsterIndex = 0;
        public GameObject monster0;
        public GameObject monster1;
        public GameObject monster2;

        private GameObject[] people;
        public float cool;
        public float cute;
        public float serious;
        public float childish;
        public float cynical;
        public float father;

        public string loves;
        public string likes;
        public string hates;
        public string special;
        private string pref;

        private bool monster0seen = false;
        private bool monster1seen = false;
        private bool monster2seen = false;

        public CharacterStats CharacterStats
        {
            get;
            private set;
        }

        // Start is called before the first frame update
        void Start()
        {
            monst = null;
            InitiateMonster();
        }

        public void InitiateMonster()
        {
            monsterIndex = Mathf.Round(Random.Range(-0.4f, 1.8f));

            if (monst != null) Destroy(monst);
            if (monsterIndex == 0)
            {
                monst = Instantiate(monster0, transform);
            }
            else if (monsterIndex == 1)
            {
                monst = Instantiate(monster1, transform);
            }
            else if (monsterIndex == 2)
            {
                monst = Instantiate(monster2, transform);
            }

            people = GameObject.FindGameObjectsWithTag("Player");
            CharacterStats = people[Random.Range(0,people.Length)].GetComponent<Character>().CharacterStats;

            cool = Mathf.Round(CharacterStats.GetStat(Stat.Cool) + Random.Range(randomness*-1,randomness));
            cute = Mathf.Round(CharacterStats.GetStat(Stat.Cute) + Random.Range(randomness * -1, randomness));
            serious = Mathf.Round(CharacterStats.GetStat(Stat.Serious) + Random.Range(randomness * -1, randomness));
            childish = Mathf.Round(CharacterStats.GetStat(Stat.Childish) + Random.Range(randomness * -1, randomness));
            cynical = Mathf.Round(CharacterStats.GetStat(Stat.Cynical) + Random.Range(randomness * -1, randomness));
            father = Mathf.Round(CharacterStats.GetStat(Stat.Father) + Random.Range(randomness * -1, randomness));

            SetPreference();

            if (monsterIndex != 2)
            {
                special = loves;
                while(special == loves || special == hates || special == likes)
                {
                    var tity = Mathf.Round(Random.Range(0,6));
                    if (tity == 1) special = "cute";
                    if (tity == 2) special = "cool";
                    if (tity == 3) special = "serious";
                    if (tity == 4) special = "childish";
                    if (tity == 5) special = "cynical";
                    if (tity == 6) special = "father";
                }
            }

            ///IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!IMPORTANT!
            ///PUT THE MONSTER DIALOG INTRODUCTIONS HERE
            ///
            if (!monster0seen && monsterIndex == 0)
            {
                monster0seen = true;
            } else if (!monster1seen && monsterIndex == 1)
            {
                monster1seen = true;
            } else if (!monster2seen && monsterIndex == 2)
            {
                monster2seen = true;
            }
        }

        void SetPreference()
        {
            float[] myArray = new float[] { cool,cute,serious,childish,cynical,father };
            float largest = float.MinValue;
            float second = float.MinValue;
            foreach (float i in myArray)
            {
                if (i > largest)
                {
                    second = largest;
                    largest = i;
                }
                else if (i > second)
                    second = i;
            }

            if (largest == cool) pref = "cool";
            if (largest == cute) pref = "cute";
            if (largest == serious) pref = "serious";
            if (largest == childish) pref = "childish";
            if (largest == cynical) pref = "cynical";
            if (largest == father) pref = "father";
            loves = pref;

            if (second == cool) pref = "cool";
            if (second == cute) pref = "cute";
            if (second == serious) pref = "serious";
            if (second == childish) pref = "childish";
            if (second == cynical) pref = "cynical";
            if (second == father) pref = "father";
            likes = pref;

            var smallest = Mathf.Min(cool,cute,serious,childish,cynical,father);

            if (smallest == cool) pref = "cool";
            if (smallest == cute) pref = "cute";
            if (smallest == serious) pref = "serious";
            if (smallest == childish) pref = "childish";
            if (smallest == cynical) pref = "cynical";
            if (smallest == father) pref = "father";
            hates = pref;
        }
    }
}

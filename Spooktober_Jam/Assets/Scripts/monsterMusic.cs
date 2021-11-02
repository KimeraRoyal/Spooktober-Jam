using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober.Character
{
    public class monsterMusic : MonoBehaviour
    {
        float mon = 0;
        public AudioClip audioc;
        public AudioClip audioe;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<MonsterScript>().monsterIndex == 2 && mon != 2)
            {
                GameObject.FindObjectOfType<Camera>().gameObject.GetComponent<AudioSource>().clip = audioc;
                GameObject.FindObjectOfType<Camera>().gameObject.GetComponent<AudioSource>().Play();
            } else if (GetComponent<MonsterScript>().monsterIndex != 2 && mon == 2)
            {
                GameObject.FindObjectOfType<Camera>().gameObject.GetComponent<AudioSource>().clip = audioe;
                GameObject.FindObjectOfType<Camera>().gameObject.GetComponent<AudioSource>().Play();
            }

            mon = GetComponent<MonsterScript>().monsterIndex;
        }
    }
}

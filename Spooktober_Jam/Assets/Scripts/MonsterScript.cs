using DG.Tweening;
using Spooktober.Character;
using Spooktober.Character.People;
using Spooktober.Dialogue;
using Spooktober.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Spooktober
{
    public class MonsterScript : MonoBehaviour
    {
        private DialogueManager m_dialogueManager;

        private SaveManager m_saveManager;

        private ScreenStatic m_screenStatic;
        
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

        public UnityEvent m_introBegin;
        public UnityEvent m_preIntroBegin;
        public UnityEvent m_introOver;
        public UnityEvent m_introSkip;

        public MonsterWants introDialogue;
        public MonsterWants wantDialogue;

        private bool m_monsterSeen;
        private bool m_secondDialogue;

        public CharacterStats CharacterStats
        {
            get;
            private set;
        }

        private void Awake()
        {
            m_dialogueManager = FindObjectOfType<DialogueManager>();
            m_saveManager = FindObjectOfType<SaveManager>();
            m_screenStatic = FindObjectOfType<ScreenStatic>();
        }

        // Start is called before the first frame update
        void Start()
        {
            monst = null;
            InitiateMonster();
            
            var holdEscape = FindObjectOfType<HoldEscape>();

            var fadeSequence = DOTween.Sequence();
            fadeSequence.Append(DOTween.To(() => holdEscape.DarknessOffset, x => holdEscape.DarknessOffset = x, 0, m_monsterSeen ? 1.0f : 2.0f));
            
            if (monsterIndex == 2) { fadeSequence.Append(DOTween.To(() => m_screenStatic.Amount, value => m_screenStatic.Amount = value, 0.15f, 2.0f)); }
            else { m_screenStatic.Amount = 0.0f; }
            
            if (!m_monsterSeen) { fadeSequence.AppendCallback(InitiateGame); }
            else { InitiateGame(); }
            fadeSequence.Play();
        }

        [SerializeField] private Texture2D m_normalStaticSlope;
        [SerializeField] private Texture2D m_entityStaticSlope;

        public void InitiateMonster()
        {
            if (monst != null) Destroy(monst);
            
            monsterIndex = Mathf.RoundToInt(Random.Range(-0.4f, 1.8f));
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

            m_screenStatic.SlopeTexture = monsterIndex == 2 ? m_entityStaticSlope : m_normalStaticSlope;

            people = GameObject.FindGameObjectsWithTag("Player");
            CharacterStats = people[Random.Range(0,people.Length)].GetComponent<Character.Character>().CharacterStats;

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

            HasMonsterBeenSeen();
            if (!m_monsterSeen)
            {
                m_preIntroBegin?.Invoke();
            }
        }

        private void InitiateGame()
        {
            var monster = monst.GetComponent<Character.Monster.Monster>();
            if (!m_monsterSeen)
            {
                m_introBegin?.Invoke();
                var dialogueName = "intro_" + monsterName;
                if (m_secondDialogue) dialogueName += "2";
                introDialogue.UpdateText(dialogueName, monster.IntroDialogueLines, IntroOver);
            }
            else
            {
                m_introSkip?.Invoke();
                IntroOver();
            }
        }

        private void HasMonsterBeenSeen()
        {
            m_monsterSeen = false;
            m_secondDialogue = false;
            if (!GameManager.monster0seen && monsterIndex == 0)
            {
                GameManager.monster0seen = true;
            }
            else if (!GameManager.monster1seen && monsterIndex == 1)
            {
                GameManager.monster1seen = true;
            }
            else if ((!GameManager.monster2seen || !GameManager.monster2secondDialogueSeen) && monsterIndex == 2)
            {
                if (!GameManager.monster2seen)
                {
                    GameManager.monster2seen = true;
                }
                else if (!GameManager.monster2secondDialogueSeen)
                {
                    GameManager.monster2secondDialogueSeen = true;
                    m_secondDialogue = true;
                }
            }
            else
            {
                m_monsterSeen = true;
            }
        }
        
        private void IntroOver()
        {
            m_introOver?.Invoke();
            wantDialogue.UpdateText("type_" + (mode ? "b" : "a") + "_" + monsterName + Random.Range(0, 2));
            
            var people = FindObjectsOfType<Person>();
            foreach (var person in people)
            {
                var illuminateSequence = DOTween.Sequence();
                illuminateSequence.Append(DOTween.To(() => person.m_multiplyChildColours.MultiplyColour, value => person.m_multiplyChildColours.MultiplyColour = value, person.CurrentColor, 0.4f));
                illuminateSequence.AppendCallback(() => person.illuminated = true);
                illuminateSequence.Play();
            }
        }

        public UnityEvent m_sacrificeEvent;
        public UnityEvent m_nonEntitySacrificeEvent;

        public void Sacrifice()
        {
            var sacrificeCount = FindObjectOfType<SacrificeCount>();
            if (sacrificeCount.LightsOff != 1) return;

            var peoplePeople = FindObjectsOfType<Person>();
            Person sacrifice = null;
            foreach (var person in peoplePeople)
            {
                if (person.Enabled) sacrifice = person;
            }

            if (sacrifice == null) return;

            m_sacrificeEvent?.Invoke();

            var score = Mathf.RoundToInt(CalculateSacrificeScore(sacrifice));
            var winDialogue = m_dialogueManager.GetWinDialogue(monsterName, score);
            
            var calculatedScore = Mathf.Clamp(score / 2.0f, 0, 100);
            var savedScore = Mathf.RoundToInt(calculatedScore);
            if (savedScore > Mathf.FloorToInt(calculatedScore)) { savedScore++; }
            
            if (GameManager.highScore < savedScore) GameManager.highScore = savedScore;
            
            var isEntity = monsterName == "entity";
            var lostToEntity = isEntity && score < 40;

            if (lostToEntity && GameManager.lostToEntity < 1)
            {
                GameManager.lostToEntity = 1;
                m_saveManager.SaveEntityImage(0);
            }
            else if (isEntity || score >= 50)
            {
                GameManager.totalScore += savedScore;

                if (isEntity && GameManager.lostToEntity == 1)
                {
                    GameManager.lostToEntity = 2;
                    m_saveManager.SaveEntityImage(1);
                }
            }
            
            m_saveManager.SaveGame();
            
            introDialogue.DialogueBox.WriteText(new[]{winDialogue}, -1, () => OutroOver(lostToEntity));
            
            if (!isEntity)
            {
                m_nonEntitySacrificeEvent?.Invoke();
                (score >= 50 ? m_succeedMusic : m_failMusic).Play();
            }
        }

        public AudioSource m_succeedMusic;
        public AudioSource m_failMusic;
        
        public AudioSource m_entityMusic;
        public AudioSource m_static;

        private void OutroOver(bool _crash)
        {
            var fadeSequence = DOTween.Sequence();
            if (_crash)
            {
                HoldEscape.s_enabled = false;
                m_static.volume = 0.0f;

                m_screenStatic.SlopeTexture = m_normalStaticSlope;
                
                //m_entityMusic.Play();
                m_static.Play();
                
                fadeSequence.Append(m_static.DOFade(1.0f, 4.0f));
                fadeSequence.Insert(0, DOTween.To(() => m_screenStatic.Amount, value => m_screenStatic.Amount = value, 1.0f, 4.0f));
                fadeSequence.AppendCallback(() => Application.Quit(0));
            }
            else
            {
                var holdEscape = FindObjectOfType<HoldEscape>();

                fadeSequence.Append(DOTween.To(() => holdEscape.DarknessOffset, x => holdEscape.DarknessOffset = x, 1, 1.0f));
                if (monsterIndex == 2) { fadeSequence.Append(DOTween.To(() => m_screenStatic.Amount, value => m_screenStatic.Amount = value, 0.0f, 2.0f)); }
                fadeSequence.AppendCallback(() => SceneManager.LoadScene(0));
            }
            fadeSequence.Play();
        }

        private float CalculateSacrificeScore(Person _sacrifice)
        {
            var stats = _sacrifice.CharacterStats;
            
            float score = 0;
            if (!mode) score = stats.GetStat(loves) + stats.GetStat(likes) * 0.6f;
            else score = stats.GetStat(loves) - stats.GetStat(hates) * 0.75f;
            if (hasSpecial) score += stats.GetStat(special);

            score = Mathf.Clamp(score * 1.5f, 0, 100);

            return score;
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

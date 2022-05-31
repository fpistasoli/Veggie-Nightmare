using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Attributes;
using VeggieNightmare.Control;
using VeggieNightmare.SceneManagement;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int numberOfLevels;
        [SerializeField] private int attackReward = 5;
        [SerializeField] private GameObject player;

        public static GameManager sharedInstance;
        public static int score = 0;

        //public static int[] highScorePerLevel; //player prefs keys: highScore1, highScore2, highScore3
        private int currentLevel;
       
        public static event Action onNewHighScore;

        private void OnEnable()
        {
            EvilVeggie.onEvilVeggieDamageTaken += OnAwardPointsHandler;
            MirrorExit.onStageComplete += OnSaveStageHighScore;
        }

        private void OnDisable()
        {
            EvilVeggie.onEvilVeggieDamageTaken -= OnAwardPointsHandler;
            MirrorExit.onStageComplete -= OnSaveStageHighScore;
        }

        private void OnSaveStageHighScore()
        {
            //int currentHighScore = highScorePerLevel[currentLevel - 1];
            int currentHighScore = PlayerPrefs.GetInt("highScore" + currentLevel); 

            int bonusHPPoints = Mathf.RoundToInt(player.GetComponent<PlayerHealth>().GetHealthPoints());

            int totalScore = score + bonusHPPoints;

            if(totalScore > currentHighScore)
            {
                PlayerPrefs.SetInt("highScore" + currentLevel, totalScore);
                PlayerPrefs.Save();


                // highScorePerLevel[currentLevel - 1] = totalScore;
                onNewHighScore?.Invoke();
            }
        }

        private void OnAwardPointsHandler()
        {
            score += attackReward;

            //TODO (creo que se puede hacer con UnityEvent en lugar de Action y pasarle parametro)
            //5 * MILD/TOUGH (o sea 5 * 1 ó 5 * 2); por ahora hacer que me otorgue 5 puntos sea mild o tough

        }

        private void Awake()
        {
            if (sharedInstance == null)
            {
                sharedInstance = this; 
                DontDestroyOnLoad(gameObject);

                score = 0;
                currentLevel = 1;
                //highScorePerLevel = new int[numberOfLevels];

                RestoreHighScores();
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void RestoreHighScores()
        {
            for(int i=0; i<numberOfLevels; i++)
            {
                if (PlayerPrefs.HasKey("highScore" + i+1))
                {
                    //highScorePerLevel[i] = PlayerPrefs.GetInt("highScore" + (i+1).ToString());
                }
                else
                {
                    PlayerPrefs.SetInt("highScore" + i+1, 0);
                    PlayerPrefs.Save();
                    //highScorePerLevel[i] = 0;
                }
            }

        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            //player = GameObject.FindWithTag("Player"); 
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public int GetNumberOfLevels() => numberOfLevels;





    }

}
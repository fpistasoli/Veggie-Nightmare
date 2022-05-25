using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Control;
using VeggieNightmare.SceneManagement;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int numberOfLevels;
        [SerializeField] private int attackReward = 5;

        public static GameManager sharedInstance;
        public static int score = 0;
        public static int[] highScorePerLevel;
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
            int currentHighScore = highScorePerLevel[currentLevel - 1];

            if(score > currentHighScore)
            {
                highScorePerLevel[currentLevel - 1] = score;
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
                highScorePerLevel = new int[numberOfLevels];

                RestoreHighScores();



            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void RestoreHighScores() //TODO
        {
           
            // restore high scores from player prefs
            // load high scores in highScorePerLevel




        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public int GetNumberOfLevels() => numberOfLevels;





    }

}
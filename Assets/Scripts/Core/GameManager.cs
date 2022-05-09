using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Control;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int numberOfLevels;

        public static GameManager sharedInstance;
        public static int score = 0;
        public static int[] highScorePerLevel;
        private int currentLevel;

        private void OnEnable()
        {
            EvilVeggie.onEvilVeggieDeath += OnAwardPointsHandler;
        }

        private void OnDisable()
        {
            
        }

        private void OnAwardPointsHandler()
        {
            score += 5;

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
                highScorePerLevel = new int[numberOfLevels];
                RestoreHighScores();
            }
            else
            {
                Destroy(gameObject);
            }

        }

        private void RestoreHighScores()
        {
            //TODO: tomar los highscores de cada nivel de PlayerPrefs y guardarlos en highScoresPerLevel

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
            Debug.Log("SCORE: " + score);
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int numberOfLevels;

        public static GameManager sharedInstance;
        public static int score;
        public static int[] highScorePerLevel;
        private int currentLevel;

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

        }
    }

}
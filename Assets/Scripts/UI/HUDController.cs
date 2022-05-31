using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VeggieNightmare.Attributes;
using VeggieNightmare.Control;
using VeggieNightmare.Core;
using VeggieNightmare.SceneManagement;

namespace VeggieNightmare.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Button attackButton;
        [SerializeField] private Button jumpButton;
        [SerializeField] private GameObject player;
        [SerializeField] private Image healthBarInner;
        [SerializeField] private Text levelValue;
        [SerializeField] private Text scoreValue;
        [SerializeField] private Text highScoreValue;
        [SerializeField] private Button pauseButton;

        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button tryAgainButton;
        [SerializeField] private Button mainMenuButton;   

        [SerializeField] private GameObject stageCompletePanel;
        [SerializeField] private Text stageCompleteText;
        [SerializeField] private Text yourScoreText;
        [SerializeField] private Text yourScoreValue;
        [SerializeField] private Text bonusHPText;
        [SerializeField] private Text bonusHPValue;
        [SerializeField] private Text totalScoreText;
        [SerializeField] private Text totalScoreValue;
        [SerializeField] private Text newHighScoreText;

        private bool newHighScore;
        private int indexActiveScene;
        private bool isPaused = false;


        private void Start()
        {
            gameOverPanel.SetActive(false);
            stageCompletePanel.SetActive(false);
            levelValue.text = GameManager.sharedInstance.CurrentLevel.ToString();

            indexActiveScene = SceneManager.GetActiveScene().buildIndex;
            //highScoreValue.text = GameManager.highScorePerLevel[indexActiveScene - 1].ToString();
            highScoreValue.text = PlayerPrefs.GetInt("highScore" + GameManager.sharedInstance.CurrentLevel).ToString(); 

            newHighScore = false;

            OnHPUpdateUI();

        }

        private void OnEnable() //aca van las suscripciones a eventos
        {
            PlayerHealth.onDeath += OnGameOverUIHandler;
            PlayerHealth.onDeath += OnHPUpdateUI;
            PlayerHealth.onDeath += OnPlayerControlsDisabledUI;
            EvilVeggie.onEvilVeggieDamageTaken += OnScoreUpdateUI;
            MirrorExit.onStageComplete += OnStageCompleteUI;
            GameManager.onNewHighScore += OnNewHighScoreUI;
        }

        private void OnDisable() //aca van las desuscripciones a eventos
        {
            PlayerHealth.onDeath -= OnGameOverUIHandler;
            PlayerHealth.onDeath -= OnHPUpdateUI;
            PlayerHealth.onDeath -= OnPlayerControlsDisabledUI;
            EvilVeggie.onEvilVeggieDamageTaken -= OnScoreUpdateUI;
            MirrorExit.onStageComplete -= OnStageCompleteUI;
            GameManager.onNewHighScore -= OnNewHighScoreUI;

        }

        private void OnNewHighScoreUI()
        {
            newHighScore = true;
        }

        private void OnStageCompleteUI()
        {

            player.SetActive(false);
            player.GetComponent<Rigidbody>().useGravity = false;

            stageCompletePanel.SetActive(true);

            HideStatsTexts();

            ShowStats();
        }

        private void ShowStats()
        {
            StartCoroutine(ShowStatsCoroutine());
        }

        private IEnumerator ShowStatsCoroutine()
        {
            int score = GameManager.score;
            yourScoreValue.text = score.ToString();

            int bonusHPPoints = Mathf.RoundToInt(player.GetComponent<PlayerHealth>().GetHealthPoints());
            bonusHPValue.text = bonusHPPoints.ToString();

            int totalScore = score + bonusHPPoints;
            totalScoreValue.text = totalScore.ToString();
            //totalScoreValue.text = GameManager.totalScore.ToString();


            yield return new WaitForSeconds(1.5f);
            yourScoreText.gameObject.SetActive(true);
            yourScoreValue.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            bonusHPText.gameObject.SetActive(true);
            bonusHPValue.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            totalScoreText.gameObject.SetActive(true);
            totalScoreValue.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);

            if (newHighScore)
            {
                //PlayerPrefs.SetInt("highScore" + indexActiveScene.ToString(), totalScore);
                //GameManager.highScorePerLevel[indexActiveScene-1] = totalScore; //already done in GameManager

                StartCoroutine(FlashingTextEffect(newHighScoreText, 0.1f, 20));
            }

            yield return new WaitForSeconds(3f);

            GameManager.score = 0;

            //newHighScore = false;

            if (GameManager.sharedInstance.CurrentLevel < GameManager.sharedInstance.GetNumberOfLevels())
            {
                GameManager.sharedInstance.CurrentLevel++;
                SceneManager.LoadScene(GameManager.sharedInstance.CurrentLevel);
            }
            else
            {
                GameManager.sharedInstance.CurrentLevel = 1;
                SceneManager.LoadScene(5);
            }

        }

        private IEnumerator FlashingTextEffect(Text flashingText, float blinkTime, int totalBlinks)
        {
            for(int i=0; i<totalBlinks; i++)
            {
                flashingText.gameObject.SetActive(true);
                yield return new WaitForSeconds(blinkTime);
                flashingText.gameObject.SetActive(false);
                yield return new WaitForSeconds(blinkTime);
            }
        }

        private void HideStatsTexts()
        {
            yourScoreText.gameObject.SetActive(false);
            yourScoreValue.gameObject.SetActive(false);
            bonusHPText.gameObject.SetActive(false);
            bonusHPValue.gameObject.SetActive(false);
            totalScoreText.gameObject.SetActive(false);
            totalScoreValue.gameObject.SetActive(false);
            newHighScoreText.gameObject.SetActive(false);
        }

        private void OnPlayerControlsDisabledUI()
        {
            attackButton.enabled = false;
            jumpButton.enabled = false;
        }

        private void OnPlayerControlsEnabledUI()
        {
            attackButton.enabled = true;
            jumpButton.enabled = true;
        }

        private void OnScoreUpdateUI()
        {
            scoreValue.text = GameManager.score.ToString();
        }

        private void OnGameOverUIHandler() 
        {
            gameOverPanel.SetActive(true);
        }

        public void Jump()
        {
            player.GetComponent<PlayerController>().Jump();
        }

        public void Attack()
        {
            player.GetComponent<PlayerController>().Attack();
        }

        public void OnHPUpdateUI()
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            float healthPoints = playerHealth.GetHealthPoints();

            healthBarInner.fillAmount = healthPoints / 100f;

        }

        public void TryAgainButtonHandler()
        {
            Time.timeScale = 1;
            GameManager.score = 0;
            SceneManager.LoadScene(GameManager.sharedInstance.CurrentLevel);
        }

        public void BackToMenuButtonHandler()
        {
            Time.timeScale = 1;
            GameManager.sharedInstance.CurrentLevel = 1;
            GameManager.score = 0;
            SceneManager.LoadScene(0);
        }

        public void Pause()
        {
            if(isPaused)
            {
                isPaused = false;
                Time.timeScale = 1;
                OnPlayerControlsEnabledUI();
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0;
                OnPlayerControlsDisabledUI();
            }
        }


    }

}
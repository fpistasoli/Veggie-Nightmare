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
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button tryAgainButton;
        [SerializeField] private Button mainMenuButton;


        private void Start()
        {
            gameOverPanel.SetActive(false);
            levelValue.text = GameManager.sharedInstance.CurrentLevel.ToString();
        }

        private void OnEnable() //aca van las suscripciones a eventos
        {
            PlayerHealth.onDeath += OnGameOverUIHandler;
            PlayerHealth.onDeath += OnHPUpdateUI;
            PlayerHealth.onDeath += OnPlayerControlsDisabledUI;
            EvilVeggie.onEvilVeggieDamageTaken += OnScoreUpdateUI;
        }

        private void OnPlayerControlsDisabledUI()
        {
            attackButton.enabled = false;
            jumpButton.enabled = false;
        }

        private void OnDisable() //aca van las desuscripciones a eventos
        {
            PlayerHealth.onDeath -= OnGameOverUIHandler;
            PlayerHealth.onDeath -= OnHPUpdateUI;
            PlayerHealth.onDeath -= OnPlayerControlsDisabledUI;
            EvilVeggie.onEvilVeggieDamageTaken -= OnScoreUpdateUI;
           
        }
        private void OnScoreUpdateUI()
        {
            scoreValue.text = GameManager.score.ToString();
        }

        private void OnGameOverUIHandler() //TODO
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
            GameManager.score = 0;
            SceneManager.LoadScene(0);
        }


    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VeggieNightmare.Core;

namespace VeggieNightmare.UI
{
    public class OutroController : MonoBehaviour
    {

        [SerializeField] private Text congratsText;

        void Start()
        {
            HideTexts();
            StartCoroutine(ShowIntro());
        }

        private IEnumerator ShowIntro()
        {
            yield return new WaitForSeconds(1f);
            congratsText.gameObject.SetActive(true);
            yield return new WaitForSeconds(10f);

            AudioManager.sharedInstance.GetComponent<AudioSource>().Play();
            SceneManager.LoadScene(0);
        }

        private void HideTexts()
        {
            congratsText.gameObject.SetActive(false);
        }

        void Update()
        {

        }
    }
}

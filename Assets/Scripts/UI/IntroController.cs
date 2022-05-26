using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VeggieNightmare.UI
{
    public class IntroController : MonoBehaviour
    {

        [SerializeField] private Text text1;
        [SerializeField] private Text text2;
        [SerializeField] private Text text3;
        [SerializeField] private Text loadingText;


        void Start()
        {
            HideTexts();
            StartCoroutine(ShowIntro());
        }

        private void HideTexts()
        {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
            text3.gameObject.SetActive(false);
            loadingText.gameObject.SetActive(false);
        }

        private IEnumerator ShowIntro()
        {
            yield return new WaitForSeconds(1f);
            text1.gameObject.SetActive(true);
            yield return new WaitForSeconds(4f);
            text2.gameObject.SetActive(true);
            yield return new WaitForSeconds(4f);
            text3.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            loadingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(1);
        }
    }





}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            yield return new WaitForSeconds(6f);
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

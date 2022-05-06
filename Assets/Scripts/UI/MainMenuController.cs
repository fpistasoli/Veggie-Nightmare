using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VeggieNightmare.UI
{
    public class MainMenuController : MonoBehaviour
    {

        [SerializeField] private Button playButton;


        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Play()
        {
            SceneManager.LoadScene(1);
        }

    }

}

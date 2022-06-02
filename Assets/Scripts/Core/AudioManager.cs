using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VeggieNightmare.Core
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager sharedInstance;

        [SerializeField] private AudioClip[] audioClips;

        private AudioSource musicPlaying;
      

        private void Awake()
        {
            if (sharedInstance == null)
            {
                sharedInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }


        private void Start()
        {


            if(SceneManager.GetActiveScene().buildIndex == 0)
            {

                musicPlaying = GetComponent<AudioSource>();

                musicPlaying.clip = audioClips[0];

                musicPlaying.Play();


            }
            


        }














    }
}

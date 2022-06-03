using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VeggieNightmare.Core
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager sharedInstance;
        private AudioSource musicPlaying;

        [SerializeField] private AudioClip[] audioClips;

        
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

        public AudioClip GetClip(int i) => audioClips[i];

    }
}

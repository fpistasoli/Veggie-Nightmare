using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Stats
{
    public class EvilVeggieStats : MonoBehaviour
    {

        public enum Evilness { Mild, Tough};
        [SerializeField] private Vector3 healthPointsMild; // mushroom - tomato - carrot
        [SerializeField] private Vector3 damagePointsMild; // mushroom - tomato - carrot

        public static EvilVeggieStats sharedInstance = null;

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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector3 GetHealthPointsMild()
        {
            return healthPointsMild;
        }

        public Vector3 GetDamagePointsMild()
        {
            return damagePointsMild;
        }




    }
}

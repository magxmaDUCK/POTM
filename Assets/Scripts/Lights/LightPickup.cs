using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightPickup : MonoBehaviour
    {
        private bool on = true;
        private bool lightoff = false;
        private int scoreValue = 1;
        private float startTime = 0f;

        public float turnOffDuration = 1.0f;

        private float offIntensity = 0;

        public LightPickup()
        {
            on = true;
            scoreValue = 1;
        }
        public LightPickup(bool onoff, int score)
        {
            on = onoff;
            scoreValue = score;
        }

        private Light light;



        private void Start()
        {
            light = GetComponentInChildren<Light>();
        }

        void Update()
        {
            if (!on && !lightoff)
            {
                
                float t = Mathf.Max(0, 1 - (Time.time - startTime));
            
                float baseIntensity = offIntensity; //Replace this with whatever you want for your base color at emission level '1'

                float finalIntensity = baseIntensity * Mathf.LinearToGammaSpace(t);

                light.intensity = finalIntensity;
                
            }

            if(!on && lightoff)
            {
                Destroy(this);
            }
        }

        virtual public void TurnOff()
        {
           AkSoundEngine.PostEvent("Play_Light_Pickup", gameObject);
            on = false;
            startTime = Time.time;
        }

        public int getScoreValue()
        {
            return scoreValue;
        }

        public bool IsOn()
        {
            return on;
        }
    }
}

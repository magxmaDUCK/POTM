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

        void Update()
        {
            if (!on && lightoff)
            {
                Debug.Log("hi");
                Renderer renderer = GetComponent<Renderer>();
                Material mat = renderer.material;

                float t = Mathf.Clamp01(1 - Time.time - startTime);

                //float emission = Mathf.PingPong(Time.time, 1.0f);
            
                Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

                Color finalColor = baseColor * Mathf.LinearToGammaSpace(t);

                mat.SetColor("_EmissionColor", finalColor);
            }
        }

        virtual public void TurnOff()
        {
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

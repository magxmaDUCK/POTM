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

        private Color offColor = Color.yellow;

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

        private Renderer rend;

        void Update()
        {
            if (!on && !lightoff)
            {
                Renderer rend = GetComponent<Renderer>();
                offColor = rend.material.GetColor("_Color");
                Material mat = rend.material;

                float t = Mathf.Max(0, 1 - (Time.time - startTime));

                Debug.Log(t);
            
                Color baseColor = offColor; //Replace this with whatever you want for your base color at emission level '1'

                Color finalColor = baseColor * Mathf.LinearToGammaSpace(t);

                mat.SetColor("_EmissionColor", finalColor);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

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

        //param Random Melodies
        int melodieSelect = 1 ;
        private int RTPCtype = (int)AkQueryRTPCValue.RTPCValue_Global;

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

        private Material mat;

        private void Start()
        {
            //light = GetComponentInChildren<Light>();
            mat = GetComponent<Renderer>().material;
        }

        void Update()
        {
            if (!on && !lightoff)
            {
                
                float t = Mathf.Max(0, 1 - (Time.time - startTime));
            
                float baseIntensity = offIntensity; //Replace this with whatever you want for your base color at emission level '1'

                float finalIntensity = baseIntensity * Mathf.LinearToGammaSpace(t);

                //light.intensity = finalIntensity;

                mat.SetFloat("_DisolveLerp", t);
                //Color c = mat.GetColor("_EmissionColor");
                //mat.SetColor("_EmissionColor", c * t);

                if (t == 0)
                {
                    lightoff = true;
                }

                //GameObject go = Instantiate(PickUpFX, transform);
                //VisualEffect f = GetComponent<VisualEffect>();
                //f.SetVector3("", Player);
            }

            if(!on && lightoff)
            {
                Destroy(gameObject);
                Destroy(this);
            }
        }

        virtual public void TurnOff()
        {
           AkSoundEngine.GetRTPCValue("LightNumbers", gameObject, 0 , out float out_rValue, ref RTPCtype);
           if (out_rValue >= 16)
           {
               AkSoundEngine.SetRTPCValue("LightNumbers",0 , null);
               melodieSelect = Random.Range(1, 4);
           }
           AkSoundEngine.PostTrigger("PickupLight_1", null);
            //AkSoundEngine.PostEvent("Play_Melodie_0" + melodieSelect, gameObject);
            //AkSoundEngine.PostEvent("Play_Melodie_0" + melodieSelect, gameObject /*, (uint)AkCallbackType.AK_MusicSyncGrid, CallBackLight, null);
            on = false;
           startTime = Time.time;
        }
        /*private void CallBackLight(object in_cookie, AkCallbackType in_type, object in_info)
        {
            AkSoundEngine.PostEvent("Play_Melodie_0" + melodieSelect, gameObject);
        }*/

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

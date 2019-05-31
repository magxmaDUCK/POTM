using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class EmiisionManager : MonoBehaviour
    {
        public GameObject body;

        [HideInInspector]public List<float> pickupTimes = new List<float>();

        private Renderer rend;
        private Color baseCol;

        private int maxIntensity = 6;
        private int minIntensity = 1;

        private List<float> deleteTimes = new List<float>();
        // Start is called before the first frame update

        private float intensity = 0f;

        void Start()
        {
            rend = body.GetComponent<Renderer>();
            baseCol = rend.material.GetVector("_EmissionColor");
        }

        // Update is called once per frame
        void Update()
        {
            intensity = minIntensity;
            for(int i = 0; i < pickupTimes.Count; i++)
            {
                if(Time.time - pickupTimes[i] > 1f)
                {
                    deleteTimes.Add(pickupTimes[i]);
                }
                else
                {
                    intensity += 1f;
                }
            }

            foreach(float f in deleteTimes)
            {
                pickupTimes.Remove(f);
            }

            intensity = Mathf.Min(intensity, maxIntensity);
            Debug.Log(intensity);
            rend.material.SetVector("_EmissionColor", baseCol * intensity);
            //rend.material.EnableKeyword("_EMISSION");
        }
    }
}

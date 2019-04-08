using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doppler
{
    public class DopplerEmitter : MonoBehaviour
    {
        public static List<DopplerListener> listeners;

        //Speed of sound in air at 15°C
        private float celerity = 340f;

        public Vector3 position
        {
            get
            {
                return transform.position;
            }
        }
        
        private void Awake()
        {
            listeners = new List<DopplerListener>();
            listeners.Clear();
        }

        private void Update()
        {
            float dist;
            float speed;
            float ccv;
            foreach (DopplerListener listener in listeners)
            {
                dist = (position - listener.GetPosition()).magnitude - (position - listener.GetPrevPosition()).magnitude;
                speed = (-dist) / Time.deltaTime;

                ccv = celerity / (celerity - speed);

                Debug.Log(ccv + " : " + dist);

                AkSoundEngine.SetRTPCValue("Doppler", ccv);
            }
        }

        public static void AddListener(DopplerListener listener)
        {
            listeners.Add(listener);
        }
    }
}

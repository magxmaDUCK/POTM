using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doppler
{
    public class DopplerEmitter : MonoBehaviour
    {
        public static List<DopplerListener> listeners;

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
            foreach(DopplerListener listener in listeners)
            {
                Vector3 moved = position - listener.GetPrevPosition() + listener.GetPosition();
                float dist = moved.magnitude;
                float speed = dist / Time.deltaTime;

                //Set rtpc for player that contains current listener;
            }
        }

        public static void AddListener(DopplerListener listener)
        {
            listeners.Add(listener);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class EventManager : MonoBehaviour
    {
        private static bool m_ShuttingDown = false;
        private static object m_Lock = new object();
        private static EventManager m_Instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                if (m_ShuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(EventManager) +
                        "' already destroyed. Returning null.");
                    return null;
                }

                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        // Search for existing instance.
                        m_Instance = (EventManager)FindObjectOfType(typeof(EventManager));

                        // Create new instance if one doesn't already exist.
                        if (m_Instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            m_Instance = singletonObject.AddComponent<EventManager>();
                            singletonObject.name = typeof(EventManager).ToString() + " (Singleton)";

                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return m_Instance;
                }
            }
        }


        private void OnApplicationQuit()
        {
            m_ShuttingDown = true;
        }


        private void OnDestroy()
        {
            m_ShuttingDown = true;
        }

        protected EventManager() { }

        /////////////////////////////

        public float timeEvent1 = 0f;
        public float timeEvent2 = 150f;
        public float timeEvent3 = 300f;
        public float timeEvent4 = 450f;

        private bool event1Started = false;
        private bool event2Started = false;
        private bool event3Started = false;
        private bool event4Started = false;

        [Tooltip("The Parent Gameobject, all his children are the light pickups to be deactivated")]
        public GameObject QuartierMarchand;
        public GameObject QuartierRestaurant;
        public GameObject FeuDeCamp;
        public GameObject Firework;




        private void Start()
        {
            
        }

        private void Update()
        {
            if(Time.time > timeEvent1 && !event1Started)
            {
                Event1();
                event1Started = true;
            }

            if (Time.time > timeEvent2 && !event2Started)
            {
                Event2();
                event2Started = true;
            }

            if (Time.time > timeEvent3 && !event3Started)
            {
                Event3();
                event3Started = true;
            }

            if (Time.time > timeEvent4 && !event4Started)
            {
                Event4();
                event4Started = true;
            }
        }

        //Events Description
        private void Event1()
        {
            
        }

        private void Event2()
        {
            
            LightPickup[] fireplaces = FeuDeCamp.GetComponentsInChildren<LightPickup>();
            foreach(LightPickup f in fireplaces)
            {
                f.TurnOff();
            }
        }

        private void Event3()
        {
            LightPickup[] lights = QuartierMarchand.GetComponentsInChildren<LightPickup>();
            foreach(LightPickup l in lights)
            {
                l.TurnOff();
            }

            //Start fireworkds
        }

        private void Event4()
        {
            LightPickup[] lights = QuartierRestaurant.GetComponentsInChildren<LightPickup>();
            foreach (LightPickup l in lights)
            {
                l.TurnOff();
            }

            //turn off Fireworks
        }

        //End the event currently activated
        private void EndEvent()
        {

        }
    }
}
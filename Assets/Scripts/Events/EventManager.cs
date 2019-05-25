using System.Collections;
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

        public float timeEvent1;
        public float timeEvent2;
        public float timeEvent3;
        public float timeEvent4;

        public bool event1Started = false;
        public bool event2Started = false;
        public bool event3Started = false;
        public bool event4Started = false;


        private void Start()
        {
            
        }

        private void Update()
        {
            if(Time.time > timeEvent1 && !event1Started)
            {
                Event1();
            }

            if (Time.time > timeEvent2 && !event2Started)
            {
                Event2();
            }

            if (Time.time > timeEvent3 && !event3Started)
            {
                Event3();
            }

            if (Time.time > timeEvent4 && !event4Started)
            {
                Event4();
            }
        }

        //Events Description
        private void Event1()
        {

        }

        private void Event2()
        {

        }

        private void Event3()
        {

        }

        private void Event4()
        {

        }

        //End the event currently activated
        private void EndEvent()
        {

        }
    }
}

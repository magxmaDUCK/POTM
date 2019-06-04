using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.UI;

namespace POTM
{
    public class EventManager : MonoBehaviour
    {
        //private static object m_Lock = new object();
        private static EventManager m_Instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                //lock (m_Lock)
                //{
                    if (m_Instance == null)
                    {
                        // Search for existing instance.
                        m_Instance = FindObjectOfType<EventManager>();

                        // Create new instance if one doesn't already exist.
                        if (m_Instance == null)
                        {
                            //// Need to create a new GameObject to attach the singleton to.
                            //var singletonObject = new GameObject();
                            //m_Instance = singletonObject.AddComponent<EventManager>();
                            //singletonObject.name = typeof(EventManager).ToString() + " (Singleton)";

                            //// Make instance persistent.
                        }
                    }

                    return m_Instance;
                //}
            }
        }
        


        private void OnDestroy()
        {
            m_Instance = null;
        }

        protected EventManager() { }

        /////////////////////////////

        //ADAPT SO THAT TIME STARTS WHEN PLAYERRS STARTS PLAYING

        public bool playing = false;
        public float startTime = 0f;
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
        public Text display;




        private void Awake()
        {
            m_Instance = this;
            display.enabled = false;
        }

        private void Update()
        {
            if (playing)
            {
                if(Time.time - startTime > timeEvent1 && !event1Started)
                {
                    Event1();
                    event1Started = true;
                    Debug.Log("First");
                }

                if (Time.time - startTime > timeEvent2 && !event2Started)
                {
                    Event2();
                    event2Started = true;
                    Debug.Log("fireplace");
                }

                if (Time.time - startTime > timeEvent3 && !event3Started)
                {
                    Event3();
                    event3Started = true;
                    Debug.Log("Fireworks");
                }

                if (Time.time - startTime > timeEvent4 && !event4Started)
                {
                    Event4();
                    event4Started = true;
                    Debug.Log("all off");
                }
            }
        }

        //Events Description
        private void Event1()
        {
            
        }

        private void Event2()
        {
            
            IsFire[] fireplaces = FeuDeCamp.GetComponentsInChildren<IsFire>();

            foreach (IsFire f in fireplaces)
            {
                Destroy(f.transform.GetComponentInChildren<VisualEffect>().gameObject);
            }

            AkSoundEngine.PostEvent("Stop_State1", gameObject);
            
        }

        private void Event3()
        {
            LightPickup[] lights = QuartierMarchand.GetComponentsInChildren<LightPickup>();
            foreach(LightPickup l in lights)
            {
                l.TurnOff();
            }

            ShopLights[] sLights = QuartierMarchand.GetComponentsInChildren<ShopLights>();
            foreach(ShopLights sl in sLights)
            {
                sl.TurnOff();
            }

            AkSoundEngine.SetState("MusicSelection", "Part2");
            
            Firework.SetActive(true);
        }

        private void Event4()
        {
            Destroy(Firework);
            display.enabled = true;
            AkSoundEngine.PostEvent("Stop_State3", gameObject);
            AkSoundEngine.SetState("MusicState", "Far");
            GameObject.Find("MusicZone").SetActive(false);

            LightPickup[] lights = QuartierRestaurant.GetComponentsInChildren<LightPickup>();
            foreach (LightPickup l in lights)
            {
                l.TurnOff();
            }

            ShopLights[] sLights = QuartierRestaurant.GetComponentsInChildren<ShopLights>();
            foreach (ShopLights sl in sLights)
            {
                sl.TurnOff();
            }

        }

        //End the event currently activated
        private void EndEvent()
        {

        }

        public void NewGame()
        {
            playing = false;
        }
    }
}

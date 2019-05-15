using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightCollector : MonoBehaviour
    {

        private int score;
        private SphereCollider triggerZone;
        public Transform player;
        public Transform WwiseGlobal;

        // Start is called before the first frame update
        void Start()
        {
            triggerZone = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            transform.position = player.position;
        }

        //Add a big trigger collider around the plane, for light detection and pickups.
        private void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("hello");
            LightPickup light = collision.gameObject.GetComponent<LightPickup>();
            if(light != null && light.IsOn())
            {
                //Debug.Log("ll");
                score += light.getScoreValue();
                WwiseGlobal.transform.position = collision.transform.position;
                light.TurnOff();
            }

            //check layer 
            //if wooshable, start woosh

            if(collision.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
            {
                AkSoundEngine.PostEvent("Play_In_Woosh", this.gameObject);
                //Debug.Log("Wooooooshing");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //if wooshable exit sound;
            if (other.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
            {
                //stop woosh sound
                AkSoundEngine.PostEvent("Play_Out_Woosh", this.gameObject);
                //Debug.Log("Wooooooshing'nt");
            }
        }

        public int GetScore()
        {
            return score;
        }
    }
}

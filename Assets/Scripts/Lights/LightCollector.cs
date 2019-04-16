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
                Debug.Log("ll");
                score += light.getScoreValue();
                light.TurnOff();
            }
        }
    
        public int GetScore()
        {
            return score;
        }
    }
}

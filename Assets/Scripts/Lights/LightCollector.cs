using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightCollector : MonoBehaviour
    {

        private int score;
        private SphereCollider triggerZone;

        // Start is called before the first frame update
        void Start()
        {
            triggerZone = GetComponent<SphereCollider>();
        }

        //Add a big trigger collider around the plane, for light detection and pickups.
        private void OnTriggerEnter(Collider other)
        {
            LightPickup light = other.gameObject.GetComponent<LightPickup>();
            if(light != null && light.IsOn())
            {
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

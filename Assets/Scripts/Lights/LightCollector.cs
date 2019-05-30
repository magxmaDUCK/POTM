using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightCollector : MonoBehaviour
    {

        private ScoreManager SM;
        private SphereCollider triggerZone;
        public Transform player;
        public Transform WwiseGlobal;
        public GameObject lightFX;

        bool hasWoosh = false;
        GameObject wooshParent = null;

        // Start is called before the first frame update
        void Start()
        {
            triggerZone = GetComponent<SphereCollider>();
            lightFX.GetComponent<PickupVFX>().player = player;
            SM = ScoreManager.Instance;
        }

        private void Update()
        {
            transform.position = player.position;
        }

        //Add a big trigger collider around the plane, for light detection and pickups.
        private void OnTriggerEnter(Collider collision)
        {
            LightPickup light = collision.gameObject.GetComponent<LightPickup>();

            if (light != null && light.IsOn())
            {
                //Increment score and turn off lights
                SM.increaseScore(light.getScoreValue());
                WwiseGlobal.transform.position = collision.transform.position;
                light.TurnOff();

                //Start VFX that follows player;
                GameObject go = Instantiate(lightFX, collision.transform.position, Quaternion.identity);
                go.SetActive(true);
                go.GetComponent<PickupVFX>().player = player;
                go.GetComponent<PickupVFX>().mat = collision.GetComponent<Renderer>().material;
            }
        }
    }
}

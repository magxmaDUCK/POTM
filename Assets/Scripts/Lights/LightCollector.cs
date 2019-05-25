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

            if(light != null && light.IsOn())
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

            //check layer 
            //if wooshable, start woosh

            if(collision.gameObject.layer == LayerMask.NameToLayer("Wooshable") && !hasWoosh)
            {

                RaycastHit hitF;
                RaycastHit hitR;
                RaycastHit hitL;
                RaycastHit hitFL;
                RaycastHit hitFR;
                Debug.Log("Woosh");

                if(Physics.Raycast(transform.position, transform.forward, out hitF, 10f) && !hasWoosh)
                {
                    if(hitF.collider.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
                    {
                        //Instancier objet
                        GameObject go = Instantiate(new GameObject(), hitF.point, Quaternion.identity, collision.gameObject.transform);
                        go.layer = LayerMask.NameToLayer("WooshEmitter");
                        AkSoundEngine.PostEvent("Play_In_Woosh", go);
                        hasWoosh = true;
                        wooshParent = collision.gameObject;
                        Debug.Log("WooshING");
                    }
                }

                if(Physics.Raycast(transform.position, transform.right, out hitR) && !hasWoosh)
                {
                    if (hitR.collider.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
                    {
                        //Instancier objet
                        GameObject go = Instantiate(new GameObject(), hitR.point, Quaternion.identity, collision.gameObject.transform);
                        go.layer = LayerMask.NameToLayer("WooshEmitter");
                        AkSoundEngine.PostEvent("Play_In_Woosh", go);
                        hasWoosh = true;
                        wooshParent = collision.gameObject;
                        Debug.Log("WooshING");
                    }
                }

                if(Physics.Raycast(transform.position, -transform.right, out hitL) && !hasWoosh)
                {
                    if (hitL.collider.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
                    {
                        //Instancier objet
                        GameObject go = Instantiate(new GameObject(), hitL.point, Quaternion.identity, collision.gameObject.transform);
                        go.layer = LayerMask.NameToLayer("WooshEmitter");
                        AkSoundEngine.PostEvent("Play_In_Woosh", go);
                        hasWoosh = true;
                        wooshParent = collision.gameObject;
                        Debug.Log("WooshING");
                    }
                }

                if (Physics.Raycast(transform.position, transform.forward + transform.right, out hitFR, 10f) && !hasWoosh)
                {
                    if (hitFR.collider.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
                    {
                        //Instancier objet
                        GameObject go = Instantiate(new GameObject(), hitFR.point, Quaternion.identity, collision.gameObject.transform);
                        go.layer = LayerMask.NameToLayer("WooshEmitter");
                        AkSoundEngine.PostEvent("Play_In_Woosh", go);
                        hasWoosh = true;
                        wooshParent = collision.gameObject;
                        Debug.Log("WooshING");
                    }
                }

                if (Physics.Raycast(transform.position, transform.forward - transform.right, out hitFL, 10f) && !hasWoosh)
                {
                    if (hitFL.collider.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
                    {
                        //Instancier objet
                        GameObject go = Instantiate(new GameObject(), hitFL.point, Quaternion.identity, collision.gameObject.transform);
                        go.layer = LayerMask.NameToLayer("WooshEmitter");
                        AkSoundEngine.PostEvent("Play_In_Woosh", go);
                        hasWoosh = true;
                        wooshParent = collision.gameObject;
                        Debug.Log("WooshING");
                    }
                }



                //collision.gameObject.transform.position;

                //Debug.Log("Wooooooshing");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //if wooshable exit sound;
            if (other.gameObject.layer == LayerMask.NameToLayer("Wooshable"))
            {
                if (other.gameObject.transform.GetChild(0))
                {
                    Transform child = other.gameObject.transform.GetChild(0);
                    if (child.gameObject.layer == LayerMask.NameToLayer("WooshEmitter"))
                    {
                        AkSoundEngine.PostEvent("Play_Out_Woosh", other.transform.GetChild(0).gameObject);
                        Invoke("killWoosh", 2f);
                    }
                }
            }
        }

        private void killWoosh()
        {
            Destroy(wooshParent.transform.GetChild(0).gameObject);
            hasWoosh = false;
            Debug.Log("WooshDEAD");
        }
    }
}

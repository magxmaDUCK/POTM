using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class FlightAutoPilot : MonoBehaviour
    {
        private Ray[] rays;
        private RaycastHit[] rayHits;
        private const int NB_RAYS = 5;
        private bool[] hitIndex;
        private float dist = 1f;
        private float maxDist = 5;

        private PlaneController pc;

        [System.NonSerialized]public float speedOverride = 1f;
        [System.NonSerialized]public Vector2 controlsOverride = new Vector2();



        // Start is called before the first frame update
        void Start()
        {
            rays = new Ray[NB_RAYS];
            rayHits = new RaycastHit[NB_RAYS];
            hitIndex = new bool[NB_RAYS];
            //Forward
            rays[0] = new Ray(transform.position, transform.forward);
            //up
            rays[1] = new Ray(transform.position, (0.9f*transform.forward + 0.1f*transform.up));
            //Down
            rays[2] = new Ray(transform.position, (0.9f*transform.forward - 0.1f*transform.up));
            //Left
            rays[3] = new Ray(transform.position, (0.9f*transform.forward + 0.1f*transform.right));
            //Right
            rays[4] = new Ray(transform.position, (0.9f*transform.forward - 0.1f*transform.right));

            pc = GetComponent<PlaneController>();
    
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            maxDist = dist * pc.currentSpeed;
            controlsOverride.Set(0, 0);
            speedOverride = 1f;

            rays[0].origin = rays[1].origin = rays[2].origin = rays[3].origin = rays[4].origin = transform.position;

            rays[0].direction = transform.forward;
            rays[1].direction = (0.9f*transform.forward + 0.1f*transform.up);
            rays[2].direction = (0.9f*transform.forward - 0.1f*transform.up);
            rays[3].direction = (0.9f*transform.forward + 0.1f*transform.right);
            rays[4].direction = (0.9f*transform.forward - 0.1f*transform.right);

            for(int i = 0; i < NB_RAYS; i++)
            {
                hitIndex[i] = Physics.Raycast(rays[i], out rayHits[i]);
            }
        
            for(int i = 0; i < NB_RAYS; i++)
            {
                if (hitIndex[i])
                {
                    //Adjust maxDist to speed
                    float distRatio = Mathf.Min(rayHits[i].distance / maxDist, 1);
                    if(distRatio < 1)
                    {
                        distRatio = 1 - distRatio;
                        switch (i)
                        {
                            case 1:
                                controlsOverride.y += distRatio * 1.3f;
                                break;
                            case 2:
                                controlsOverride.y -= distRatio * 1.3f;
                                break;
                            case 3:
                                controlsOverride.x -= distRatio * 1.3f;
                                break;
                            case 4:
                                controlsOverride.x += distRatio * 1.3f;
                                break;
                            case 0:
                                if(distRatio > 0.8f)
                                {
                                    //PULLS UP AND SIDEWAYS, NOT ALWAYS BEST
                                    speedOverride = 1- 5*Time.deltaTime;

                                    //Check distance to contact

                                    if (!hitIndex[1])
                                    {
                                        controlsOverride.y -= 20f * distRatio;
                                    }
                                    else if (!hitIndex[2])
                                    {
                                        controlsOverride.y += 20f * distRatio;
                                    }
                                    else
                                    {
                                        controlsOverride.y -= 20f * distRatio;
                                    }

                                    if (!hitIndex[3])
                                    {
                                        controlsOverride.x += 20f * distRatio;
                                    }
                                    else if (!hitIndex[4])
                                    {
                                        controlsOverride.x -= 20f * distRatio;
                                    }
                                    else
                                    {
                                        controlsOverride.x += 20f * distRatio;
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            //Draw rays
            foreach (Ray r in rays)
            {
                Debug.DrawRay(r.origin, r.direction * maxDist, Color.green);
            }
        }
    }
}

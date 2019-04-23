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
        private int maxDist = 20;



        // Start is called before the first frame update
        void Start()
        {
            rays = new Ray[NB_RAYS];
            rayHits = new RaycastHit[NB_RAYS];
            hitIndex = new bool[NB_RAYS];
            //Forward
            rays[0] = new Ray(transform.position, transform.forward);
            //up
            rays[1] = new Ray(transform.position, (transform.forward + transform.up) / 2);
            //Down
            rays[2] = new Ray(transform.position, (transform.forward - transform.up)/2);
            //Left
            rays[3] = new Ray(transform.position, (transform.forward + transform.right) /2);
            //Right
            rays[4] = new Ray(transform.position, (transform.forward - transform.right)/2);
    
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rays[0].origin = rays[1].origin = rays[2].origin = rays[3].origin = rays[4].origin = transform.position;
            rays[0].direction = transform.forward;
            rays[1].direction = (transform.forward + transform.up) / 2;
            rays[2].direction = (transform.forward - transform.up) / 2;
            rays[3].direction = (transform.forward + transform.right) / 2;
            rays[4].direction = (transform.forward - transform.right) / 2;

            for(int i = 0; i < NB_RAYS; i++)
            {
                hitIndex[i] = Physics.Raycast(rays[i], out rayHits[i]);
            }
        
            for(int i = 0; i < NB_RAYS; i++)
            {
                if (hitIndex[i])
                {
                    Mathf.Min(rayHits[i].distance / maxDist, 1);
                }
            }

            //Draw ryas
            foreach (Ray r in rays)
            {
                Debug.DrawRay(r.origin, r.direction, Color.green);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace POTM
{
    public class Stars : MonoBehaviour
    {

        public GameObject expansionVFX;
        public GameObject onlineStarsVFX;
        public GameObject sphereVFX;
        public GameObject explosionVFX;

        private VisualEffect expFX;
        private VisualEffect onlineFX;

        private VisualEffect sphereFX;
        private VisualEffect explosionFX;

        private ScoreManager SM;

        public float onlineStarsTime = 0.5f;
        public float sphereTime = 2f;
        public float explosionTime = 4f;
        private float startTime = 0f;

        private bool onlineStarted = false;
        private bool sphereStarted = false;
        private bool explosionStarted = false;
        private int onlineGalaxies = 0;
        // Start is called before the first frame update
        void Start()
        {
            SM = ScoreManager.Instance;
            SM.postScore();

            expansionVFX = Instantiate(expansionVFX, transform.position, Quaternion.identity, transform);
            expFX = expansionVFX.GetComponent<VisualEffect>();
            expFX.Stop();

            //expFX.SetInt("nbStars", SM.getPlayerScore());
            expFX.SetInt("nbStars", 400);

            expFX.Play();
            startTime = Time.time;
        }

        private void OnEnable()
        {
            startTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(Time.time - startTime);
            if(Time.time - startTime > sphereTime && !sphereStarted)
            {
                GameObject sphereGO = Instantiate(sphereVFX, transform.position, Quaternion.identity, transform);
                sphereFX = sphereGO.GetComponent<VisualEffect>();
                //sphereFX.SetInt("nb Stars", SM.getPlayerScore());
                sphereFX.SetInt("nb Stars", 400);
                sphereStarted = true;
            }

            if (Time.time - startTime > explosionTime && !explosionStarted)
            {
                sphereFX.Stop();
                Destroy(sphereFX);
                GameObject explosionGO = Instantiate(explosionVFX, transform.position, Quaternion.identity, transform);
                explosionFX = explosionGO.GetComponent<VisualEffect>();
                //explosionFX.SetInt("nb Stars", SM.getPlayerScore());
                explosionFX.SetInt("nb Stars", 400);
                explosionStarted = true;
            }


            if (Time.time - startTime > onlineStarsTime && !onlineStarted)
            {
                onlineStarsVFX = Instantiate(onlineStarsVFX, transform.position, Quaternion.identity, transform);
                onlineFX = onlineStarsVFX.GetComponent<VisualEffect>();

                //int onlineScore = SM.getOnlineScore();
                int onlineScore = 40000;
                if (onlineScore > 100000)
                {
                    onlineGalaxies = onlineScore / 1000000;
                    int onlineStars = onlineScore % 1000000;
                    onlineFX.SetInt("nbStars", onlineStars);
                    //onlineFX.SetInt("nbGalaxies", onlineGalaxies);
                }
                else
                {
                    onlineFX.SetInt("nbStars", onlineScore);
                }

                while (onlineGalaxies > 0)
                {
                    int r = Random.RandomRange(1, 4);

                    if( r == 1)
                        onlineFX.SendEvent("SpawnG1");

                    else if(r == 2)
                        onlineFX.SendEvent("SpawnG2");

                    else if(r == 3)
                        onlineFX.SendEvent("SpawnG3");

                    else if(r == 4)
                        onlineFX.SendEvent("SpawnG4");

                    onlineGalaxies--;
                }
                onlineStarted = true;
            }
            
        }
    }
}

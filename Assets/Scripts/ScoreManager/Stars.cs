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

        private VisualEffect expFX;
        private VisualEffect onlineFX;

        private ScoreManager SM;

        public float onlineStarsTime = 0.5f;
        private float startTime = 0f;

        private bool onlineStarted;
        private int onlineGalaxies = 0;
        // Start is called before the first frame update
        void Start()
        {
            SM = ScoreManager.Instance;
            SM.postScore();

            expansionVFX = Instantiate(expansionVFX, transform.position, Quaternion.Euler(90, 0, 0), transform);
            expFX = expansionVFX.GetComponent<VisualEffect>();
            expFX.Stop();
            onlineStarsVFX = Instantiate(onlineStarsVFX, transform);
            onlineFX = onlineStarsVFX.GetComponent<VisualEffect>();
            onlineFX.Stop();

            expFX.SetInt("nbStars", SM.getPlayerScore());

            int onlineScore = SM.getOnlineScore();
            if(onlineScore > 1000000)
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

            expFX.Play();
            startTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - startTime > onlineStarsTime && !onlineStarted)
            {
                onlineFX.Play();
                while(onlineGalaxies > 0)
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

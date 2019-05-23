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
            onlineFX.SetInt("nbStars", SM.getOnlineScore());

            expFX.Play();
            startTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - startTime > onlineStarsTime)
            {
                onlineFX.Play();
            }
        }
    }
}

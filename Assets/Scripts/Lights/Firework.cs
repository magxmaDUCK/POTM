using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace POTM
{
    public class Firework : MonoBehaviour
    {
        public GameObject spark;
        public LightColors colors;

        private float startTime = 0f;
        private float explosionTime = 3f;
        private float endTime = 6f;
        private bool exploded = false;

        private VisualEffect rocketFX;

        void Start()
        {
            startTime += Time.time;
            endTime += Time.time;
            rocketFX = GetComponent<VisualEffect>();
            AkSoundEngine.PostEvent("Play_FireWorks_Spin", gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            rocketFX.SetVector3("pos", transform.position);

            if (Time.time - startTime > explosionTime && !exploded)
            {
                Material mat = colors.col[Random.Range(0, colors.col.Count)];
                for (int i = 0; i < 30; i++)
                {
                    GameObject go  = Instantiate(spark, transform.position, transform.rotation);
                    Renderer rend = go.GetComponent<Renderer>();
                    rend.material = mat;
                    AkSoundEngine.PostEvent("Play_FireWorks_Burst", gameObject);
                }
                exploded = true;
                Destroy(gameObject);
            }

            if(Time.time - startTime > endTime)
            {
            
            }
        }
    }
}

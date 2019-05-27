using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        void Start()
        {
            startTime += Time.time;
            endTime += Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time - startTime > explosionTime && !exploded)
            {
                Material mat = colors.col[Random.Range(0, colors.col.Count)];
                for (int i = 0; i < 6; i++)
                {
                    GameObject go  = Instantiate(spark, transform.position, transform.rotation);
                    Renderer rend = go.GetComponent<Renderer>();
                    rend.material = mat;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace POTM
{
    public class PickupVFX : MonoBehaviour
    {
        [SerializeField] public Transform player;

        [SerializeField] public Material mat;

        private VisualEffect lightFX;

        private Vector3 position;

        private float startTime = 0;

        public float duration = 1.0f;

        private float timePassed = 0f;

        private bool stopped = false;

        private Renderer childRend;
        private TrailRenderer childTRend;

        // Start is called before the first frame update
        void Start()
        {
            lightFX = GetComponent<VisualEffect>();
            childRend = transform.GetChild(1).GetComponent<Renderer>();
            position = transform.position;

            //SET VFX COLOR TO MATERIAL COLOR
            GradientAlphaKey[] keysA = new GradientAlphaKey[4];

            keysA[0].alpha = 0;
            keysA[0].time = 0;
            keysA[1].alpha = 0;
            keysA[1].time = 1;

            keysA[2].alpha = 255;
            keysA[2].time = 0.29f;
            keysA[3].alpha = 255;
            keysA[3].time = 0.77f;

            GradientColorKey[] keysC = new GradientColorKey[2];


            keysC[0].color = mat.GetColor("_EmissionColor");
            keysC[0].time = 0;

            keysC[1].color = Color.white;
            keysC[1].time = 1;

            Gradient g = new Gradient();
            g.SetKeys(keysC, keysA);

            lightFX.SetGradient("lightGradiant", g);

            startTime = Time.time;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timePassed = Time.time - startTime;
            transform.position = Vector3.Lerp(position, player.transform.position, timePassed / duration);

            if (timePassed > duration)
            {
                if (!stopped)
                {
                    lightFX.SendEvent("OnStop");
                    stopped = true;
                    Destroy(transform.GetChild(1).gameObject);

                }
                transform.position = player.transform.position;

                float ratio = (timePassed - duration) / 2f;
                //childRend.material.SetFloat("_DisolveLerp", 1 - ratio);
                Debug.Log(1 - ratio);
            }

            if (timePassed > duration + 2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class FirstPersonCameraController : MonoBehaviour
    {
        public float HorizontalSens;
        public Camera cineCam;
        public LevelChanger lc;
        private float startTime = 0f;

    
        // Update is called once per frame
        void FixedUpdate()
        {
            float turn = Input.GetAxis("Horizontal");
            Vector3 rot = transform.rotation.eulerAngles;

            //transform.Rotate(-rot);
            //transform.Local.Rotate(new Vector3(0, turn * HorizontalSens * Time.deltaTime, 0));
            //transform.Rotate(rot);
            transform.RotateAroundLocal(Vector3.up, turn * HorizontalSens * Time.deltaTime);
            

            if(Time.time - startTime > 30.0f)
            {
                lc.FadeToLevel("IslandLevel");
            }
        }
        
        void OnEnable()
        {
            startTime = Time.time;
        }
    }
}

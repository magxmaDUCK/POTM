using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class CameraController : MonoBehaviour
    {
        [Tooltip("The player the camera has to follow")]
        public PlaneController player;

        [Tooltip("Temporary varibale. Sets the base distance of the camera with the player")]
        public float baseDistanceFromPlayer;

        [Tooltip("The distance will be calculated according to the planes speed")]
        public float maxDist, minDist;
        [Tooltip("Downwards angle of the camera")]
        public float cameraAngle;
        [Tooltip("hight difference between player and camera")]
        public float cameraHeight;

        [Tooltip("Max and min FOV. Fov will be calculated by your speed / acceleration / environment")]
        public float maxFOV, minFOV;

        private Camera cam;
        private bool isTurningRight = false, isTurningLeft = false;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            transform.position = player.transform.position + (-player.transform.forward * baseDistanceFromPlayer);
            transform.position += player.transform.up * cameraHeight;
            transform.Rotate(new Vector3(cameraAngle, 0, 0));
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float speedDiff = player.maxSpeed - player.minSpeed;
            float distDiff = maxDist - minDist;
            float diffFov = maxFOV - minFOV;
            float additionalDist = (player.currentSpeed * distDiff) / speedDiff;
            float additionalFOV = (player.currentSpeed * diffFov) / speedDiff;
            
            //Change camera distance + reset height
            transform.position = (player.transform.position + -player.transform.forward * (minDist + additionalDist) + player.transform.up * cameraHeight);

            //Set FOV according to speed
            cam.fieldOfView = minFOV + additionalFOV;
        }

        public void RightOffset()
        {

            /// /!\
            /// Add boolean in PlaneController to use as an event
            /// This is done on top of the turning in plane controller

            if (!isTurningRight)
            {
                transform.Rotate(new Vector3(-cameraAngle, 0, 0));
                transform.Rotate(new Vector3(0, 5, 0));
                transform.Rotate(new Vector3(cameraAngle, 0, 0));
                isTurningRight = true;
            }
        }

        public void LeftOffset()
        {
            if (!isTurningLeft)
            {
                transform.Rotate(new Vector3(-cameraAngle, 0, 0));
                transform.Rotate(new Vector3(0, -5, 0));
                transform.Rotate(new Vector3(cameraAngle, 0, 0));
                isTurningLeft = true;
            }
        }

        public void ResetOffset()
        {
            if (isTurningLeft)
            {
                transform.Rotate(new Vector3(-cameraAngle, 0, 0));
                transform.Rotate(new Vector3(0, 5, 0));
                transform.Rotate(new Vector3(cameraAngle, 0, 0));
                isTurningLeft = false;
            }
            if(isTurningRight)
            {
                transform.Rotate(new Vector3(-cameraAngle, 0, 0));
                transform.Rotate(new Vector3(0, -5, 0));
                transform.Rotate(new Vector3(cameraAngle, 0, 0));
                isTurningRight = false;
            }
        }

    }
}

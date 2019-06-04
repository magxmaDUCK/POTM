using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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
        [Tooltip("Max offset angle of the camera for up and down")]
        public float pitchOffsetAngle;
        [Tooltip("The max angle of the camera while turning")]
        public float yawOffsetAngle;
        [Tooltip("The time to smoothen the camera position")]
        public float minSmoothTime;
        public float maxSmoothTime;

        public float smoothTimeRot;

        //Empty game object in front of plane to know where it is going;
        [Tooltip("Where the camera is looking at")]
        public GameObject target;

        [Tooltip("If rotation shake, max degrees of the rotation. Else, max movement in units (meters)")]
        public float shakeStrength;
        [Tooltip("Speed of the camera shake")]
        public float shakeSpeed;
        public bool rotationShake = true;

        [Tooltip("Use target camera type")]
        public bool target_cam = false;

        private Camera cam;
        private float speedDiff;
        private float distDiff;
        private float diffFov;

        private Vector3 velocity = Vector3.zero;
        private Vector3 velocityRot = Vector3.zero;

        private float perlinX = 1f;
        private float perlinY = 1f;


        private float additionalDist;
        private float additionalHeight;
        private float additionalFOV;
        private Vector3 playerRot;
        private float smoothTime;

        private float playerSpeedDiff;
        float currentSpeed = 0f;
        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            speedDiff = player.maxSpeed - player.minSpeed;
            distDiff = maxDist - minDist;
            diffFov = maxFOV - minFOV;

            ResetCamera();
            playerSpeedDiff = (player.maxSpeed - player.minSpeed);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            currentSpeed = Mathf.Max(player.currentSpeed - player.minSpeed, 0);
            additionalDist = (currentSpeed * distDiff) / speedDiff;
            additionalHeight = (currentSpeed * (cameraHeight-0.01f)) / speedDiff;
            additionalHeight += 0.5f;
            additionalFOV = (currentSpeed * diffFov) / speedDiff;
            playerRot = player.transform.rotation.eulerAngles;

            smoothTime = Mathf.Lerp(smoothTime, Mathf.Lerp( minSmoothTime, maxSmoothTime, (player.currentSpeed - player.minSpeed) / playerSpeedDiff), 2f * Time.fixedDeltaTime);
            //Change camera distance + reset height
            transform.position = Vector3.Lerp(
                transform.position,
                (player.transform.position
                + -player.transform.forward * (minDist + additionalDist)
                + player.transform.up * (0.01f + additionalHeight))
                ,
                smoothTime * Time.fixedDeltaTime)+(rotationShake ? Vector3.zero : CameraShake());

            //Debug.Log(currentSpeed);
            //Set camera height value so that it fits in the center of the screen;
            //Vector3 onScreenPlanePos = cam.WorldToScreenPoint(player.transform.position);
            //float screenHeightPos = cam.pixelHeight / 3;
            //onScreenPlanePos.y = screenHeightPos;
            //transform.position = cam.ScreenToWorldPoint(onScreenPlanePos);

            //Move camera so that its aligned on the 1st or 2nd third of the screen;

            //Turn and move camera to side

            /*
                      transform.rotation = Quaternion.Euler(
                           Vector3.SmoothDamp( transform.rotation.eulerAngles,
                        new Vector3(
                            cameraAngle + playerRot.x,
                            playerRot.y,
                            playerRot.z 
                        ) ,
                        ref velocityRot,
                        smoothTimeRot)
                        + (rotationShake?CameraShake():Vector3.zero));
                        */

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(new Vector3(
                    cameraAngle + playerRot.x,
                    playerRot.y,
                    playerRot.z)), smoothTimeRot * Time.fixedDeltaTime);

            //Set FOV according to speed
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, minFOV + additionalFOV, 2f * Time.fixedDeltaTime);
            //cam.fieldOfView = minFOV + additionalFOV;
        }

        public void ResetRotation()
        {
            transform.rotation = Quaternion.Euler(
                new Vector3(player.transform.rotation.eulerAngles.x + cameraAngle, player.transform.rotation.eulerAngles.y, 0)
                );
        }

        public Vector3 CameraShake()
        {
            float speedN = (player.currentSpeed - player.minSpeed)/speedDiff;

            float shakeSp = speedN * shakeSpeed;
            float shakeSt = speedN * shakeStrength;

            float perlinValueX = Mathf.PerlinNoise(Time.time * shakeSp * perlinX, Time.time * shakeSp * perlinX) - 0.5f;
            float perlinValueY = Mathf.PerlinNoise(Time.time * shakeSp * perlinY + 1.0f, Time.time * shakeSp * perlinY + 1.0f) - 0.5f;

            return new Vector3(perlinValueX * shakeSt, perlinValueY * shakeSt, 0);
        }

        public void ResetCamera()
        {
            float additionalDist = ((player.currentSpeed - player.minSpeed) * distDiff) / speedDiff;
            float additionalHeight = ((player.currentSpeed - player.minSpeed) * (cameraHeight - 0.01f)) / speedDiff;
            transform.position = (player.transform.position
                + -player.transform.forward * (minDist + additionalDist)
                + player.transform.up * (0.01f + additionalHeight));
            //transform.Rotate(new Vector3(cameraAngle, 0, 0));
        }

        
    }
}

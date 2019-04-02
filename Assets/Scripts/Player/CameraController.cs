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
        [HideInInspector]
        public float maxFOV, minFOV;
        [Tooltip("Max offset angle of the camera for up and down")]
        public float pitchOffsetAngle;
        [Tooltip("The max angle of the camera while turning")]
        public float yawOffsetAngle;

        [Tooltip("Where the camera is looking at")]
        public GameObject target;

        public float shakeStrength;
        public bool rotationShake = true;

        public Text display;

        [Tooltip("Use target camera type")]
        public bool target_cam = false;

        private Camera cam;
        private bool isTurningRight = false, isTurningLeft = false;
        private float speedDiff;
        private float distDiff;
        private float diffFov;

        private float perlinX = 1f;
        private float perlinY = 1f;

        private float camYaw = 0;
        private float camPitch = 0;
        //Make a lerp like behavior with the two variables above


        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            transform.position = player.transform.position + (-player.transform.forward * baseDistanceFromPlayer);
            transform.position += player.transform.up * cameraHeight;
            transform.Rotate(new Vector3(cameraAngle, 0, 0));

            speedDiff = player.maxSpeed - player.minSpeed;
            distDiff = maxDist - minDist;
            diffFov = maxFOV - minFOV;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float additionalDist = (player.currentSpeed * distDiff) / speedDiff;
            float additionalFOV = (player.currentSpeed * diffFov) / speedDiff;
            Vector3 playerRot = player.transform.rotation.eulerAngles;

            //Change camera distance + reset height
            transform.position = (player.transform.position + -player.transform.forward * (minDist + additionalDist) + player.transform.up * cameraHeight)
                + (rotationShake?Vector3.zero:CameraShake());

            transform.rotation = Quaternion.Euler(new Vector3(cameraAngle + playerRot.x + (player.pitch * pitchOffsetAngle), (player.planeYaw*yawOffsetAngle)
                + playerRot.y, playerRot.z ) + (rotationShake?CameraShake():Vector3.zero));

            transform.RotateAround(player.transform.position, player.transform.right, player.pitch * pitchOffsetAngle);
            transform.RotateAround(player.transform.position, Vector3.up, player.planeYaw * yawOffsetAngle);

            //Set FOV according to speed
            cam.fieldOfView = minFOV + additionalFOV;

            UpdateDisplay();
        }

        public void ResetRotation()
        {
            transform.rotation = Quaternion.Euler(new Vector3(player.transform.rotation.eulerAngles.x + cameraAngle, player.transform.rotation.eulerAngles.y, 0));
        }

        public void UpdateDisplay()
        {
            display.text = "CAMERA\ncamera offset angle yaw : " + player.yaw * yawOffsetAngle + "\n pitch angle : " + player.pitch * pitchOffsetAngle;
        }

        public Vector3 CameraShake()
        {
            float perlinValueX = Mathf.PerlinNoise(Time.time * perlinX, Time.time * perlinX) - 0.5f;
            float perlinValueY = Mathf.PerlinNoise(Time.time * perlinY + 1.0f, Time.time * perlinY + 1.0f) - 0.5f;
            return new Vector3(perlinValueX * shakeStrength, perlinValueY * shakeStrength, 0);
        }
    }
}

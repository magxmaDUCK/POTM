using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public Text display;

        private Camera cam;
        private bool isTurningRight = false, isTurningLeft = false;
        private float speedDiff;
        private float distDiff;
        private float diffFov;

        private float perlinX = 1f;
        private float perlinY = 1f;

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

            //Change camera distance + reset height
            transform.position = (player.transform.position + -player.transform.forward * (minDist + additionalDist) + player.transform.up * cameraHeight) + CameraShake();

            transform.rotation = Quaternion.Euler(new Vector3(cameraAngle + player.transform.rotation.eulerAngles.x + (player.pitch * pitchOffsetAngle), (player.yaw*yawOffsetAngle) + player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z ));

            transform.RotateAround(player.transform.position, player.transform.right, player.pitch * pitchOffsetAngle);
            transform.RotateAround(player.transform.position, Vector3.up, player.yaw * yawOffsetAngle);

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
            float perlinValue = Mathf.PerlinNoise(Time.time * perlinX, Time.time * perlinY) - 0.5f;
            return new Vector3(perlinValue * 0.01f, perlinValue * 0.01f, 0);
        }
    }
}

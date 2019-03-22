using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace POTM
{
    public class PlaneController : MonoBehaviour
    {
        [Tooltip("The min and max speed of the plane")]
        public float minSpeed, maxSpeed;
        [Tooltip("how fast the plane turns sideways")]
        public float turnSpeed;
        [Tooltip("Speed at which the plane changes it pitchs")]
        public float dipSpeed;
        [Tooltip("Angle at which the plane stays at a constant speed")]
        public float balanceAngle;
        [Tooltip("How fast the plane accelerates (gravity strength)")]
        public float accelRatio;
        [Tooltip("Max angle when turning. has to be lower than 90")]
        public float maxTurningAngle;

        [Tooltip("UI for info")]
        public Text display;

        private Rigidbody planeRB;
        [HideInInspector]public float currentSpeed;
        private CameraController cam;
        private MeshRenderer planeMesh;
        private SphereCollider triggerZone;

        [HideInInspector] public float yaw;
        [HideInInspector] public float pitch;

        private float pitchAngle;
        private bool right = false,
            left = false;

        private float lerpLength;
        private float rollStartAngle;

        //score is the number of stars
        private int score = 0;

        // Start is called before the first frame update
        void Start()
        {
            planeRB = GetComponent<Rigidbody>();
            cam = GetComponentInChildren<CameraController>();
            planeMesh = GetComponentInChildren<MeshRenderer>();
            currentSpeed = (maxSpeed + minSpeed) / 2;
            planeRB.velocity = transform.forward * maxSpeed;
            if(maxTurningAngle >= 90)
            {
                maxTurningAngle = 89;
            }

            triggerZone = GetComponent<SphereCollider>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Get controls
            yaw = Input.GetAxis("Horizontal");
            pitch = Input.GetAxis("Vertical");


            //Move bcack to horizontal, turn, and go back
            if (yaw != 0)
            {
                float currentRotation = transform.rotation.eulerAngles.x;
                transform.Rotate(new Vector3(-currentRotation, 0, 0));
                transform.Rotate(new Vector3(0, yaw * turnSpeed, 0));
                transform.Rotate(new Vector3(currentRotation, 0, 0));

                if (yaw > 0)
                {
                    if (left)
                    {
                        rollStartAngle = planeMesh.transform.rotation.eulerAngles.z;
                        left = false;
                    }
                    resetRoll();
                    planeMesh.transform.Rotate(new Vector3(0, 0, yaw * maxTurningAngle));
                    //cam.RightOffset();
                    right = true;
                }

                if (yaw < 0)
                {
                    if (right)
                    {
                        rollStartAngle = planeMesh.transform.rotation.eulerAngles.z;
                        right = false;
                    }
                    if (!left)
                    {
                     //   cam.LeftOffset(yaw);
                    }
                    resetRoll();
                    planeMesh.transform.Rotate(new Vector3(0, 0, yaw * maxTurningAngle));
                    //cam.LeftOffset();
                    left = true;
                }
            }
            else
            {
                if (right)
                {
                    resetRoll();
                    right = false;
                }
                if (left)
                {
                    resetRoll();
                    left = false;
                }
            }

            //Down
            if (((transform.rotation.eulerAngles.x < 85) || (transform.rotation.eulerAngles.x > 270)) && pitch > 0)
            {
                transform.Rotate(new Vector3(pitch * dipSpeed, 0, 0));
            }

            //Up
            if ((((transform.rotation.eulerAngles.x > 277) || (transform.rotation.eulerAngles.x < 87)) && pitch < 0))
            {
                transform.Rotate(new Vector3(pitch * dipSpeed, 0, 0));
            }

            rollStartAngle = planeMesh.transform.rotation.eulerAngles.z;

            //Update the speed and pitchAngle varibale
            updateSpeed();

            //Update the display text
            if (display != null)
                updateDisplay();
        }

        //Changes the speed depending on it's pitch angle
        //Has to be called in FixedUpdate ONLY ! affected by FPS;
        public void updateSpeed()
        {
            //Get plane's angle to set it's acceleration
            pitchAngle = transform.rotation.eulerAngles.x;
            if (pitchAngle > 200) pitchAngle -= 360;
            //Normalize
            pitchAngle /= 90;

            //Calculate its acceleration
            float speedVariation = pitchAngle * accelRatio * Time.deltaTime;
            if (pitchAngle < 0) speedVariation *= 0.8f;
            currentSpeed += speedVariation;

            //Speed limits
            if (currentSpeed < minSpeed) currentSpeed = minSpeed;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;

            planeRB.velocity = transform.forward * currentSpeed;

            //Change camera to look where you are turning
            //Set le RTPC de vitesse Wwise
            AkSoundEngine.SetRTPCValue("Speed", currentSpeed);
        }

        public void updateDisplay()
        {
            display.text = "PLANE\nSpeed : " + currentSpeed + "\nAngle : " + pitchAngle + "\n Roll angle : " + rollStartAngle ;
        }

        public void resetRoll()
        {
            planeMesh.transform.rotation = Quaternion.Euler(planeMesh.transform.rotation.eulerAngles.x, planeMesh.transform.rotation.eulerAngles.y, 0);
        }

        //Add a big trigger collider around the plane, for light detection and pickups.
        private void OnTriggerEnter(Collider other)
        {
            LightCollectible light = other.gameObject.GetComponent<LightCollectible>();
            if(light != null)
            {
                score++;
                light.turnOff();
            }
        }
    }
}

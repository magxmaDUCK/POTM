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
        [Range(-90, 90)]
        public float balanceAngle;
        [Tooltip("The offset from the balanced angle so that the plane doesn't aim for it's average speed, from 0 - 90")]
        [Range(0, 90)]
        public float balanceDeadZone;
        [Tooltip("The average speed the plane will aim for at balanced angles")]
        public float cruisingSpeed;
        [Tooltip("How fast the plane accelerates (gravity strength)")]
        public float accelRatio;
        [Tooltip("How fast the plane slows down to go to cruising speed")]
        public float decelRatio;
        [Tooltip("Max angle when turning. has to be lower than 90")]
        [Range(0, 90)]
        public float maxTurningAngle;

        [Tooltip("UI for info")]
        public Text display;

        public bool newControls = false;
        
        public float yawTurningSpeed;

        private Rigidbody planeRB;
        [HideInInspector]public float currentSpeed;
        private CameraController cam;
        private MeshRenderer planeMesh;
        private CapsuleCollider planeCollider;

        [HideInInspector] public float yaw;
        [HideInInspector] public float pitch;

        private float pitchAngle;
        private bool right = false,
            left = false;

        [HideInInspector]public float planeYaw = 0;

        private float lerpLength;
        private float rollStartAngle;

        private float speedDiff;

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
            
            planeCollider = GetComponent<CapsuleCollider>();
            speedDiff = maxSpeed - minSpeed;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Get controls
            yaw = Input.GetAxis("Horizontal");
            AkSoundEngine.SetRTPCValue("Wind_Yaw", yaw);
            pitch = Input.GetAxis("Vertical");
            AkSoundEngine.SetRTPCValue("Wind_Pitch", pitch);

            if (!newControls)
            {
                Turning1();
            }
            else
            {
                Turning2();
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
            if (pitchAngle > 200) pitchAngle -= 360f;
            //Normalize
            pitchAngle += balanceAngle;
            pitchAngle /= 90;

            if (pitchAngle > 1.0f) pitchAngle = 1.0f;
            else if (pitchAngle < -1.0f) pitchAngle = -1.0f;

            float speedVariation;
            //Calculate its acceleration
            if (pitchAngle < (balanceAngle - balanceDeadZone)/90f || pitchAngle > (balanceAngle + balanceDeadZone)/90f)
            {
                //Not in cruising speed pos
                speedVariation = pitchAngle * accelRatio * Time.deltaTime;
                if (pitchAngle < balanceAngle) speedVariation += -2f * pitch * Time.deltaTime;
            }
            else
            {
                //Aim towards cruising speed
                if(currentSpeed > cruisingSpeed - 0.1f && currentSpeed < cruisingSpeed + 0.1f)
                {
                    currentSpeed = cruisingSpeed;
                    speedVariation = 0;
                }
                else if(currentSpeed > cruisingSpeed)
                {
                    speedVariation = - decelRatio * Time.deltaTime;
                }
                else
                {
                    speedVariation = decelRatio * Time.deltaTime;
                }
            }

            currentSpeed += speedVariation;

            //Speed limits
            if (currentSpeed < minSpeed) currentSpeed = minSpeed;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;

            planeRB.velocity = transform.forward * currentSpeed;

            //Change camera to look where you are turning
            //Set le RTPC de vitesse Wwise

            AkSoundEngine.SetRTPCValue("Wind_Speed", (currentSpeed - minSpeed)/speedDiff);
        }

        public void updateDisplay()
        {
            display.text = "PLANE\nSpeed : " + currentSpeed + "\nAngle : " + pitchAngle + "\n Roll angle : " + rollStartAngle ;
        }

        public void resetRoll()
        {
            planeMesh.transform.rotation = Quaternion.Euler(planeMesh.transform.rotation.eulerAngles.x, planeMesh.transform.rotation.eulerAngles.y, 0);
        }

        private void Turning1()
        {
            if (Mathf.Abs(yaw) > Mathf.Abs(pitch))
            {
                AkSoundEngine.SetRTPCValue("Wind_Move", yaw);
            }
            else
            {
                AkSoundEngine.SetRTPCValue("Wind_Move", pitch);
            }

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
                    right = true;
                }

                if (yaw < 0)
                {
                    if (right)
                    {
                        rollStartAngle = planeMesh.transform.rotation.eulerAngles.z;
                        right = false;
                    }
                    resetRoll();
                    planeMesh.transform.Rotate(new Vector3(0, 0, yaw * maxTurningAngle));
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
        }

        private void Turning2()
        {
            if (Mathf.Abs(planeYaw) > Mathf.Abs(pitch))
            {
                AkSoundEngine.SetRTPCValue("Wind_Move", yaw);
            }
            else
            {
                AkSoundEngine.SetRTPCValue("Wind_Move", pitch);
            }


            if(planeYaw < yaw + 0.05f && planeYaw > yaw - 0.05f)
            {
                planeYaw = yaw;
            }
            else if (planeYaw < yaw)
            {
                planeYaw += yawTurningSpeed * Time.deltaTime;
                if (planeYaw > yaw) planeYaw = yaw;
            }
            else
            {
                planeYaw -= yawTurningSpeed * Time.deltaTime;
                if (planeYaw < yaw) planeYaw = yaw;
            }

            float currentRotation = transform.rotation.eulerAngles.x;
            transform.Rotate(new Vector3(-currentRotation, 0, 0));
            transform.Rotate(new Vector3(0, planeYaw * turnSpeed, 0));
            transform.Rotate(new Vector3(currentRotation, 0, 0));

            resetRoll();
            planeMesh.transform.Rotate(new Vector3(0, 0, planeYaw * maxTurningAngle));
        }

        [ExecuteInEditMode]
        private void OnValidate()
        {
            if(cruisingSpeed < minSpeed)
            {
                cruisingSpeed = minSpeed;
            }
            if(cruisingSpeed > maxSpeed)
            {
                cruisingSpeed = maxSpeed;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            transform.position += new Vector3(0, 100, 0);
        }
    }
}

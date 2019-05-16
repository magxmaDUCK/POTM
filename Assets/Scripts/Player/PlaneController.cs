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

        [Range(0,1)]
        public float joystickDiffTolerence = 0.4f;

        [Tooltip("UI for info")]
        public Text display;

        public bool newControls = false;

        public float yawTurningSpeed;

        public bool multiplayer = false;

        public bool seesaw = false;

        [HideInInspector] public float currentSpeed;
        private Rigidbody planeRB;
        private CameraController cam;
        private MeshRenderer planeMesh;
        private CapsuleCollider planeCollider;
        private ArduinoReader AR;

        [HideInInspector] public float yaw;
        [HideInInspector] public float pitch;

        private float pitchAngle;
        private bool right = false;
        private bool left = false;

        [HideInInspector] public float planeYaw = 0;
        [HideInInspector] public float planePitch = 0;

        private float lerpLength;
        private float rollStartAngle;

        private float speedDiff;

        private float yawP1;
        private float yawP2;
        private float pitchP1;
        private float pitchP2;

        private FlightAutoPilot autoPilot;

        // Start is called before the first frame update
        private void Awake()
        {
            planeRB = GetComponent<Rigidbody>();
            cam = GetComponentInChildren<CameraController>();
            planeMesh = GetComponentInChildren<MeshRenderer>();
            currentSpeed = (maxSpeed + minSpeed) / 2;
            planeRB.velocity = transform.forward * maxSpeed;
            if (maxTurningAngle >= 90)
            {
                maxTurningAngle = 89;
            }

            planeCollider = GetComponent<CapsuleCollider>();
            speedDiff = maxSpeed - minSpeed;
            autoPilot = GetComponent<FlightAutoPilot>();
        }

        void Start()
        {
            AR = GetComponent<ArduinoReader>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Get controls
            if (seesaw)
            {
                yawP1 = ((float)AR.dist * 2.0f) / (AR.distMax - AR.distMin) - 2.0f;
                pitchP1 = ((float)AR.potP1 * 2.0f) / (AR.potMax - AR.potMin) - 1.0f;
                pitchP2 = ((float)AR.potP2 * 2.0f) / (AR.potMax - AR.potMin) - 1.0f;

                //Debug.Log("d : " + yawP1 + " x : " + pitchP1 + " y : " + pitchP2);
                yaw = yawP1;
                pitch = (pitchP1 / 2.0f) + (pitchP2 / 2.0f);
                
            }
            else
            {
                yawP1 = Input.GetAxis("Horizontal");
                yawP2 = Input.GetAxis("Horizontal2");

                pitchP1 = Input.GetAxis("Vertical");
                pitchP2 = Input.GetAxis("Vertical2");

                if (multiplayer)
                {

                    float yawDiff = yawP2 - yawP1;
                    float pitchDiff = pitchP2 - pitchP1;
                    float yawResult = Mathf.Min(Mathf.Abs(yawDiff), joystickDiffTolerence);
                    float pitchResult = Mathf.Min(Mathf.Abs(pitchDiff), joystickDiffTolerence);

                    yawP1 *= (1 - yawResult);
                    yawP2 *= (1 - yawResult);
                    pitchP1 *= (1 - pitchResult);
                    pitchP2 *= (1 - pitchResult);

                    yaw = (yawP1 + yawP2) / 2;
                    pitch = (pitchP1 + pitchP2) / 2;
                    /*
                    //Too precise ! Need to tone down
                    if (CloseTo(yawP1, 0.6f, yawP2) && CloseTo(pitchP1, 0.6f, pitchP2))
                    {
                        yaw = (yawP1 + yawP2) / 2;
                        pitch = (pitchP1 + pitchP2) / 2;
                    }
                    else
                    {
                        yaw = (yawP1 + yawP2) / 6;
                        pitch = (pitchP1 + pitchP2) / 6;
                    }
                    */
                }
                else
                {
                    yaw = yawP1;
                    pitch = pitchP1;
                }
            }

            AkSoundEngine.SetRTPCValue("Wind_Yaw", planeYaw);
            AkSoundEngine.SetRTPCValue("Wind_Pitch", pitchAngle);

            yaw += autoPilot.controlsOverride.x;
            pitch += autoPilot.controlsOverride.y;
            currentSpeed *= autoPilot.speedOverride;

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

        public void AutoEvasion()
        {

        }

        //Changes the speed depending on it's pitch angle
        //Has to be called in FixedUpdate ONLY ! affected by FPS;
        public void updateSpeed()
        {
            //Get plane's angle to set it's acceleration
            pitchAngle = transform.rotation.eulerAngles.x;
            if (pitchAngle > 200) pitchAngle -= 360f;

            //Normalize
            //pitchAngle += balanceAngle;
            pitchAngle /= 90;

            if (pitchAngle > 1.0f) pitchAngle = 1.0f;
            else if (pitchAngle < -1.0f) pitchAngle = -1.0f;

            float speedVariation;

            //Calculate its acceleration
            if (pitchAngle < ((balanceAngle - balanceDeadZone)/90f) || pitchAngle > ((balanceAngle + balanceDeadZone)/90f))
            {
                //Not in cruising speed pos
                speedVariation = pitchAngle * accelRatio * Time.deltaTime;
                if (pitchAngle < ((balanceAngle - balanceDeadZone)/90f)) speedVariation += -2f * accelRatio * Mathf.Abs(pitchAngle) * Time.deltaTime;
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
            //Set le RTPC de vitesse Wwiseqq

            AkSoundEngine.SetRTPCValue("Wind_Speed", (currentSpeed - minSpeed)/speedDiff);
        }

        public void updateDisplay()
        {
            display.text = "PLANE\nSpeed : " + currentSpeed + "\nAngle : " + pitchAngle + "\n Roll angle : " + rollStartAngle;
        }

        public void resetRoll()
        {
            planeMesh.transform.rotation = Quaternion.Euler(planeMesh.transform.rotation.eulerAngles.x, planeMesh.transform.rotation.eulerAngles.y, 0);
        }

        private void Turning1()
        {
            if (Mathf.Abs(yaw) > Mathf.Abs(pitch))
            {
                AkSoundEngine.SetRTPCValue("Wind_Move", planeYaw);
            }
            else
            {
                //pitchAngle;
                AkSoundEngine.SetRTPCValue("Wind_Move", planePitch);
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
            if(collision.gameObject.tag != "collector")
            {
                /*
                 * RESPAWN CODE
                transform.position += new Vector3(0, 100, 0);
                transform.rotation = Quaternion.identity;
                planeRB.velocity = transform.forward * cruisingSpeed;
                planeRB.angularVelocity = Vector3.zero;
                planeYaw = 0;
                planePitch = 0;
                cam.ResetCamera();
                */
                //BOUNCE OFF WALLS CODE

                //Change rotation according to angle of crassh
                if (collision.gameObject.layer != LayerMask.NameToLayer("AudioBox"))
                {
                    RaycastHit colPt;
                    Physics.Raycast(transform.position - transform.forward * 2, transform.forward, out colPt);
                    Vector3 reflectedVector = Vector3.Reflect(transform.forward, colPt.normal);

                    Quaternion newRot = Quaternion.LookRotation(reflectedVector);
                    transform.rotation = newRot;
                    planeRB.velocity = transform.forward * cruisingSpeed;
                    planeRB.angularVelocity = Vector3.zero;
                    planeYaw = 0;
                    planePitch = 0;
                }
            }
        }

        private bool SameSign(float a, float b)
        {
            return (a < 0f && b < 0f) || (a >= 0f && b >=0f);
        }

        private bool CloseTo(float value, float offset, float closeTo)
        {
            return value < (closeTo + offset) && value > (closeTo - offset);  
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class StartMenu : MonoBehaviour
    {

        public bool seesaw;
        public bool multiplayer;
        [Range(0, 1)]
        public float joystickDiffTolerence = 0.4f;

        private float yawP1;
        private float yawP2;
        private float pitchP1;
        private float pitchP2;

        private float pitch;
        private float yaw;

        private float startTime;
        private bool started = false;

        public float duration = 5f;

        private ArduinoReader AR;
        // Start is called before the first frame update
        private void Awake()
        {
            GetComponent<PlaneController>().enabled = false;
            GetComponent<FlightAutoPilot>().enabled = false;
        }

        void Start()
        {
            AR = GetComponent<ArduinoReader>();
            EventManager.Instance.NewGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (seesaw)
            {
                //yawP1 = ((float)AR.dist) * 2.0f / (AR.distMax - AR.distMin) - 2.0f;
                yawP1 = (((float)AR.dist - (float)AR.distMin) * 2f / ((float)AR.distMax - (float)AR.distMin)) - 1f;
                yawP1 = -yawP1;

                pitchP1 = ((float)AR.potP1 * 2.0f) / (AR.potMaxG - AR.potMinG) - 1.0f;
                pitchP2 = ((float)AR.potP2 * 2.0f) / (AR.potMaxD - AR.potMinD) - 1.0f;
                pitchP2 = -pitchP2;

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
                }
                else
                {
                    yaw = yawP1;
                    pitch = pitchP1;
                }
            }

            if(pitch > 0.9f && !started)
            {
                GetComponent<Spline.Walker>().enabled = true;
                startTime = Time.time;
                started = true;
            }

            if(Time.time - startTime > duration && started)
            {
                GetComponent<Spline.Walker>().enabled = false;
                GetComponent<PlaneController>().enabled = true;
                GetComponent<FlightAutoPilot>().enabled = true;
                EventManager.Instance.startTime = Time.time;
                EventManager.Instance.playing = true;
                Destroy(this);
            }
        }
    }
}

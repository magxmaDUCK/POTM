using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class Boost : MonoBehaviour
    {
        public bool directionnalBoost = false;
        public float boostSpeed = 5.0f;
        public Vector3 boostDirection;
        public float maxAngle = 45f;

        private void OnTriggerEnter(Collider other)
        {
            //Add code for boost to work in only one direction.
            PlaneController pc = other.GetComponent<PlaneController>();
            if (pc != null)
            {
                if (directionnalBoost)
                {
                    if(directionIsSimilar(other.transform.forward, boostDirection))
                    {
                        pc.currentSpeed += 5.0f;
                    }
                }
                else
                {
                    pc.currentSpeed += 5.0f;
                }
            }
        }

        private bool directionIsSimilar(Vector3 A, Vector3 B)
        {
            return (Vector3.Angle(A, B) > maxAngle);
        }
    }
}

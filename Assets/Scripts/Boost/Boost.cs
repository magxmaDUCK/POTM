using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class Boost : MonoBehaviour
    {
        public bool directionnalBoost = false;
        public float boostSpeed = 5.0f;
        public List<Vector3> boostDirections = new List<Vector3>();
        public float maxAngle = 45f;

        private void Start()
        {
            foreach(Vector3 bd in boostDirections)
            {
                bd.Normalize();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Add code for boost to work in only one direction.
            PlaneController pc = other.GetComponent<PlaneController>();
            if (pc != null)
            {
                if (directionnalBoost)
                {
                    foreach(Vector3 bd in boostDirections)
                    {
                        if(directionIsSimilar(other.transform.forward, bd))
                        {
                            pc.currentSpeed += 5.0f;
                        }
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
            return (Vector3.Angle(A, B) < maxAngle);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            foreach (Vector3 bd in boostDirections)
                Gizmos.DrawRay(transform.position, bd);
        }
    }
}

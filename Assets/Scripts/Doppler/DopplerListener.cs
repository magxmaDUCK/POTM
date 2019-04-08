using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doppler
{
    public class DopplerListener : MonoBehaviour
    {
        private Vector3 prevPosition;
        private Vector3 position;

        private float moveDist;
        private float speed;

        private void Start()
        {
            prevPosition = transform.position;
            position = prevPosition;

            DopplerEmitter.AddListener(this);
        }

        private void Update()
        {
            moveDist = (prevPosition - transform.position).magnitude;
            speed = moveDist / Time.deltaTime;
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            prevPosition = position;
            position = transform.position;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public Vector3 GetPrevPosition()
        {
            return prevPosition;
        }
    }
}

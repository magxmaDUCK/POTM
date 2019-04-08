using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doppler
{
    public class DopplerListener : MonoBehaviour
    {
        private Vector3 prevPosition;

        private Vector3 position;

        //Speed of sound in air in m.s-1
        private float celerity = 340f;

        private float moveDist;

        private float speed;

        private void Awake()
        {
            DopplerEmitter.AddListener(this);
        }

        private void Start()
        {
            prevPosition = transform.position;
            position = prevPosition;
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

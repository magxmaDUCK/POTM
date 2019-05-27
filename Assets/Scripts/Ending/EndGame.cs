using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace POTM
{
    public class EndGame : MonoBehaviour
    {
        public LevelChanger lc;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlaneController>())
            {
                lc.FadeToLevel("StarrySky");
            }
        }
    }
}

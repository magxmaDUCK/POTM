using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightCollectible : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void turnOff()
        {

            //destroy this script
            Destroy(this);
        }
    }
}

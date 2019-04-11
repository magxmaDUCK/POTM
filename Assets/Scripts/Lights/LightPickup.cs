using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightPickup : MonoBehaviour
    {
        private bool on = true;

        private int scoreValue = 1;

        public LightPickup()
        {
            on = true;
            scoreValue = 1;
        }

        public LightPickup(bool onoff, int score)
        {
            on = onoff;
            scoreValue = score;
        }

        virtual public void TurnOff()
        {
            on = false;
        }

        public int getScoreValue()
        {
            return scoreValue;
        }

        public bool IsOn()
        {
            return on;
        }
    }
}

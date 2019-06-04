using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class ShopLights : MonoBehaviour
    {
        public List<LightPickup> light = new List<LightPickup>();
        
        public void TurnOff()
        {
            foreach (LightPickup l in light)
            {
                if(l != null)
                {
                    l.TurnOff();
                }
            }
        }
    }

}

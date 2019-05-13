using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace POTM
{
    [CreateAssetMenu(fileName = "ColorPicker", menuName ="Lights/Colors", order = 1)]
    public class LightColors : ScriptableObject
    {
        public List<Material> col;
    }
}

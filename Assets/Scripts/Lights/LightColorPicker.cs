using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POTM
{
    public class LightColorPicker : MonoBehaviour
    {
        public LightColors colors;
        
        // Start is called before the first frame update
        void Awake()
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material = colors.col[Random.Range(0, colors.col.Count)];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineDecorator : MonoBehaviour
{
    public BezierSpline path;

    public int frequency;

    public Vector3 offset = Vector3.zero;

    public GameObject[] items;

    [Tooltip("Apply curves rotation to the objects instantiated")]
    public bool curveRotation = false;

    public bool distanceBased = false;

    private void Awake()
    {
        if (frequency <= 0 || items.Length == 0 || path == null)
        {
            return;
        }

        float step = 1f / ((float)frequency + 1f);

        for(int i = 1; i <= frequency; i++)
        {
            int index = Random.Range(0, items.Length - 1);

            Vector3 pos = path.GetPoint(i * step);

            GameObject Go = Instantiate(items[index], pos + offset, Quaternion.identity, transform);

            //Turns object to go in the direction of the bezier curve
            if (curveRotation)
            {
                Go.transform.forward = path.GetDirection(i * step);
            }
        }
    }
}

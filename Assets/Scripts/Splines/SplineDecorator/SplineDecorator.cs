using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineDecorator : MonoBehaviour
{

    public BezierSpline path;

    public int frequency;

    public GameObject[] items;

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

            Instantiate(items[index], pos, Quaternion.identity);
        }
    }
}

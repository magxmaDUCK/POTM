using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMesh : MonoBehaviour
{
    public BezierSpline path;

    public int subDivisions;

    public Mesh mesh;

    private void Awake()
    {
        //multiply by 3 because 3 points at each subdiv;
        Vector3[] vertices = new Vector3[subDivisions * 3];

        for(int i = 0; i < subDivisions; i++)
        {
            mesh = new Mesh();
            float t = (float)i / (float)subDivisions;
            Vector3 splinePointPos = path.GetPoint(t);
            Vector3 splinePointDir = path.GetDirection(t);

            //up, down left, down right
            vertices[i] = new Vector3(0, 1, 0) + splinePointPos;
            vertices[i + 1] = new Vector3(-.5f, -.5f, 0) + splinePointPos;
            vertices[i + 2] = new Vector3(.5f, -.5f, 0) + splinePointPos;

            mesh.vertices = vertices;
        }
    }
}

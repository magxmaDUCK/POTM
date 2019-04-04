using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public BezierSpline path;

    [Range(1,100)]
    public int subDivisions;

    public Mesh mesh;

    private void Awake()
    {
        //multiply by 3 because 3 points at each subdiv;
        Vector3[] vertices = new Vector3[subDivisions * 3];
        int[] triangles = new int[(2 + 6 * subDivisions) * 3];

        for(int i = 0; i <= subDivisions; i++)
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


            if(i == 0)
            {
                triangles[i] = i;
                triangles[i+1] = i+1;
                triangles[i+2] = i+2;
            }
            else if(i == subDivisions)
            {
                //Tube and end
            }
            else
            {
                //Tube
            }
        }
    }

    //Used for debugging, to delete !
    private void Update()
    {
        Vector3 pos, dir, tan;
        float t;

        for(int i = 0; i <= 10; i++)
        {
            t = (float)i / (float)10;
            pos = path.GetPoint(t);
            dir = path.GetDirection(t);
            tan = path.GetNormal(t);

            //Debug.DrawLine(pos, pos + dir);
            //Debug.DrawLine(pos, pos + tan);

        }
    }
}

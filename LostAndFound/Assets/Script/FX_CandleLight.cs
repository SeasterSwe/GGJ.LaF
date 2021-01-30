using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_CandleLight : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertsOrgPos;
    Vector3[] verts;

    public Transform pLight;
    // Start is called before the first frame update
    void Start()
    {
        mesh =
      gameObject.GetComponent<MeshFilter>().mesh;

        verts = mesh.vertices;
        vertsOrgPos = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        for (int v = 0; v < verts.Length; v++)
        {
            verts[v].x = vertsOrgPos[v].x + Mathf.Sin(Time.time + v  *0.1f) * v *0.01f;
            verts[v].y = vertsOrgPos[v].y + Mathf.Sin(Time.time * 4 + v  *0.1f) * v *0.01f;

        }

        pLight.position = transform.TransformPoint((verts[verts.Length - 1] + verts[verts.Length - 2]) * 0.5f);

        mesh.vertices = verts;
        mesh.RecalculateBounds();
    }
}

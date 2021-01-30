using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //Y= mxb
    public float offsett = 10;
    public float angle = 60;


    public Transform pOne;
    public Transform pTwo;
    public Transform pTree;
    public Vector3 pointOne;
    public Vector3 pointTwo;

    public Vector3 result;
    public Vector3 midPoint;
    public Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointOne = pOne.position;
        pointTwo = pTwo.position;


        dir = (pointTwo - pointOne);
        float baseLength = dir.magnitude;
        float sinAng = 60;

        float hypLength = baseLength * Mathf.Sin(sinAng);
        Debug.DrawRay(pointTwo, dir * hypLength);
        Debug.DrawRay(pointOne, Quaternion.Euler(90, 0, 0) * dir.normalized * hypLength);

        result = pointOne + Quaternion.Euler(90, 0, 0) * dir.normalized * hypLength;
        Debug.DrawRay(result, Vector3.forward);
        result += (result - pointTwo).normalized * offsett; 


        pTree.position = result;

        midPoint = (pointOne + pointTwo) * 0.5f;

        pTree.LookAt(midPoint);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{

    //Y= mxb
    public float offsett = 10;
    public float offsettMid = -2;
    public float angle = 55;

    public void UpdateCamPos(Vector3 posOne, Vector3 posTwo)
    {
        Vector3 dir = (posOne - posTwo);
        float baseLength = dir.magnitude;

        Vector3 midpoint = (posOne + posTwo) * 0.5f;
        Vector3 campos = midpoint + Quaternion.Euler(angle, 0, 0) * (dir.normalized * (baseLength + offsett));
        transform.DOMove(campos, 2f).SetEase(Ease.InQuad);
        transform.DORotateQuaternion(Quaternion.LookRotation( midpoint - campos), 2f).SetEase(Ease.InQuad);
    }


    public void ShakeCam()
    {
//        transform.DOShakePosition(0.5f, Vector3.one * 0.25f ,100, 90, true);
        StartCoroutine( ShakeCamOverTime());
    }

    IEnumerator ShakeCamOverTime()
    {
        Vector3 orgPos = transform.position;
        float time = 0;
        while (time < 0.25f)
        {
            transform.position += Random.insideUnitSphere * 0.05f;
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = orgPos;
    }
}

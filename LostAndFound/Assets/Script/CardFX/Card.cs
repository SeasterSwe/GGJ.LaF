using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool busy;
    float hoverPos;
    float hoverStr = 0.2f;

    Vector3 orgPos;
    Vector3 hovPos;

    private void Start()
    {
        orgPos = transform.position;
        hoverPos = Random.Range(0, Mathf.PI);
        hovPos = transform.position + Vector3.up * hoverStr;
    }

    public void StartFlipCard()
    {
        if (!busy)
        {
            StartCoroutine(FlipCard());
            busy = true;
        }
    }
    // Update is called once per frame
    private void Update()
    {

        //transform.position = Vector3.Lerp(orgPos, hovPos, Mathf.Sin(hoverPos));
        //hoverPos += Time.deltaTime;
    }
    IEnumerator FlipCard()
    {
        int r = 0;
        while (r <= 180)
        {
            transform.rotation = Quaternion.Euler(0, 0, r);
            r++;
            yield return new WaitForEndOfFrame();
        }
        busy = false;
    }
}

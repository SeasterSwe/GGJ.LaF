using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDOMe : MonoBehaviour
{
    public AnimationCurve aCurve;
    public float v = 0;
    private float t;
    private void Start()
    {
        t = transform.localScale.x; 
    }
    // Update is called once per frame
    void Update()
    {
        v += Time.deltaTime * 0.25f;
        transform.localScale = Vector3.one * t * aCurve.Evaluate(v);
    }
}

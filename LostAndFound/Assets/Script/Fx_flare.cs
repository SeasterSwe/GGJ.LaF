using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_flare : MonoBehaviour
{
    public float speed = 0.1f;
    public float range = 2;
    Vector3 orgPos;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        targetPos = Random.insideUnitSphere * range + orgPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if(transform.position == targetPos)
        {
            targetPos = Random.insideUnitSphere * range + orgPos;
        }
    }
}

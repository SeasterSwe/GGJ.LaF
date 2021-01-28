using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public Transform pointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            pointer.position = hit.point;
            if (hit.collider.GetComponent<Card>())
            {
                print("HitCard");
                hit.collider.gameObject.GetComponent<Card>().StartFlipCard();
            }
        }
    }
}

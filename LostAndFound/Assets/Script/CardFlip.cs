using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public Transform pointer;

    public Transform cOne;
    public Transform cTwo;

    public ShuffleCards shffleCard;
    // Start is called before the first frame update
    void Start()
    {
        if (!shffleCard)
            shffleCard = GetComponent<ShuffleCards>();
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
                if (!hit.collider.gameObject.GetComponent<Card>().busy)
                {
                    if (cOne != hit.collider.transform)
                    {
                        cTwo = cOne;
                        cOne = hit.collider.transform;
                    }
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    shffleCard.FlipThisCardAround(hit.transform);
                }

                if(Input.GetButtonDown("Fire2") && cTwo != null)
                {
                    shffleCard.SwapCardOneWithTwo(cOne.transform, cTwo.transform);
                }
            }
        }
    }
}

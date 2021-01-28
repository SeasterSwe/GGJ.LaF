using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public Transform pointer;

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
              //  if (!hit.collider.gameObject.GetComponent<Card>().busy)
               // {
                    shffleCard.FlipThisCard(hit.transform);
                //}
            }
        }
    }
}

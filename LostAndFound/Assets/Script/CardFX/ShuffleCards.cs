using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    public Transform cardOne;
    public Transform cardTwo;
    public Transform cardTree;

    public bool busy;
    

    void Start()
    {
        StartCoroutine(SwapCards(cardOne, cardTwo));
        StartCoroutine(FlipCard(cardTree));
    }

    public IEnumerator SwapCards(Transform cardOne, Transform cardTwo)
    {
        busy = true;
        Vector3 dir = Quaternion.Euler(0, 90, 0) * (cardOne.position - cardTwo.position).normalized;
        Vector3 midPoint = (cardOne.position + cardTwo.position) * 0.5f;
        Vector3 dirOne = cardOne.position - midPoint;
        Vector3 dirTwo = cardTwo.position - midPoint;


        int angle = 0;
        while(angle <= 180)
        {
            busy = true;
            cardOne.position = Quaternion.Euler(dir * angle) * dirOne + midPoint;
            cardTwo.position = Quaternion.Euler(dir * angle) * dirTwo + midPoint;
            yield return new WaitForEndOfFrame();
            angle++;
            print(angle);
        }

        busy = false;
    }

    IEnumerator FlipCard(Transform card)
    {
        busy = true;    
        int r = 0;
        while (r <= 180)
        {
            busy = true;
            card.rotation = Quaternion.Euler(0, 0, r);
            r++;
            yield return new WaitForEndOfFrame();
        }
        busy = false;
    }
}

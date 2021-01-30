using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    public bool busy;
    int rotSpeed = 4;
    public AudioClip swapSound;
    public AudioClip cardSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwapCardOneWithTwo(Transform cardOne, Transform cardTwo)
    {
        StartCoroutine(SwapCards(cardOne, cardTwo));
    }
    public void FlipThisCard(Transform card)
    {
        if (card.gameObject.GetComponent<Card>().open)
        {
            StartCoroutine(FlipCardClose(card));
        }
        else
        {
            StartCoroutine(FlipCardOpen(card));
        }
    }

    public void FlipThisCardOpen(Transform card)
    {
        StartCoroutine(FlipCardOpen(card));
    }

    public void FlipThisCardClose(Transform card)
    {
        StartCoroutine(FlipCardClose(card));
    }

    public void FlipThisCardAround(Transform card)
    {
        StartCoroutine(FlipCard360(card));
    }

    private IEnumerator SwapCards(Transform cardOne, Transform cardTwo)
    {
        busy = true;
        cardOne.gameObject.GetComponent<Card>().busy = true;
        cardTwo.gameObject.GetComponent<Card>().busy = true;

        Vector3 dir = Quaternion.Euler(0, -90, 0) * (cardOne.position - cardTwo.position).normalized;
        Vector3 midPoint = (cardOne.position + cardTwo.position) * 0.5f;
        Vector3 dirOne = cardOne.position - midPoint;
        Vector3 dirTwo = cardTwo.position - midPoint;

        audioSource.clip = swapSound;
        audioSource.Play();

        int angle = 0;
        while (angle <= 180)
        {
            busy = true;
            cardOne.position = Quaternion.Euler(dir * angle) * dirOne + midPoint;
            cardTwo.position = Quaternion.Euler(dir * angle) * dirTwo + midPoint;
            yield return new WaitForEndOfFrame();
            angle += rotSpeed;
        }

        cardOne.gameObject.GetComponent<Card>().busy = false;
        cardTwo.gameObject.GetComponent<Card>().busy = false;
        busy = false;
    }

    private IEnumerator FlipCardOpen(Transform card)
    {
        busy = true;
        card.gameObject.GetComponent<Card>().busy = true;
        int r = (int)card.eulerAngles.z;
        audioSource.clip = cardSound;
        audioSource.Play();
        while (r <= 180)
        {
            busy = true;
            card.rotation = Quaternion.Euler(0, 0, r);
            r += rotSpeed;
            yield return new WaitForEndOfFrame();
        }

        card.gameObject.GetComponent<Card>().open = true;
        card.gameObject.GetComponent<Card>().busy = false;
        busy = false;

    }

    private IEnumerator FlipCardClose(Transform card)
    {
        if (card)
        {
            audioSource.clip = cardSound;
            audioSource.Play();

            busy = true;
            card.gameObject.GetComponent<Card>().busy = true;
            int r = (int)card.eulerAngles.z;
            while (r >= 0)
            {
                busy = true;
                card.rotation = Quaternion.Euler(0, 0, r);
                r -= rotSpeed;
                yield return new WaitForEndOfFrame();
            }

            card.gameObject.GetComponent<Card>().open = false;
            card.gameObject.GetComponent<Card>().busy = false;
            busy = false;
        }
    }

    private IEnumerator FlipCard360(Transform card)
    {
        busy = true;
        card.gameObject.GetComponent<Card>().busy = true;
        int r = (int)card.eulerAngles.z;
        int rTo = r + 360;
        while (r <= rTo)
        {
            busy = true;
            card.rotation = Quaternion.Euler(0, 0, r);
            r += rotSpeed;
            yield return new WaitForEndOfFrame();
        }

        card.gameObject.GetComponent<Card>().open = false;
        card.gameObject.GetComponent<Card>().busy = false;
        busy = false;
    }
}

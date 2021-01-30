using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int id;
    public int x;
    public int y;
    public bool iAmPath = false;
    public bool iAmGoal = false;

    public enum CardType { evil, good, neutral, final };
    public CardType type = CardType.evil;

    public bool open;
    public bool busy;
    public int hpDmg = 2;
    public int score = 0;
    public Material redTesting;

    GameObject model;

    private void Start()
    {
        model ??= transform.GetChild(0).gameObject;
    }

    public void setXY(int xInList, int yInList)
    {
        x = xInList;
        y = yInList;
    }

    public void SetToCard(GameObject newCard, int dmg, int point, bool path, bool final, int xInList, int yInList)
    {
        x = xInList;
        y = yInList;
        if (hpDmg > 0)
            type = CardType.neutral;
        else if (hpDmg == 0)
            type = CardType.neutral;
        else
            type = CardType.evil;

        iAmPath = path;
        iAmGoal = final;
        hpDmg = dmg;
        score = point;
        StartCoroutine(swapType(newCard));
    }

    public void SetToCard(GameObject newCard, int dmg, int point, bool path, bool final)
    {
        if (hpDmg > 0)
            type = CardType.neutral;
        else if (hpDmg == 0)
            type = CardType.neutral;
        else
            type = CardType.evil;

        iAmPath = path;
        iAmGoal = final;
        hpDmg = dmg;
        score = point;
        StartCoroutine(swapType(newCard));
    }

    IEnumerator swapType(GameObject newCardModel)
    {
        GameObject model = transform.GetChild(0).gameObject;
        Destroy(model);
        yield return null;
        GameObject newModel = Instantiate(newCardModel, transform.position, newCardModel.transform.rotation);
        newModel.transform.parent = transform;
    }

    public void ChangeColor()
    {
        /*
        Material[] mats = transform.GetChild(0).GetComponent<Renderer>().materials;
        mats[0] = redTesting;
        mats[1] = redTesting;
        mats[2] = redTesting;
        transform.GetChild(0).GetComponent<Renderer>().materials = mats;
    */
    }

}

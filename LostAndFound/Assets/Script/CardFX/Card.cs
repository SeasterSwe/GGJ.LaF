using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int id;
    int x;
    int y;
    public bool iAmPath = false;
    public bool open;
    public bool busy;
    public int hpDmg = 2;
    public Material redTesting;

    GameObject model;

    private void Start()
    {
        model ??= transform.GetChild(0).gameObject;
    }

    public void SetToCard(GameObject newCard, int dmg, bool path)
    {
        iAmPath = path;
        hpDmg = dmg;
        StartCoroutine(swapType(newCard));       
    }

    IEnumerator swapType(GameObject newCardModel)
    {
        model ??= transform.GetChild(0).gameObject;
        Destroy(model);
        yield return null;
        model = Instantiate(newCardModel, transform.position, transform.rotation);
        model.transform.parent = transform;
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

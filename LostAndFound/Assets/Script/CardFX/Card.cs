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
    public void ChangeColor()
    {

        Material[] mats = transform.Find("card").GetComponent<Renderer>().materials;
        mats[0] = redTesting;
        mats[1] = redTesting;
        mats[2] = redTesting;
        transform.Find("card").GetComponent<Renderer>().materials = mats;

    }
}

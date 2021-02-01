using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSound : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.Play("Menu");
        print("Im alive");
    }
}

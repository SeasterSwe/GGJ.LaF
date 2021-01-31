using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackroundSound : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.Play("ThunderAndRain");
    }
}

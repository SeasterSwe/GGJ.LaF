using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public string soundClip;
    void Start()
    {
        AudioManager.instance.Play(soundClip);
        // Kopiera denna l�nk f�r att f� ett ljud att spela upp. S�tt d�r den ska sitta hos "fireball hit". Soundclip - �ndra de namnet f�r att f� ljudet att funka.
    }
}

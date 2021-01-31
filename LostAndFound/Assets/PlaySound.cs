using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public string soundClip;
    void Start()
    {
        AudioManager.instance.Play(soundClip);
        // Kopiera denna länk för att få ett ljud att spela upp. Sätt där den ska sitta hos "fireball hit". Soundclip - ändra de namnet för att få ljudet att funka.
    }
}

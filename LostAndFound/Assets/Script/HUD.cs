using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public GameState gm;
    public TextMeshProUGUI playerTxtHolder;
    public TextMeshProUGUI scoreTxtHolder;
    // Start is called before the first frame update
    public void UpdatePlayerText(string text)
    {
        playerTxtHolder.text = text;
    }
    /*
    public void AddPlayerScore(int num)
    {
        gm.plScore += num;
        UpdatePlayerScore("Score : " + gm.plScore);
    }
    */
    public void UpdateHiScore(string text)
    {
        scoreTxtHolder.text = text;
    }
}

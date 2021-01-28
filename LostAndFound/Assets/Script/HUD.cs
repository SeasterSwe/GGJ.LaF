using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public TextMeshProUGUI playerTxtHolder;
    // Start is called before the first frame update
    public void UpdatePlayerText(string text)
    {
        playerTxtHolder.text = text;
    }

}

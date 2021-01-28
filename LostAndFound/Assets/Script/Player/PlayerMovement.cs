using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //cardWidth + spaceBetweenCards
    //cardHeight + spaceBetweenCards
    public GameState gm;

    public int xPos;
    public int yPos;
    public int maxYpos;
    public int maxXpos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(0, -1);
        }
    }
    void Move(int x, int z)
    {
        if(xPos + x > maxXpos || xPos + x < 0)
        {
            x = 0;
        }

        if(yPos + z > maxYpos || yPos + z < 0)
        {
            z = 0;
        }

        Card card = MapGenerator.cards[xPos + x, yPos + z].GetComponent<Card>();

        gm.cardShuffle.FlipThisCard(card.transform);
        if (card.iAmPath)
        {
            xPos += x;
            yPos += z;
//            xPos = Mathf.Clamp(xPos, 0, maxXpos - 1);
//            yPos = Mathf.Clamp(yPos, 0, maxYpos - 1);
            transform.position = MapGenerator.cards[xPos, yPos].transform.position;
        }
        else
        {
            gm.plStats.TakeDamage(card.hpDmg);
        }
    }

    void GetCard()
    {

    }
}

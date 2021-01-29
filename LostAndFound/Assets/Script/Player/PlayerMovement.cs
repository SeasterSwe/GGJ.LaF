using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //cardWidth + spaceBetweenCards
    //cardHeight + spaceBetweenCards
    public GameState gm;
    public GameObject particle;

    public int xPos;
    public int zPos;
    public int maxZpos;
    public int maxXpos;

    public bool locked;

    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        if (!gm.IsBusy())
        {
            if (locked == true)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Move(0, 0);
                    locked = false;
                    return;
                }
            }

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
    }

    public void SetPlayerPosition(int x, int z, int maxX, int maxZ, bool freeze)
    {
        xPos = x;
        zPos = z;
        maxXpos = maxX -1;
        maxZpos = maxZ -1;
        locked = freeze;

        transform.position = MapGenerator.cards[xPos, zPos].transform.position;
        Instantiate(particle, transform.position, particle.transform.rotation);
        transform.position -= Vector3.forward * gm.mapGen.cardHeight;
    }
    void Move(int x, int z)
    {
        if (xPos + x > maxXpos || xPos + x < 0)
        {
            x = 0;
            CheckPlayerCard(0, z);
            return;
        }

        if (zPos + z > maxZpos || zPos + z < 0)
        {
            z = 0;
            CheckPlayerCard(x, 0);
            return;
        }
        CheckPlayerCard(x, z);
    }
    void CheckPlayerCard(int x, int z)
    {
        Card card = MapGenerator.cards[xPos + x, zPos + z].GetComponent<Card>();


        //Card attrebuts affects player
        gm.cardShuffle.FlipThisCardOpen(card.transform);
        gm.plStats.TakeDamage(card.hpDmg);

        if (card.iAmPath)
        {
            xPos += x;
            zPos += z;
            transform.position = MapGenerator.cards[xPos, zPos].transform.position;

            gm.plStats.AddPlayerScore(card.score);

            if (card.iAmGoal) ///Win the game
            {
                gm.hud.playerTxtHolder.text = "YOU WIN - FINISH HER!";
                Instantiate(gm.WinFX, transform.position, Quaternion.identity);
                print("YOU WIN - FINISH HER!");
                gm.StartGoalSecquence();
            }
        }
    }
}

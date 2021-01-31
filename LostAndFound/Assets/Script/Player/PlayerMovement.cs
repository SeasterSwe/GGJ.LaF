using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    //cardWidth + spaceBetweenCards
    //cardHeight + spaceBetweenCards
    public GameState gm;
    public GameObject particle;

    public float moveSpeed = 0.3f;
    public int xPos;
    public int zPos;
    public int maxZpos;
    public int maxXpos;

    public bool locked;

    [HideInInspector] public bool FirstMoveOfTheDay = true;
    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {

        if (!gm.IsBusy() && canMove)
        {
            if (locked == true)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow)
                    || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Move(0, 0);
                    locked = false;
                    return;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    Move(0, 1);
                }

                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    Move(1, 0);
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    Move(-1, 0);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    Move(0, -1);
                }
            }
        }
    }

    public void SetPlayerPosition(int x, int z, int maxX, int maxZ, bool freeze)
    {
        xPos = x;
        zPos = z;
        maxXpos = maxX - 1;
        maxZpos = maxZ - 1;
        locked = freeze;

        transform.position = MapGenerator.cards[xPos, zPos].transform.position;
        transform.position -= Vector3.forward * gm.mapGen.cardHeight;
        transform.Find("Trail").GetComponent<TrailRenderer>().Clear();
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
        StartCoroutine(CheckPlayerCard(x, z));
    }
    bool canMove = true;
    Card card;
    float normalY;
    IEnumerator CheckPlayerCard(int x, int z)
    {
        canMove = false;
        card = MapGenerator.cards[xPos + x, zPos + z].GetComponent<Card>();


        //Card attrebuts affects player
        gm.cardShuffle.FlipThisCardOpen(card.transform);
        gm.plStats.TakeDamage(card.hpDmg);

        while (card.busy)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        if (card.iAmPath)
        {
            xPos += x;
            zPos += z;
            transform.DOMoveX(MapGenerator.cards[xPos, zPos].transform.position.x, moveSpeed).SetEase(Ease.InOutQuad);
            transform.DOMoveZ(MapGenerator.cards[xPos, zPos].transform.position.z, moveSpeed).SetEase(Ease.InOutQuad);
            normalY = transform.position.y;
            transform.DOMoveY(transform.position.y + 2, moveSpeed * 0.5f).SetEase(Ease.OutQuad).OnComplete(JumpDown);
        }
        else
        {
            canMove = true;
        }
    }

    void Complete()
    {
        if (card.givenPoint == false)
        {
            gm.plStats.AddPlayerScore(card.score);
            card.givenPoint = true;
        }

        Instantiate(particle, transform.position + (Vector3.up * 0.5f), particle.transform.rotation);
        if (card.iAmGoal) ///Win the game
        {
            Instantiate(gm.WinFX, transform.position, Quaternion.identity);
            print("YOU WIN - FINISH HER!");
            gm.StartGoalSecquence();
        }
        canMove = true;
    }
    void JumpDown()
    {
        transform.DOMoveY(normalY, moveSpeed * 0.5f).SetEase(Ease.InQuad).OnComplete(Complete);
    }
    public void MoveOutOfScreen()
    {
        transform.position = Vector3.down * -100;
        transform.Find("Trail").GetComponent<TrailRenderer>().Clear();
    }
}

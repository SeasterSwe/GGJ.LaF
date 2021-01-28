using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    GameObject[,] cards;
    public GameObject card;
    public int cardsX;
    public int cardsY;

    float cardWidth = 2f;
    float cardHeight = 3f;
    float spaceBetweenCards = 0.3f;

    void Start()
    {
        MakeGrid(cardsX, cardsY);
    }

    void MakeGrid(int xSize, int ySize)
    {
        cards = new GameObject[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float xPos = x * (cardWidth + spaceBetweenCards);
                float zPos = y * (cardHeight + spaceBetweenCards);
                Vector3 spawnPos = new Vector3(xPos, 0, zPos);
                cards[x, y] = Instantiate(card, spawnPos, card.transform.rotation);
                cards[x, y].SetActive(false);
            }
        }
        StartCoroutine(GeneratePath(cardsX, cardsY));
    }

    IEnumerator GeneratePath(int xSize, int ySize)
    {
        int startCardX = Random.Range(0, xSize);
        int startY = 0;
        cards[startCardX, startY].SetActive(true);

        int currentY = startY;
        int currentX = startCardX;

        while (currentY != ySize)
        {
            int direction = Random.Range(0, 3);
            switch (direction)
            {
                case 0:
                    currentX -= 1;
                    if (currentX < 0)
                    {
                        currentX += 1;
                        continue;
                    }
                    break;
                case 1:
                    currentX += 1;
                    if (currentX >= xSize)
                    {
                        currentX -= 1;
                        continue;
                    }
                    break;
                default:
                    currentY += 1;
                    break;
            }

            if (currentY >= ySize)
                break;

            cards[currentX, currentY].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForEndOfFrame();
    }
    //bredd antal kort
    //höjd antal kort

    //sätta start kort i base

    //loop
    //ska jag gå fram? ja/nej
    //annars sida? om kant gå andra sida
    //quit loop om höjd är höjd

    //future
    //gå höger 1-2 gånger
    //gå vänster 


}

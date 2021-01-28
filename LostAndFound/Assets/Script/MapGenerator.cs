using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameState gm;
    public static GameObject[,] cards;
    public GameObject card;
    public GameObject goodCard;
    public GameObject player;
    public int cardsX;
    public int cardsY;

    public float flipTime;

    float cardWidth = 2f;
    float cardHeight = 3f;
    float spaceBetweenCards = 0.3f;

    /// <summary>
    /// Robbans test
    /// </summary>
    List<Transform> path = new List<Transform>();

    [Header("Card types")]
    public GameObject evilCardPreFab;
    public GameObject neutralCardPreFab;
    public GameObject goodCardPreFab;
    public GameObject finalCardPreFab;

    public void ResetMap(int level)
    {
        StartCoroutine(EraseMap(level));
    }

    IEnumerator EraseMap(int level)
    {
        for (int y = 0; y < cardsY; y++)
        {
            for (int x = 0; x < cardsX; x++)
            {
                Destroy(cards[x, y]);
            }
            yield return new WaitForSeconds(0.5f);
        }

        StartGenerating(level);
    }

    public void StartGenerating(int difficultyLevel)
    {
        cardsY = difficultyLevel;
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
                cards[x, y].GetComponent<Card>().SetToCard(evilCardPreFab, -2, false, false);
            }
        }
        StartCoroutine(GeneratePath(cardsX, cardsY));
    }

    private void GenerateAPath()
    {
        int itt = 0;
        int y = 0;
        int x = (int)(cardsX * 0.5f);
        List<GameObject> pathRoute = new List<GameObject>();
        while (y < cardsY - 1)
        {
            itt++;
            if (Random.Range(0, 10) > 5)
            {
                y++;
            }
            else
            {
                if (Random.Range(0, 10) > 5 && x + 1 < cardsX)
                {
                    if (x + 1 < cardsX)
                        x++;
                    else
                        x--;
                }
                else
                {
                    if (x - 1 > -1)
                        x--;
                    else
                        x++;
                }
            }
            print("y : " + y + " itt : " + itt);
            pathRoute.Add(cards[x, y]);
        }


        foreach (GameObject o in pathRoute)
        {
            o.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
        }

    }

    IEnumerator GeneratePath(int xSize, int ySize)
    {
        yield return null;
        int startCardX = Random.Range(2, xSize - 2);
        int startY = 0;
        cards[startCardX, startY].GetComponent<Card>().iAmPath = true;
        ChangeColor(startCardX, startY);

        var playerMove = player.GetComponent<PlayerMovement>();
        playerMove.xPos = startCardX;
        playerMove.yPos = startY;
        player.transform.position = cards[startCardX, startY].transform.position + (Vector3.up * 0.01f);
        playerMove.maxXpos = xSize;
        playerMove.maxYpos = ySize;

        int currentY = startY;
        int currentX = startCardX;

        while (currentY != ySize)
        {
            int direction = Random.Range(0, 3);
            switch (direction)
            {
                case 0:
                    if (currentX - 1 < 0) //boardercheck
                        continue;

                    if (cards[currentX - 1, currentY].GetComponent<Card>().iAmPath == true) //kollar om korter dir �r true
                        continue;

                    currentX -= 1; //g�r h�ger
                    MakePathCardAndSpin(currentX, currentY);
                    break;

                case 1:
                    if (currentX + 1 >= xSize - 1) //boardercheck
                        continue;

                    if (cards[currentX + 1, currentY].GetComponent<Card>().iAmPath == true) //kollar om korter dir �r true
                        continue;

                    currentX += 1; //g�r h�ger
                    MakePathCardAndSpin(currentX, currentY);
                    break;

                default:
                    currentY += 1; //g�r upp
                    if (currentY >= ySize)
                        break;

                    MakePathCardAndSpin(currentX, currentY);

                    bool leftDown = (currentX == 0) ? false : cards[currentX - 1, currentY - 1].GetComponent<Card>().iAmPath;
                    bool rightDown = (currentX == xSize - 1) ? false : cards[currentX + 1, currentY - 1].GetComponent<Card>().iAmPath;

                    if (leftDown)
                    {
                        currentY += 1; //g�r upp
                        if (currentY >= ySize)
                            break;

                        MakePathCardAndSpin(currentX, currentY);
                    }
                    else if (rightDown)
                    {
                        currentY += 1; //g�r upp
                        if (currentY >= ySize)
                            break;

                        MakePathCardAndSpin(currentX, currentY);
                    }
                    break;
            }

            if (currentY >= ySize)
            {
                //last card aka win card
                ChangeColor(currentX, currentY - 1);
                break;
            }

            yield return new WaitForSeconds(flipTime);
        }

        yield return new WaitForEndOfFrame();

        StartCoroutine(ClosePath());
    }

    void ChangeColor(int x, int y)
    {
        cards[x, y].GetComponent<Card>().ChangeColor();
        cards[x, y].GetComponent<Card>().SetToCard(finalCardPreFab, 5, true, true);
    }

    void MakePathCardAndSpin(int x, int y)
    {
        path.Add(cards[x, y].transform);
        //cards[x, y].GetComponent<Card>().iAmPath = true;
        cards[x, y].GetComponent<Card>().SetToCard(neutralCardPreFab, 0, true, false);

        gm.cardShuffle.FlipThisCardOpen(cards[x, y].transform);
    }

    IEnumerator ClosePath()
    {
        yield return new WaitForSeconds(1);
        int i = 0;
        while (i < path.Count)
        {
            gm.cardShuffle.FlipThisCardClose(path[i]);
            i++;
            yield return new WaitForSeconds(0.25f);
        }
    }

    //bredd antal kort
    //h�jd antal kort

    //s�tta start kort i base

    //loop
    //ska jag g� fram? ja/nej
    //annars sida? om kant g� andra sida
    //quit loop om h�jd �r h�jd

    //future
    //g� h�ger 1-2 g�nger
    //g� v�nster 

    //ist�llet f�r else m�jligen
    //else if (currentY != 0 && cards[currentX - 1, currentY - 1].GetComponent<Card>().iAmPath || cards[currentX + 1, currentY - 1].GetComponent<Card>().iAmPath)
    //{
    //    currentY += 1; //g�r upp
    //    if (currentY >= ySize)
    //        break;

    //    MakePathCardAndSpin(currentX, currentY);
    //}

}

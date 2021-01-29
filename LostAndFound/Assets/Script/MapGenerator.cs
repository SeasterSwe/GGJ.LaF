using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapGenerator : MonoBehaviour
{
    public GameState gm;
    public static GameObject[,] cards;
    public int cardsX;
    public int cardsY;

    public GameObject card;
    public GameObject goodCard;
    public GameObject player;

    public float flipTime;

    float cardWidth = 2f;
    public float cardHeight = 3f;
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
        while (gm.IsBusy())
        {
            yield return null;
        }

        gm.SetBusy(true, "Eraseing Map");
        path = new List<Transform>();
        for (int y = 0; y < cardsY; y++)
        {
            for (int x = 0; x < cardsX; x++)
            {
                cards[x, y].transform.DOScale(Vector3.zero, 0.1f);
                yield return new WaitForSeconds(0.1f);
                Destroy(cards[x, y]);
            }
           // yield return new WaitForSeconds(flipTime);
        }
        yield return null;

       StartGenerating(level);
    }

    public void StartGenerating(int difficultyLevel)
    {
        cardsY = difficultyLevel;
        MakeGrid(cardsX, cardsY);
    }

    void MakeGrid(int xSize, int ySize)
    {
        gm.SetBusy(true, "Building Level");

        cards = new GameObject[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float xPos = x * (cardWidth + spaceBetweenCards);
                float zPos = y * (cardHeight + spaceBetweenCards);
                Vector3 spawnPos = new Vector3(xPos, 0, zPos);
                cards[x, y] = Instantiate(card, spawnPos, card.transform.rotation);
                cards[x, y].GetComponent<Card>().SetToCard(evilCardPreFab.transform.GetChild(0).gameObject, -2, false, false, x,y);
            }
        }
        //StartCoroutine(GeneratePath(cardsX, cardsY));

        StartCoroutine(TakeABreak());
    }

    //Roberts Test
    IEnumerator TakeABreak()
    {
        yield return null;
        GenerateAPath();
    }
    private void GenerateAPath()
    {
        int r = Random.Range(0, 10);
        int y = 0;
        int x = (int)(cardsX * 0.5f);
        
        //First card = Princess
        //Create a list of following pathRoute
        GameObject firstCard = cards[x, y];
        List<GameObject> pathRoute = new List<GameObject>();
        pathRoute.Add(firstCard);

        while (y < cardsY - 1)
        {
            if (Random.Range(0, 10) > 5)
            {
                y++;
                r = Random.Range(0, 10);
            }
            else
            {
                if (r > 5)
                {
                    if (x + 1 < cardsX)
                        x++;
                    else
                    {
                        y++;
                        r = Random.Range(0, 10);
                    }
                }
                else
                {
                    if (x - 1 > -1)
                        x--;
                    else
                    {
                        y++;
                        r = Random.Range(0, 10);
                    }
                }
            }
            pathRoute.Add(cards[x, y]);
        }

        //Convert to array and set card propetys
        GameObject[] listofPath = pathRoute.ToArray();
        listofPath[0].GetComponent<Card>().SetToCard(finalCardPreFab.transform.GetChild(0).gameObject, 0, true, true);
        for (int i = 1; i < listofPath.Length; i++)
        {
            listofPath[i].GetComponent<Card>().SetToCard(neutralCardPreFab.transform.GetChild(0).gameObject, 0, true, false);
        }

        //Start the fun stuff
        StartCoroutine(OpenPath(listofPath));
    }

    IEnumerator OpenPath(GameObject[] listOfPath)
    {
        for (int y = 0; y < listOfPath.Length; y++)
        {
            gm.cardShuffle.FlipThisCardOpen(listOfPath[y].transform);
            yield return new WaitForSeconds(flipTime);
        }

        Card PrincessCard = listOfPath[0].GetComponent<Card>();

        int x = 0;
        for (int y = 1; y < listOfPath.Length; y++)
        {
            Card otherCard = listOfPath[y].GetComponent<Card>();
            int swapX = otherCard.x;
            int swapY = otherCard.y;

            otherCard.setXY(PrincessCard.x, PrincessCard.y);
            cards[PrincessCard.x, PrincessCard.y] = listOfPath[y];
            PrincessCard.setXY(swapX, swapY);
            cards[swapX, swapY] = PrincessCard.gameObject;

            gm.cardShuffle.SwapCardOneWithTwo(listOfPath[0].transform, listOfPath[y].transform);
            while (listOfPath[0].GetComponent<Card>().busy)
            {
                yield return null;
            }
            x = y;
        }

        for (int y = listOfPath.Length - 1; y > -1; y--)
        {
            gm.cardShuffle.FlipThisCardClose(listOfPath[y].transform);
            yield return new WaitForSeconds(flipTime);
        }

        //cards = SortGridByPlace(cards);


        gm.plMove.SetPlayerPosition((int)(cardsX * 0.5f),0 , cardsX, cardsY, true);

        gm.SetBusy(false, "Level Compleated");
    }

    //SÅ FUKKI@ J#VLA CLEAN CODE Alltså........
    GameObject[,] SortGridByPlace(GameObject[,] oldGrid)
    {
        GameObject[,] newGrid = new GameObject[cardsX, cardsY];
        for(int x = 0; x < cardsX; x++)
        {
            for(int y = 0; y < cardsY; y++)
            {
                int newX = (int)(oldGrid[x, y].transform.position.x / cardWidth);
                int newY = (int)(oldGrid[x, y].transform.position.z / cardHeight);
                newGrid[newX, newY] = oldGrid[x,y];
                cards[x, y].GetComponent<Card>().x = newX;
                cards[x, y].GetComponent<Card>().y = newY;
            }
        }
        return newGrid;
    }
    //Roberts test slut//
    IEnumerator GeneratePath(int xSize, int ySize)
    {
        yield return null;
        int startCardX = Random.Range(2, xSize - 2);
        int startY = 0;
        // cards[startCardX, startY].GetComponent<Card>().iAmPath = true;
        // ChangeColor(startCardX, startY);

        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        playerMove.xPos = startCardX;
        playerMove.zPos = startY;
        player.transform.position = cards[startCardX, startY].transform.position + (Vector3.up * 0.01f);
        playerMove.maxXpos = xSize;
        playerMove.maxZpos = ySize;

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

                    if (cards[currentX - 1, currentY].GetComponent<Card>().iAmPath == true) //kollar om korter dir är true
                        continue;

                    currentX -= 1; //går höger
                    MakePathCardAndSpin(currentX, currentY);
                    break;

                case 1:
                    if (currentX + 1 >= xSize - 1) //boardercheck
                        continue;

                    if (cards[currentX + 1, currentY].GetComponent<Card>().iAmPath == true) //kollar om korter dir är true
                        continue;

                    currentX += 1; //går höger
                    MakePathCardAndSpin(currentX, currentY);
                    break;

                default:
                    currentY += 1; //går upp
                    if (currentY >= ySize)
                        break;

                    MakePathCardAndSpin(currentX, currentY);

                    bool leftDown = (currentX == 0) ? false : cards[currentX - 1, currentY - 1].GetComponent<Card>().iAmPath;
                    bool rightDown = (currentX == xSize - 1) ? false : cards[currentX + 1, currentY - 1].GetComponent<Card>().iAmPath;

                    if (leftDown)
                    {
                        currentY += 1; //går upp
                        if (currentY >= ySize)
                            break;

                        MakePathCardAndSpin(currentX, currentY);
                    }
                    else if (rightDown)
                    {
                        currentY += 1; //går upp
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
}

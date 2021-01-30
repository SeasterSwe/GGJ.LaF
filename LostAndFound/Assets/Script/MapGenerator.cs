using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapGenerator : MonoBehaviour
{
    public bool DestroyRTiles;
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

    int robertDatorEförSnabb; //Jakobs hjärna är för slö
    IEnumerator EraseMap(int level)
    {
        robertDatorEförSnabb = level;
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
                if (x == (cardsX - 1) && (y == cardsY - 1))
                    cards[x, y].transform.DOScale(Vector3.zero, 0.5f).OnComplete(RobertDatorÄrFörSnabbFörSittEgetBästa);
                else
                    cards[x, y].transform.DOScale(Vector3.zero, 0.5f);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    void RobertDatorÄrFörSnabbFörSittEgetBästa()
    {
        for (int y = 0; y < cardsY; y++)
        {
            for (int x = 0; x < cardsX; x++)
            {
                Destroy(cards[x, y].gameObject);
            }
        }

        StartGenerating(robertDatorEförSnabb);
    }

    public void StartGenerating(int difficultyLevel)
    {
        cardsY = difficultyLevel;
        StartCoroutine(MakeGrid(cardsX, cardsY));
    }

    IEnumerator MakeGrid(int xSize, int ySize)
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
                cards[x, y].GetComponent<Card>().SetToCard(evilCardPreFab.transform.GetChild(0).gameObject, -2,-1, false, false, x, y);
                yield return null;
                cards[x, y].gameObject.transform.localScale = Vector3.zero;
            }
            yield return new WaitForEndOfFrame();
            for (int y = 0; y < ySize; y++)
            {
                if (x == (xSize - 1) && (y == ySize - 1))
                    cards[x, y].gameObject.transform.DOScale(Vector3.one, 0.5f).OnComplete(GenerateAPath);
                else
                    cards[x, y].gameObject.transform.DOScale(Vector3.one, 0.5f);

            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    //Roberts Test
    void TakeABreak()
    {
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
        listofPath[0].GetComponent<Card>().SetToCard(finalCardPreFab.transform.GetChild(0).gameObject, 0,10, true, true);
        for (int i = 1; i < listofPath.Length; i++)
        {
            listofPath[i].GetComponent<Card>().SetToCard(neutralCardPreFab.transform.GetChild(0).gameObject, 0,1, true, false);
        }

        //Start the fun stuff
        StartCoroutine(OpenPath(listofPath));
    }

    IEnumerator OpenPath(GameObject[] listOfPath)
    {
        if(DestroyRTiles)
            StartCoroutine(DestroyStuff(cardsX, cardsY));

        for (int y = 0; y < listOfPath.Length; y++)
        {
            gm.cardShuffle.FlipThisCardOpen(listOfPath[y].transform);
            yield return new WaitForSeconds(flipTime);
        }

        Card PrincessCard = listOfPath[0].GetComponent<Card>();

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
        }

        for (int y = listOfPath.Length - 1; y > -1; y--)
        {
            gm.cardShuffle.FlipThisCardClose(listOfPath[y].transform);
            yield return new WaitForSeconds(flipTime);
        }

        gm.plMove.SetPlayerPosition((int)(cardsX * 0.5f), 0, cardsX, cardsY, true);

        gm.MapReady();
        gm.SetBusy(false, "Level Compleated");
    }

    IEnumerator DestroyStuff(int x, int y)
    {
        int amountofcards = x * y;
        int destroyAmount = Mathf.FloorToInt(amountofcards * 0.4f);
        int t = 0;
        while (t < destroyAmount)
        {
            int xRand = Random.Range(0, x);
            int yRand = Random.Range(0, y);
            Card card = cards[xRand, yRand].GetComponent<Card>();
            bool isPath = card.iAmPath;
            if(!isPath)
            {
                card.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                card.hpDmg = 0;
                t += 1;
            }
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMapGen : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
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
                // cards[x, y].GetComponent<Card>().SetToCard(evilCardPreFab.transform.GetChild(0).gameObject, -2, false, false, x, y);
            }
        }
        StartCoroutine(GeneratePath(cardsX, cardsY));
    }

    IEnumerator GeneratePath(int xSize, int ySize)
    {
        yield return new WaitForEndOfFrame();
        int startCardX = Random.Range(0, xSize);
        int startY = 0;
        // cards[startCardX, startY].GetComponent<Card>().iAmPath = true;
        // ChangeColor(startCardX, startY);

        GameObject firstCard = cards[startCardX, startY];
        List<GameObject> pathRoute = new List<GameObject>();
        pathRoute.Add(firstCard);

        //PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        //playerMove.xPos = startCardX;
        //playerMove.zPos = startY;
        //player.transform.position = cards[startCardX, startY].transform.position + (Vector3.up * 0.01f);
        //playerMove.maxXpos = xSize;
        //playerMove.maxZpos = ySize;
        gm.plMove.SetPlayerPosition(startCardX, startY, xSize, ySize, true);

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
                currentY = ySize;
                cards[currentX, currentY - 1].GetComponent<Card>().SetToCard(finalCardPreFab.transform.GetChild(0).gameObject, 0, true, true);
                gm.cardShuffle.FlipThisCard(cards[currentX, currentY - 1].transform);
                break;
            }

            yield return new WaitForSeconds(flipTime);
        }

        yield return new WaitForEndOfFrame();
        gm.SetBusy(false, "Level Compleated");
    }
    void MakePathCardAndSpin(int x, int y)
    {
        path.Add(cards[x, y].transform);
        cards[x, y].GetComponent<Card>().iAmPath = true;
        cards[x, y].GetComponent<Card>().SetToCard(neutralCardPreFab.transform.GetChild(0).gameObject, 0, true, false);
        gm.cardShuffle.FlipThisCardOpen(cards[x, y].transform);
    }
}

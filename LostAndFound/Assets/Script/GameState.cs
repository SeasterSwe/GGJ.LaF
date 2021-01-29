using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [Header("level")]
    public int level = 3;
    public int plScore = 0;


    [Header("gameState")]
    public static gameStates currentState;
    public enum gameStates { Init, shuffleDeck, Busy, Go, win };

    public bool busy;

    public MapGenerator mapGen;
    public PlayerMovement plMove;
    public ShuffleCards cardShuffle;
    public PlayerStats plStats;
    public HUD hud;

    [Header ("JSF")]
    public GameObject WinFX;

    public bool IsBusy()
    {
        return busy;
    }

    public void SetBusy(bool b)
    {
        busy = b;
    }
    public void SetBusy(bool b, string why)
    {
        print("StateHolder" + why);
        busy = b;
    }


    private void Awake()
    {
        Transform[] allObj = FindObjectsOfType<Transform>();
        foreach(Transform t in allObj)
        {
            if (t.gameObject.GetComponent<PlayerMovement>())
            {
                plMove = t.GetComponent<PlayerMovement>();
                plMove.gm = this;
            }
            if (t.gameObject.GetComponent<MapGenerator>())
            {
                mapGen = t.gameObject.GetComponent<MapGenerator>();
                mapGen.gm = this;
            }

            if (t.gameObject.GetComponent<ShuffleCards>())
            {
                cardShuffle = t.gameObject.GetComponent<ShuffleCards>();
            }

            if (t.gameObject.GetComponent<PlayerStats>())
            {
                plStats = t.gameObject.GetComponent<PlayerStats>();
                plStats.gm = this;
            }
        }


        if (!mapGen)
        {
            mapGen = GetComponentInChildren<MapGenerator>();
            mapGen.gm = this;
        }

        if (!hud)
        {
            hud = this.gameObject.GetComponent<HUD>();
            hud.gm = this;
        }
    }

    private void Start()
    {
        mapGen.StartGenerating(level);
    }

    public void StartGoalSecquence()
    {
        StartCoroutine(GoalSecquence());
    }

    IEnumerator GoalSecquence()
    {
        SetBusy(true, "Player Congrats");
        yield return new WaitForSeconds(3);
        level++;
        SetBusy(false, "Player Congrats Over");
        mapGen.ResetMap(level);
    }
}

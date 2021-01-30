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
       // print("StateHolder" + why);
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
        hud.playerTxtHolder.text = "Find the princess!" + "\n" + "Score : " + plStats.score;
        mapGen.StartGenerating(level);
        hud.UpdateHiScore("Bestest");
        plStats.HidePlayer();
    }

    public void StartGoalSecquence()
    {
        if (!IsBusy())
        {
            hud.playerTxtHolder.text = "You found the princess!" + "\n" + "Score : " + plStats.score;
            print("YOU WIN - FINISH HER!");


            StartCoroutine(GoalSecquence());
        }
    }

    IEnumerator GoalSecquence()
    {
        SetBusy(true, "Player Congrats");

        yield return new WaitForSeconds(3);

        plStats.HidePlayer();
        level++;
        hud.playerTxtHolder.text = "Level : " + (level -2) + "\n" + "Score : " + plStats.score;

        SetBusy(false, "Player Congrats Over");
        mapGen.ResetMap(level);
    }

    public void StartDeathSequence()
    {
        if (!IsBusy())
        {
            hud.UpdatePlayerText("Your journey ends here." + "\n" + "score : "+ plStats.score);
            if (plScore < plStats.score)
            {
                plScore = plStats.score;
                hud.UpdateHiScore("Bestest : " + plScore);
            }
            StartCoroutine(ResetGameIn(3));
        }
    }

    public IEnumerator ResetGameIn(float time)
    {
        plStats.HidePlayer();
        SetBusy(true, "Player Death");
        yield return new WaitForSeconds(time);
        level = 3;

        if (level > 11)
            level = 11;

        

        SetBusy(false, "Player Death Over");

        

        ResetPlayer();
        mapGen.ResetMap(level);
    }

    public void MapReady()
    {
        plStats.ShowPlayer();
        hud.playerTxtHolder.text = ("GO! Find her\n-Press UP-");
    }

    void ResetPlayer()
    {
        plStats.hp = 5;
        plStats.score = 0;
        hud.UpdatePlayerText("");
    }
}

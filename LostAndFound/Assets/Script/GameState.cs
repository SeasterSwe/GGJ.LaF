using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [Header("level")]
    public int level = 3;
    public int plScore = 0;
    public enum GameStates { Go, Busy };
    public GameStates gameState = GameStates.Go;

    public MapGenerator mapGen;
    public PlayerMovement plMove;
    public ShuffleCards cardShuffle;
    public PlayerStats plStats;
    public HUD hud;

    [Header ("JSF")]
    public GameObject WinFX;


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

    public void SetStateToGo()
    {
        gameState = GameStates.Go;
    }

    public void SetStateToBusy()
    {
        gameState = GameStates.Busy;
    }


    public void StartGoalSecquence()
    {
        StartCoroutine(GoalSecquence());
    }

    IEnumerator GoalSecquence()
    {
        yield return new WaitForSeconds(3);
        level++;
        mapGen.ResetMap(level);
    }
}

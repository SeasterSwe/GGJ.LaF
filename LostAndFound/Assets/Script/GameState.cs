using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum GameStates { Go, Busy };
    public GameStates gameState = GameStates.Go;

    public void SetStateToGo()
    {
        gameState = GameStates.Go;
    }

    public void SetStateToBusy()
    {
        gameState = GameStates.Busy;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameState gm;
    public int hp = 5;
    public int score;

    private void Start()
    {
       // gm.hud.UpdatePlayerText("Hero\n" + "HP : " + hp);
    }
    public void HidePlayer()
    {
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
    }
    
    public void ShowPlayer()
    {
       gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }

    public void AddPlayerScore(int num)
    {
        score += num;
        gm.hud.UpdatePlayerText("Hero\n" + "HP : " + hp + "\n" + "score : " + score);
    }

    public void TakeDamage(int dmg)
    {
        hp += dmg;
        gm.hud.UpdatePlayerText("Hero\n" + "HP : " + hp + "\n" + "score : " + score);
        if (hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gm.StartDeathSequence();
        print("Ur dead");
    }

    public void AddHP(int add)
    {
        hp += add;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameState gm;
    public int hp = 5;

    private void Start()
    {
        gm.hud.UpdatePlayerText("Hero\n" + "HP : " + hp);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        gm.hud.UpdatePlayerText("Hero\n" + "HP : " + hp);
        if (hp <= 0)
        {
            gm.hud.UpdatePlayerText("DEATH TO YOUUUUUUUUUUUUUuuuuuuuuuuuuuu...................!");
        }
    }

    public void AddHP(int add)
    {
        hp += add;
    }
}

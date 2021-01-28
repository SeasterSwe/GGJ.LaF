using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hp = 5;
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            print("DEATH TO YOUUUUUUUUUUUUUuuuuuuuuuuuuuu...................!");
        }
    }

    public void AddHP(int add)
    {
        hp += add;
    }
}

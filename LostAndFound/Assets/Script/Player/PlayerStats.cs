using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameState gm;
    public int hp = 5;
    public int maxHP = 5;
    public int score;
    public Wine_Death DeathFXOne;
    public GameObject deathEffekt;

    private void Start()
    {
        // gm.hud.UpdatePlayerText("Hero\n" + "HP : " + hp);
    }
    public void HidePlayer()
    {
        gameObject.GetComponentInChildren<TrailRenderer>().emitting = true;
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }

    public void ShowPlayer()
    {
        gameObject.GetComponentInChildren<TrailRenderer>().emitting = true;
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }

    public void AddPlayerScore(int num)
    {
        score += num;
        if (hp == maxHP)
        {
            hp = maxHP;
            gm.hud.UpdatePlayerText("Hero" + " HP : MAX" + "\n" + "Level : " + (gm.level - 2) + " Score : " + score);

        }
        else
        {
            gm.hud.UpdatePlayerText("Hero" + " HP : " + hp + "\n" + "Level : " + (gm.level - 2) + " Score : " + score);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp += dmg;
        if (dmg < 0)
        {
            Camera.main.GetComponent<CameraMovement>().ShakeCam();
            GameObject bloodClone = Instantiate(deathEffekt, transform.position, deathEffekt.transform.rotation);
            Destroy(bloodClone, 7f);
        }
        if (hp > maxHP)
        {
            hp = maxHP;
            gm.hud.UpdatePlayerText("Hero" + " HP : MAX\n" + "Level : " + (gm.level -2) + " Score : " + score);
        }
        else
        {
            gm.hud.UpdatePlayerText("Hero" + " HP : " + hp + "\n" + "Level : " + (gm.level - 2) + " Score : " + score);
        }
        if (hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.GetComponentInChildren<TrailRenderer>().emitting = (false);
        Instantiate(DeathFXOne, transform.position, Quaternion.identity);
        GameObject bloodClone = Instantiate(deathEffekt, transform.position, deathEffekt.transform.rotation);
        Destroy(bloodClone, 7f);

        gm.StartDeathSequence();
        print("Ur dead");
    }

    public void AddHP(int add)
    {
        hp += add;
    }
}

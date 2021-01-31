using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Thunder : MonoBehaviour
{
    public GameObject LightFx;

    public int minDelay;
    public int maxDelay;

    public float timer;

    private string thunderSound = "ThunderSound";

    void Start()
    {
        timer = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        if (timer <= 0)
        {
            StartCoroutine(CreateThunder());
            timer = Random.Range(minDelay, maxDelay);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    
    }

    IEnumerator CreateThunder()
    {
        AudioManager.instance.Play(thunderSound);
        LightFx.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        LightFx.SetActive(false);
    }
}

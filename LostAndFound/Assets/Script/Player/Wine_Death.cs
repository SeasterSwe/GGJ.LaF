using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wine_Death : MonoBehaviour
{
    public float angleSpeed = 2;
    public GameObject wineOne;
    public float waitForDestroy = 2;
    private void Start()
    {
        wineOne.transform.rotation = Quaternion.Euler(0,0,0);
        deathByWineStart();
    }
    void deathByWineStart()
    {
        StartCoroutine(deathByWine());
    }
    IEnumerator deathByWine()
    {
        float angleT = 540;
        float currentAngle = 0;

        while (currentAngle < angleT)
        {
            currentAngle += angleT * angleSpeed * Time.deltaTime * 0.5f;
            wineOne.transform.position += Vector3.up * 2.5f * angleSpeed * Time.deltaTime * 0.5f;
            wineOne.transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            yield return null;
        }
        yield return new WaitForSeconds(waitForDestroy);
        Destroy(gameObject);
    }
}

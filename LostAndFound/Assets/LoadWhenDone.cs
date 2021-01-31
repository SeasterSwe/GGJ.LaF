using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWhenDone : MonoBehaviour
{
    public float loadTime;
    void Start()
    {
        StartCoroutine(LoadAfter(loadTime));
    }
    IEnumerator LoadAfter(float t)
    {
        yield return new WaitForSeconds(t);
        GameObject.Find("Scene").GetComponent<SceneMangement>().LoadScene("Menu");
    }
}

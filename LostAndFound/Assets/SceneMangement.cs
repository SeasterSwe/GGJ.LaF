using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangement : MonoBehaviour
{
    public GameObject animation;
    public void LoadScene(string name)
    {
        StartCoroutine(LoadSceneAnimation(name));
    }
    IEnumerator LoadSceneAnimation(string name)
    {
        if (animation == null)
        {
            SceneManager.LoadScene(name);
        }
        else
        {
            animation.GetComponent<Animator>().SetTrigger("Close");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(name);
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public bool controlPanel;
    
    void Update()
    {
        if (controlPanel == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }

                else
                {
                    Pause();
                }
            }
        }


        
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Startar spelet - ej pausat men drar igång.
        //SceneMangement.LoadScene("Menu");
    }

    public void Controls()
    {
        controlPanel = !controlPanel;
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField]
    private CinemachineBrain cmb;

    [SerializeField]
    public CinemachineVirtualCamera newCam;

    public GameObject pauseMenuUI;

    public GameObject player;

    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        newCam.Priority = 1;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        player.SetActive(true);
        newCam.Priority = 1;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Debug()
    {
        cmb.ActiveVirtualCamera.Priority = 10;
        newCam.Priority = 15;
    
        player.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        

    }
}

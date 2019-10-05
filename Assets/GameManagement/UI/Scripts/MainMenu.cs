using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public void Awake()
    {
        ManagerAudio.instance.PlayTheme("menuTheme");
        
    }
    public void PlayGame()
    {
        InGameUI.GameIsPaused = false;
        ManagerAudio.instance.StopTheme("menuTheme");
        ManagerAudio.instance.PlayTheme("Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void PointerHover()
    {
        ManagerAudio.instance.Play("MouseOver");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class InGameUI : MonoBehaviour {

    // Use this for initialization
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;

    public GameObject helpers;

    public GameObject endGame;

    public RectTransform endgameComponent;

    private TMP_Text endGameText;

    private Vector2 hotSpot;

	// Update is called once per frame
	void Update () {
        if (!GameManagement.GM.playerDead && !GameManagement.GM.levelFinished)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Paused");
            if (GameIsPaused)
            {
                Resume();
            }
            else
                Pause();
        }
        
	}

    public void SetCursorVisibility(Boolean value)
    {
        Cursor.visible = value;
    }

    public void Awake()
    {

        SetCursorVisibility(false);
        Time.timeScale = 1f;
        helpers.SetActive(true);
        endGameText = (TMP_Text)endGame.GetComponentInChildren(typeof(TMP_Text));
    }

    private void Congrats()
    {
        SetCursorVisibility(true);
        helpers.SetActive(false);
        endGame.SetActive(true);
        Time.timeScale = 0f;
        endGameText.text = "Congratulations";

    }

    private void Pause()
    {
        SetCursorVisibility(true);
        pauseMenu.SetActive(true);
        helpers.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
    public void Resume()
    {
        SetCursorVisibility(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        helpers.SetActive(true);
        GameIsPaused = false;
    }
    public void FixedUpdate()
    {
        if (GameManagement.GM.playerDead)
        {
            PlayerDead();
        }
        else
        if (GameManagement.GM.levelFinished)
        {
            Congrats();
        }
    }
    public void LoadMainMenu()
    {
        SetCursorVisibility(true);
        GameManagement.GM.playerDead = false;
        GameManagement.GM.levelFinished = false;
        ManagerAudio.instance.StopTheme("Theme");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Reload()
    {
        GameManagement.GM.playerDead = false;
        GameManagement.GM.levelFinished = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PointerOver()
    {
        ManagerAudio.instance.Play("MouseOver");
    }
    public void PlayerDead()
    {
        SetCursorVisibility(true);
        helpers.SetActive(false);
        endGame.SetActive(true);
        Time.timeScale = 0f;
        GameManagement.GM.playerDead = true;
        endGameText.text = "You died";
    }
}

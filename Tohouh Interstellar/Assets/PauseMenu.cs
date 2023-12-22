using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public Color baseColor;
    public Color selectedColor;

    public List<GameObject> textObjects;

    public GameObject pauseMenu;
    public GameObject pauseMenuResumeButton;

    private bool isPaused = false;

    public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();
    }

    private void Update()
    {
        setDefaultColor();
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().color = selectedColor;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseTheGame();
        }
    }

    void setDefaultColor()
    {
        foreach (GameObject go in textObjects)
        {
            go.GetComponent<TMP_Text>().color = baseColor;
        }
    }

    void pauseTheGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        } else
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(pauseMenuResumeButton);
        }
    }

    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void restartButton()
    {
        Time.timeScale = 1.0f;
        levelLoader.loadScene("Game Scene");
        pauseMenu.SetActive(false);
    }

    public void mainMenu()
    {
        Time.timeScale = 1.0f;
        levelLoader.loadScene("Main Menu");
        pauseMenu.SetActive(false);
    }


}

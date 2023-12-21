using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Color baseColor;
    public Color selectedColor;

    public List<GameObject> textObjects;

    public GameObject mainMenu;

    public GameObject practiceMenu;
    public GameObject practiceMenuButton;
    public GameObject mainPracticeButton;

    public GameObject creditsMenu;
    public GameObject creditsBackMenuButton;
    public GameObject MainCreditsButton;

    public LevelLoader levelLoader;

    private void Update()
    {
        setDefaultColor();
        if (EventSystem.current != null)
        {
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().color = selectedColor;
        }
    }

    void setDefaultColor()
    {
        foreach (GameObject go in textObjects)
        {
            go.GetComponent<TMP_Text>().color = baseColor;
        }
    }

    public void practiceButton()
    {
        mainMenu.SetActive(false);
        practiceMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(practiceMenuButton);
    }

    public void practiceBackButton()
    {
        practiceMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainPracticeButton);
    }

    public void creditsButton()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(creditsBackMenuButton);
    }

    public void creditsBackButton()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(MainCreditsButton);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void loadGameStart()
    {
        PlayerPrefs.SetInt("Practice", 0);
        PlayerPrefs.SetInt("Boss Index", 0);
        levelLoader.loadScene("Game Scene");
    }

    public void loadPracticeCat()
    {
        PlayerPrefs.SetInt("Practice", 1);
        PlayerPrefs.SetInt("Boss Index", 0);
        levelLoader.loadScene("Game Scene");
    }

    public void loadPracticeBunnygirl()
    {
        PlayerPrefs.SetInt("Practice", 1);
        PlayerPrefs.SetInt("Boss Index", 1);
        levelLoader.loadScene("Game Scene");
    }

    public void loadPracticePlanet()
    {
        PlayerPrefs.SetInt("Practice", 1);
        PlayerPrefs.SetInt("Boss Index", 2);
        levelLoader.loadScene("Game Scene");
    }
}

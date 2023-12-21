using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    public GameObject blinkingText;
    public float startBlinkingText = 3f;

    public GameObject scoreText;

    public ScoreManager scoreManager;

    public LevelLoader levelLoader;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void OnEnable()
    {
        blinkingText.SetActive(false);
        scoreText.SetActive(false);
        StartCoroutine(blinkText());
        StartCoroutine(PressAnyKey());
    }

    IEnumerator blinkText()
    {
        yield return new WaitForSeconds(1f);
        setScoreText();

        yield return new WaitForSeconds(startBlinkingText - 1);

        while (true)
        {
            blinkingText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            blinkingText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator PressAnyKey()
    {
        yield return new WaitForSeconds(startBlinkingText);

        while (true)
        {
            if (Input.anyKeyDown)
            {
                levelLoader.loadScene("Main Menu");
            }
            yield return null;
        }
    }

    void setScoreText()
    {
        TextMeshProUGUI scoreTextMesh = scoreText.GetComponent<TextMeshProUGUI>();

        scoreText.SetActive(true);
        if (scoreManager.newHighScore)
        {
            scoreTextMesh.text = "New High Score " + scoreManager.score;
        }
        else
        {
            scoreTextMesh.text = "Score " + scoreManager.score;
        }
    }
}

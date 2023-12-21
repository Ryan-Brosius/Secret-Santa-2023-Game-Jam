using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI bonusScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;


    private void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    private void Update()
    {
        bonusScore.text = scoreManager.currentBonus.ToString("000000000");
        score.text = scoreManager.score.ToString("000000000");
        highScore.text = scoreManager.highScore.ToString("000000000");
    }
}

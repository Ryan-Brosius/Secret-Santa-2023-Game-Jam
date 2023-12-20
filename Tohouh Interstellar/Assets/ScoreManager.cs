using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;

    private List<int> Bonuses = new List<int> { 100000, 150000, 200000, 250000,
                                               300000, 350000, 400000, 500000,
                                               600000, 750000, 900000, 1000000};
    private int currentBonusIndex = 0;
    public int currentBonus = 0;
    private float tickRate = .1f;

    public void AddScore(int num)
    {
        score += num;

        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void startBonus()
    {
        StartCoroutine(bonusCalculation());
    }

    public void endBonus()
    {
        score += currentBonus;
        currentBonus = 0;
        StopCoroutine("bonusCalculation");
    }

    IEnumerator bonusCalculation()
    {
        currentBonus = Bonuses[currentBonusIndex];
        currentBonusIndex++;

        while (true && currentBonus > 0)
        {
            yield return new WaitForSeconds(tickRate);
            currentBonus -= 20;
        }

        if (currentBonus < 0)
        {
            currentBonus = 0;
        }
    }
}

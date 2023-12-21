using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public List<GameObject> bosses;
    GameObject currentBoss;
    private int currentBossIndex = 0;
    public Slider healthBar;

    private WarningText warningText;

    private bool playerIsDead = false;
    public GameObject GameOverCanvas;


    private void Start()
    {
        healthBar = FindAnyObjectByType<Slider>();
        warningText = GetComponentInChildren<WarningText>();
        StartCoroutine(makeCalls());
        healthBar.gameObject.SetActive(false);
    }

    IEnumerator makeCalls()
    {
        while (!playerIsDead)
        {
            if (currentBoss == null && currentBossIndex < bosses.Count)
            {
                //Boss is dead
                //scoreManager.endBonus();

                yield return new WaitForSeconds(3f);
                healthBar.gameObject.SetActive(false);

                warningText.gameObject.SetActive(true);
                warningText.spawnWarningScreen();

                yield return new WaitForSeconds(5f);

                warningText.gameObject.SetActive(false);

                yield return new WaitForSeconds(1.5f);

                spawnBoss(currentBossIndex);
                currentBossIndex++;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void spawnBoss(int boss)
    {
        healthBar.gameObject.SetActive(true);
        currentBoss = Instantiate(bosses[boss], new Vector3(0f, 1.9f), Quaternion.identity);
    }

    public void playerDied()
    {
        playerIsDead = true;
        GameOverCanvas.SetActive(true);
    }
}

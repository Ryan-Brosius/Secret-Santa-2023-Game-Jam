using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> bosses;
    GameObject currentBoss;
    private int currentBossIndex = 0;

    private WarningText warningText;

    private void Start()
    {
        warningText = GetComponentInChildren<WarningText>();
        StartCoroutine(makeCalls());
    }

    IEnumerator makeCalls()
    {
        while (true)
        {
            if (currentBoss == null && currentBossIndex < bosses.Count)
            {
                yield return new WaitForSeconds(3f);

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
        currentBoss = Instantiate(bosses[boss], new Vector3(0f, 1.9f), Quaternion.identity);
    }
}

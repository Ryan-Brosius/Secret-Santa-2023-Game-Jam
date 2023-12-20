using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    public GameObject blinkingText;
    public float startBlinkingText = 3f;

    private void OnEnable()
    {
        blinkingText.SetActive(false);
        StartCoroutine(blinkText());
        StartCoroutine(PressAnyKey());
    }

    IEnumerator blinkText()
    {
        yield return new WaitForSeconds(startBlinkingText);

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
                Debug.Log("Game Over Pressed Key Bruh");
            }
            yield return null;
        }
    }
}

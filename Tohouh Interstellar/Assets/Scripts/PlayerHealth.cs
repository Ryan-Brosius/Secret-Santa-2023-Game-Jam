using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float totalHealth = 4;
    public float currentHealth;
    public float colorTickTime = 0.1f;
    public bool canTakeDamage = true;
    public float invincibleFrameTime = 0.5f;
    public float freezeTime = 0.2f;
    public CameraShake shake;

    public LevelManager levelManager;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        levelManager = FindAnyObjectByType<LevelManager>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet") && canTakeDamage == true)
        {
            currentHealth -= 1;
            if(currentHealth <= 0)
            {
                //Destroy(gameObject);
                playerDied();
            }

            StartCoroutine(takeDamage());
            StartCoroutine(invincibleFrames());
        }
    }

    IEnumerator takeDamage()
    {
        Time.timeScale = 0;

        StartCoroutine(damageTick());

        StartCoroutine(shake.Shake(.15f, .03f));

        yield return new WaitForSecondsRealtime(freezeTime);

        StartCoroutine(damageTick());

        Time.timeScale = 1;
    }

    IEnumerator damageTick()
    {
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(255, 0, 0);

        yield return new WaitForSecondsRealtime(colorTickTime);

        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    IEnumerator invincibleFrames()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(invincibleFrameTime);

        canTakeDamage = true;

    }

    void playerDied()
    {
        levelManager.playerDied();
        player.SetActive(false);
    }
}

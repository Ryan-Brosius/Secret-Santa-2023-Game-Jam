using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float totalHealth = 4;
    public float currentHealth;
    public float colorTickTime = 0.1f;
    public bool canTakeDamage = true;
    public float iFrameTime = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bullet") && canTakeDamage == true)
        {
            currentHealth -= 1;
            if(currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(damageTick());
            StartCoroutine(iFrames());
        }
    }

    IEnumerator damageTick()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);

        yield return new WaitForSeconds(colorTickTime);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    IEnumerator iFrames()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(iFrameTime);

        canTakeDamage = true;

    }
}

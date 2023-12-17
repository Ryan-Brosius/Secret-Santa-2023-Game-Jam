using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public float totalHealth;
    public float colorTickTime = 0.1f;
    public Color tickColor;
    private float currentHealth;
    public bool canTakeDamage = true;
    public bool canDie = true;

    private void Start()
    {
        currentHealth = totalHealth;
    }

    public void damageEnemy(float dmg)
    {
        if (canTakeDamage)
        {
            currentHealth -= dmg;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (canDie)
                {
                    Destroy(gameObject);
                }
            }

            StartCoroutine(damageTick());
        }
    }

    IEnumerator damageTick()
    {
        gameObject.GetComponent<SpriteRenderer>().color = tickColor;

        yield return new WaitForSeconds(colorTickTime);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

    }

    public float HealthPercentage()
    {
        return currentHealth / totalHealth;
    }

    public float damageTaken()
    {
        return totalHealth - currentHealth;
    }

    public void restoreAllHealth()
    {
        currentHealth = totalHealth;
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }
}

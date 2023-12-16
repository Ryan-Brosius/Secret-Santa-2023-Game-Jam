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

    private void Start()
    {
        currentHealth = totalHealth;
    }

    public void damageEnemy(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(damageTick());
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
}

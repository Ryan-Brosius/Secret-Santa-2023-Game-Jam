using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public float totalHealth;
    public float colorTickTime = 0.1f;
    public Color tickColor;
    private float currentHealth;
    public bool canTakeDamage = true;
    public bool canDie = true;
    public GameObject healthBarUI;
    public Slider slider;
    public float test;
    private float healthbarHealth;
    private float healthbarPercentage;


    private void Start()
    {
        currentHealth = totalHealth;
        healthbarHealth = currentHealth;
        healthbarPercentage = currentHealth / totalHealth;
        slider = FindAnyObjectByType<Slider>();
        slider.value = HealthPercentage();
        //healthBarUI.SetActive(true);
        StartCoroutine(healthbarTick());
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
            slider.value = healthbarPercentage;
        }
    }

    IEnumerator healthbarTick()
    {
        while (true)
        {
            float difference = currentHealth - healthbarHealth;
            difference = Mathf.Clamp(difference, .5f, -.5f); ;
            healthbarHealth -= difference;
            healthbarPercentage = healthbarHealth / totalHealth;
            yield return new WaitForSeconds(0.01f);
            slider.value = healthbarPercentage;
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

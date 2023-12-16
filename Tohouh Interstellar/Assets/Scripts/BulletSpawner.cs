using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public float firingRate;
    private float originalFiringRate;
    public float bulletsPerSpawn;
    public float rotation;
    public float bulletSpeed;
    public bool isOn = false;
    public bool oscillateFiringRateBool = false;
    private float oscillationTimer = 0f;
    public Vector2 oscilationRange = new Vector2(0f, 0.1f);
    public float oscilateFrequency = 1f;

    private float timer;


    private void Start()
    {
        originalFiringRate = firingRate;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotation);

        if (timer >= firingRate && isOn)
        {
            fire();
            timer = 0f;
        }

        if (oscillateFiringRateBool)
        {
            oscillateFiringRate();
        } else
        {
            firingRate = originalFiringRate;
        }
    }

    void fire()
    {
        for (int i = 0; i < bulletsPerSpawn; i++)
        {
            float angle = i * 360 / bulletsPerSpawn;
            GameObject spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.transform.rotation = transform.rotation;
            spawnedBullet.transform.Rotate(0, 0, angle);
            spawnedBullet.GetComponent<EnemyBullet>().speed = bulletSpeed;
        }
    }

    void oscillateFiringRate()
    {
        oscillationTimer += Time.deltaTime / oscilateFrequency;

        float oscillationValue = Mathf.Sin(oscillationTimer) * oscilationRange.y;
        oscillationValue = Mathf.Abs(oscillationValue) + oscilationRange.x + originalFiringRate;
        firingRate = oscillationValue;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPhase2Spawner : MonoBehaviour
{
    public GameObject bullet;
    public float firingRate;
    public float bulletsPerSpawn;
    public float rotation;
    private float currentRotation = 0f;
    public float bulletSpeed;
    public float radius = 5f;
    public bool isOn = false;

    private List<GameObject> spawnedBullets = new List<GameObject>();

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotation);

        if (timer >= firingRate && isOn)
        {
            currentRotation += rotation;
            if (currentRotation > 360f)
            {
                currentRotation -= 360f;
            }
            fire();
            timer = 0f;
        }
    }

    void fire()
    {
        for (int i = 0; i < bulletsPerSpawn; i++)
        {
            float angle = (i * Mathf.PI * 2f / bulletsPerSpawn) + currentRotation;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
            GameObject spawnedBullet = Instantiate(bullet, newPos, Quaternion.identity);
            spawnedBullets.Add(spawnedBullet);
            spawnedBullet.transform.rotation = determineRotation(spawnedBullet);
            spawnedBullet.GetComponent<EnemyBullet>().speed = bulletSpeed;
        }
    }

    Quaternion determineRotation(GameObject b)
    {
        Vector3 direction = transform.position - b.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet" && spawnedBullets.Contains(col.gameObject))
        {
            spawnedBullets.Remove(col.gameObject);
            Destroy(col.gameObject);
        }
    }
}

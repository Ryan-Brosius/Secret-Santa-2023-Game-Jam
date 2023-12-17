using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class CircleBulletSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public float firingRate;
    public float bulletsPerSpawn;
    public float bulletSpeed;
    public bool isOn = false;
    public float radius = .5f;

    public float bulletWaitFireTime = 1f;
    private float timer;

    private void Start()
    {
        timer = bulletWaitFireTime;
        firingRate += bulletWaitFireTime;

        player = GameObject.Find("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= firingRate && isOn)
        {
            StartCoroutine(fire());
            timer = 0f;
        }
    }

    IEnumerator fire()
    {
        List<GameObject> bullets = new List<GameObject>();

        for (int i = 0; i < bulletsPerSpawn; i++)
        {
            float angle = i * Mathf.PI * 2f / bulletsPerSpawn;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
            GameObject spawnedBullet = Instantiate(bullet, newPos, Quaternion.identity);
            bullets.Add(spawnedBullet);

            spawnedBullet.GetComponent<EnemyBullet>().speed = 0;
        }

        yield return new WaitForSeconds(bulletWaitFireTime);

        foreach (GameObject bullet in bullets)
        {
            Vector3 direction = player.transform.position - bullet.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            bullet.transform.rotation = rotation;

            bullet.GetComponent<EnemyBullet>().speed = bulletSpeed;
        }
    }


}

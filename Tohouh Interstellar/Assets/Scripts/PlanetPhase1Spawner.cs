using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetPhase1Spawner : MonoBehaviour
{
    public GameObject player;

    public GameObject bullet;
    public float firingRate;
    public float bulletsPerSpawn;
    public float bulletSpeed;
    public Vector2 x_range;
    public bool isOn = false;

    public float attackInterval;
    private float attackIntervalTimer;
    public float attackFreeze;

    List<GameObject> spawnedBullets = new List<GameObject>();
    List<GameObject> spawnedBulletsClone = new List<GameObject>();

    private float timer;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        attackIntervalTimer += Time.deltaTime;

        if (timer >= firingRate && isOn)
        {
            fire();
            timer = 0f;
        }
    }

    private void fire()
    {
        if(attackIntervalTimer > attackInterval)
        {
            spawnedBulletsClone = new List<GameObject>(spawnedBullets);
            spawnedBullets.Clear();
            attackIntervalTimer = 0f;
            StartCoroutine(bulletsAttack());
        }

        float position = Random.Range(x_range.x, x_range.y);
        Vector3 add_pos = new Vector2(position, 0);
        GameObject spawnedBullet = Instantiate(bullet, transform.position + add_pos, Quaternion.identity);
        spawnedBullet.transform.Rotate(0, 0, -90f);
        spawnedBullet.GetComponent<EnemyBullet>().speed = bulletSpeed;
        spawnedBullets.Add(spawnedBullet);
    }

    IEnumerator bulletsAttack()
    {
        foreach (GameObject bullet in spawnedBulletsClone)
        {
            if (bullet != null)
            {
                bullet.GetComponent<EnemyBullet>().speed = 0;
                //set animation if possible
                bullet.GetComponent<Animator>().SetBool("ChasingPlayer", true);
            }
        }

        yield return new WaitForSeconds(attackFreeze);

        foreach (GameObject bullet in spawnedBulletsClone)
        {
            if (bullet != null)
            {
                Vector3 direction = player.transform.position - bullet.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                bullet.transform.rotation = rotation;

                bullet.GetComponent<EnemyBullet>().speed = bulletSpeed;
            }
        }
    }
}

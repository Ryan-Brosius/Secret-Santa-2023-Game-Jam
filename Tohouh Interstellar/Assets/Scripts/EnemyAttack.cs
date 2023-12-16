using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletSpawner;

    private void Start()
    {
        bulletSpawner = Instantiate(bulletSpawner);
        bulletSpawner.transform.SetParent(gameObject.transform);
        bulletSpawner.transform.position = transform.position;
    }

    public GameObject getBulletSpawner()
    {
        return bulletSpawner;
    }
}

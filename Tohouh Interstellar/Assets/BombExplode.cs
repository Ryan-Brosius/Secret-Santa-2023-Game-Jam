using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public float timeToExplode;
    public GameObject prefabToSpawn;
    public float prefabSpawnCount;
    public int health;

    private float timer;

    EnemyHealth eh;

    private void Start()
    {
        eh = gameObject.GetComponent<EnemyHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToExplode)
        {
            Explode();
        }

        if (eh.damageTaken() >= health)
        {
            Explode();
        }
    }

    void Explode()
    {
        for (int i = 0; i < prefabSpawnCount; i++)
        {
            float degrees = Random.Range(0, 359);
            GameObject go = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            go.transform.Rotate(0, 0, degrees);
        }
        Destroy(gameObject);
    }
}

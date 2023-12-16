using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBossPhases : MonoBehaviour
{
    public List<GameObject> phase1Spawners = new List<GameObject>();
    public GameObject bombPrefab;
    public float timeBetweenBombSpawns = 1f;
    private float bombTimer = 0f;
    public enum Phase
    {
        phase1,
        phase2
    }
    public Phase phase = Phase.phase2;

    private bool throwBombsBool = true;

    // Start is called before the first frame update
    void Start()
    {
        setUpSpawners();
    }

    void setUpSpawners()
    {
        List<GameObject> newphase1Spawners = new List<GameObject>();
        foreach (GameObject spawner in phase1Spawners)
        {
            GameObject sp = Instantiate(spawner, transform.position, Quaternion.identity);
            sp.transform.parent = gameObject.transform;
            newphase1Spawners.Add(sp);
        }
        phase1Spawners = newphase1Spawners;
    }

    // Update is called once per frame
    void Update()
    {
        determinePhase();

        Phase1();
        Phase2();
    }

    void determinePhase()
    {
        float healthPercent = gameObject.GetComponent<EnemyHealth>().HealthPercentage();

        if (healthPercent > .5)
        {
            phase = Phase.phase1;
        }
        else
        {
            phase = Phase.phase2;
        }
    }

    void Phase1()
    {
        if (phase == Phase.phase1)
        {
            turnOnSpawners(phase1Spawners);
        }
        else
        {
            turnOffSpawners(phase1Spawners);
        }
    }

    void Phase2()
    {
        if (phase == Phase.phase2)
        {
            bombTimer += Time.deltaTime;
            if (bombTimer > timeBetweenBombSpawns)
            {
                throwBombsBool = true;
                bombTimer = 0f;
            }

            if (throwBombsBool)
            {
                ThrowBombs();
                throwBombsBool = false;
            }
        }
    }

    void ThrowBombs()
    {
        float angleRange = Random.Range(-80, -45);
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.transform.Rotate(0, 0, angleRange);

        bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.transform.Rotate(0, 0, 180 + Mathf.Abs(angleRange));
    }

    void turnOnSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            s.GetComponent<BulletSpawner>().isOn = true;
        }
    }

    void turnOffSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            s.GetComponent<BulletSpawner>().isOn = false;
        }
    }
}

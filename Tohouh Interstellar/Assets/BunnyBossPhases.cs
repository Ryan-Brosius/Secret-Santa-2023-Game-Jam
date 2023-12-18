using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBossPhases : MonoBehaviour
{
    public List<GameObject> phase1Spawners = new List<GameObject>();
    public List<GameObject> phase2Spawners = new List<GameObject>();
    public List<GameObject> phase3Spawners = new List<GameObject>();
    public List<GameObject> phase4Spawners = new List<GameObject>();
    public GameObject bombPrefab;
    private float bombTimer = 0f;
    public float timeBetweenBombSpawns = 1f;
    public enum Phase
    {
        phase1,
        phase2,
        phase3,
        phase4
    }
    public Phase phase = Phase.phase2;
    public Phase previousPhase = Phase.phase1;
    private float phaseCooldown = 5f;
    private float phaseTimer = 0f;

    private bool throwBombsBool = true;

    private EnemyHealth em;

    // Start is called before the first frame update
    void Start()
    {
        setUpSpawners();
        em = GetComponent<EnemyHealth>();
        em.canDie = false;
        turnOffAllSpawners();
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

        phase2Spawners = instantiateSpawners(phase2Spawners);

        phase3Spawners = instantiateSpawners(phase3Spawners);

        phase4Spawners = instantiateSpawners(phase4Spawners);
    }

    List<GameObject> instantiateSpawners(List<GameObject> spawners)
    {
        List<GameObject> clonedSpawners = new List<GameObject>();
        foreach (GameObject spawner in spawners)
        {
            GameObject sp = Instantiate(spawner, transform.position, Quaternion.identity);
            sp.transform.parent = gameObject.transform;
            clonedSpawners.Add(sp);
        }
        return clonedSpawners;
    }

    // Update is called once per frame
    void Update()
    {
        determinePhase();

        if (phase != previousPhase)
        {
            turnOffAllSpawners();
            em.restoreAllHealth();
            phaseTimer += Time.deltaTime;
            if (phaseTimer >= phaseCooldown) {
                previousPhase = phase;
                phaseTimer = 0f;
             }
            return;
        }

        if (phase == Phase.phase1)
        {
            Phase1();
        }
        
        if (phase == Phase.phase2)
        {
            Phase2();
        }

        if (phase == Phase.phase3)
        {
            Phase3();
        }

        if (phase == Phase.phase4)
        {
            Phase4();
        }
    }

    void determinePhase()
    {
        bool isDead = em.isDead();

        if (phase == Phase.phase1 && isDead)
        {
            phase = Phase.phase2;
            return;
        }

        if (phase == Phase.phase2 && isDead)
        {
            phase = Phase.phase3;
            return;
        }

        if (phase == Phase.phase3 && isDead)
        {
            phase = Phase.phase4;
            return;
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
            turnOnSpawners(phase2Spawners);
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

    void Phase3()
    {
        turnOnSpawners(phase3Spawners);
    }

    void Phase4()
    {
        turnOnSpawners(phase4Spawners);
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
            BulletSpawner bs = s.GetComponent<BulletSpawner>();
            CircleBulletSpawner cbs = s.GetComponent<CircleBulletSpawner>();
            if (bs != null)
            {
                bs.isOn = true;
            }
            if (cbs != null)
            {
                cbs.isOn = true;
            }
        }
    }

    void turnOffSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            BulletSpawner bs = s.GetComponent<BulletSpawner>();
            CircleBulletSpawner cbs = s.GetComponent<CircleBulletSpawner>();
            if (bs != null)
            {
                bs.isOn = false;
            }
            if (cbs != null)
            {
                cbs.isOn = false;
            }
        }
    }

    void turnOffAllSpawners()
    {
        turnOffSpawners(phase1Spawners);
        turnOffSpawners(phase2Spawners);
        turnOffSpawners(phase3Spawners);
        turnOffSpawners(phase4Spawners);
    }
}

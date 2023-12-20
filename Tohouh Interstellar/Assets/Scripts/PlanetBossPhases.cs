using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetBossPhases : MonoBehaviour
{
    public List<GameObject> phase1Spawners = new List<GameObject>();
    public List<GameObject> phase2Spawners = new List<GameObject>();
    public List<GameObject> phase3Spawners = new List<GameObject>();
    public List<GameObject> phase4Spawners = new List<GameObject>();

    public enum Phase
    {
        phase1,
        phase2,
        phase3,
        phase4
    }

    public Phase phase = Phase.phase1;
    public Phase previousPhase = Phase.phase1;
    private float phaseCooldown = 5f;
    private float phaseTimer = 0f;

    private EnemyHealth em;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        scoreManager.startBonus();

        setUpSpawners();
        em = GetComponent<EnemyHealth>();
        em.canDie = false;
        turnOffAllSpawners();
    }

    private void Update()
    {
        determinePhase();

        if (phase != previousPhase)
        {
            turnOffAllSpawners();
            em.restoreAllHealth();
            phaseTimer += Time.deltaTime;
            if (phaseTimer >= phaseCooldown)
            {
                previousPhase = phase;
                phaseTimer = 0f;
                scoreManager.startBonus();
            }
            return;
        }

        if (phase == Phase.phase1)
        {
            turnOnSpawners(phase1Spawners);
        }

        if (phase == Phase.phase2)
        {
            turnOnSpawners(phase2Spawners);
        }

        if (phase == Phase.phase3)
        {
            turnOnSpawners(phase3Spawners);
        }

        if (phase == Phase.phase4)
        {
            turnOnSpawners(phase4Spawners);
        }
    }

    void determinePhase()
    {
        bool isDead = em.isDead();

        if (isDead)
        {
            scoreManager.endBonus();
        }

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

        if (phase == Phase.phase4 && isDead)
        {
            Destroy(gameObject);
        }
    }

    void setUpSpawners()
    {
        phase1Spawners = instantiateSpawners(phase1Spawners);
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

    void turnOffAllSpawners()
    {
        turnOffSpawners(phase1Spawners);
        turnOffSpawners(phase2Spawners);
        turnOffSpawners(phase3Spawners);
        turnOffSpawners(phase4Spawners);
    }

    void turnOffSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            BulletSpawner bs = s.GetComponent<BulletSpawner>();
            PlanetPhase1Spawner pp1s = s.GetComponent<PlanetPhase1Spawner>();
            PlanetPhase2Spawner pp2s = s.GetComponent<PlanetPhase2Spawner>();
            if (bs != null)
            {
                bs.isOn = false;
            }
            if (pp1s != null)
            {
                pp1s.isOn = false;
            }
            if (pp2s != null)
            {
                pp2s.isOn = false;
            }
        }
    }

    void turnOnSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            BulletSpawner bs = s.GetComponent<BulletSpawner>();
            PlanetPhase1Spawner pp1s = s.GetComponent<PlanetPhase1Spawner>();
            PlanetPhase2Spawner pp2s = s.GetComponent<PlanetPhase2Spawner>();
            if (bs != null)
            {
                bs.isOn = true;
            }
            if (pp1s != null)
            {
                pp1s.isOn = true;
            }
            if (pp2s != null)
            {
                pp2s.isOn = true;
            }
        }
    }
}

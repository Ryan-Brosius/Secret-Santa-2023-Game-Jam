using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CatBoss : MonoBehaviour
{
    EnemyHealth em;

    public GameObject eyePrefab;
    private List<GameObject> eyePrefabs = new List<GameObject>();
    public float distanceAway;
    public int numOfEyes;

    public List<GameObject> phase2Spawners = new List<GameObject>();
    public List<GameObject> phase2SpinningSpawners = new List<GameObject>();

    public List<GameObject> phase3Spawners = new List<GameObject>();

    public List<GameObject> phase4Spawners = new List<GameObject>();

    public float rotationSpeed;
    public GameObject triangle1;
    public GameObject triangle2;

    public float attackTimer = 10f;
    private float timer = 9.9f;

    private float phaseCooldown = 5f;
    private float phaseTimer = 0f;

    private ScoreManager scoreManager;

    public enum Phase
    {
        phase1,
        phase2,
        phase3,
        phase4
    }
    public Phase phase = Phase.phase1;
    public Phase previousPhase = Phase.phase1;

    private void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        scoreManager.startBonus();
        EnemyHealth em = gameObject.GetComponent<EnemyHealth>();
        em.canDie = false;
        em.canTakeDamage = false;

        triangle1 = Instantiate(triangle1, transform.position, Quaternion.identity);
        triangle2 = Instantiate(triangle2, transform.position, Quaternion.identity);

        triangle1.transform.parent = gameObject.transform;
        triangle2.transform.parent = gameObject.transform;

        List<GameObject> newphase2Spawners = new List<GameObject>();
        foreach (GameObject spawner in phase2Spawners)
        {
            GameObject sp = Instantiate(spawner, transform.position, Quaternion.identity);
            sp.transform.parent = gameObject.transform;
            newphase2Spawners.Add(sp);
        }
        phase2Spawners = newphase2Spawners;

        List<GameObject> newphase2SpinningSpawners = new List<GameObject>();
        foreach (GameObject spawner in phase2SpinningSpawners)
        {
            GameObject sp = Instantiate(spawner, transform.position, Quaternion.identity);
            sp.transform.parent = gameObject.transform;
            newphase2SpinningSpawners.Add(sp);
        }
        phase2SpinningSpawners = newphase2SpinningSpawners;

        phase3Spawners = instantiateSpawners(phase3Spawners);

        phase4Spawners = instantiateSpawners(phase4Spawners);

        SpawnEyes();
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

    void SpawnEyes()
    {
        for (int i = 0; i < numOfEyes; i++)
        {
            float radius = distanceAway;
            float angle = i * Mathf.PI * 2f / numOfEyes;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
            GameObject tempEye = Instantiate(eyePrefab, newPos, Quaternion.Euler(0, 0, 0));
            eyePrefabs.Add(tempEye);
        }
    }

    private void Update()
    {
        em = gameObject.GetComponent<EnemyHealth>();

        RotateTriangles();

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
            timer += Time.deltaTime;
            checkIfEyeDied();
            if (timer >= attackTimer)
            {
                ChooseEyesToAttack();
                timer = 0f;
            }
            if (eyePrefabs.Count == 0)
            {
                em.canTakeDamage = true;
            }
        }

        if (phase == Phase.phase2)
        {
            phase2();
        }

        if (phase == Phase.phase3)
        {
            phase3();
        }

        if (phase == Phase.phase4)
        {
            phase4();
        }
    }

    void determinePhase()
    {
        bool isDead = em.isDead();

        if (isDead)
        {
            Debug.Log("bruh2");
            scoreManager.endBonus();
        }

        if (phase == Phase.phase1 && eyePrefabs.Count == 0)
        {
            scoreManager.endBonus();
            em.canTakeDamage = true;
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

    void ChooseEyesToAttack()
    {
        foreach (GameObject eye in eyePrefabs)
        {
            eye.GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn = false;
        }

        List<GameObject> onEyes = chooseEyes();

        foreach (GameObject eye in onEyes)
        {
            eye.GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn = true;
        }
    }

    List<GameObject> chooseEyes()
    {
        List<GameObject> onEyes = new List<GameObject>();

        if (eyePrefabs.Count <= 2)
        {
            return eyePrefabs;
        }

        int count = eyePrefabs.Count;
        int bottom = count / 2;
        int top = bottom + 1;

        int Eye1 = Random.Range(0, bottom);
        int Eye2 = Random.Range(top, count);

        onEyes.Add(eyePrefabs[Eye1]);
        onEyes.Add(eyePrefabs[Eye2]);

        return onEyes;
    }

    void RotateTriangles()
    {
        Vector3 rotation = new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        triangle1.transform.Rotate(rotation);
        triangle2.transform.Rotate(-rotation);
    }

    void checkIfEyeDied()
    {
        for (int i = eyePrefabs.Count - 1 ; i >= 0; i--) {
            if (eyePrefabs[i] == null)
            {
                eyePrefabs.RemoveAt(i);
                SpeedUpEyes(5f);
                turnOnEyeIfOnEyeDied();
            }
        }
    }

    void turnOnEyeIfOnEyeDied()
    {
        int eyesOn = 0;
        foreach (GameObject eye in eyePrefabs)
        {
            if (eye != null)
            {
                if (eye.GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn == true)
                    eyesOn++;
            }
        }

        if (eyesOn == 2)
            return;

        List<GameObject> eyes = chooseEyes();

        if (eyes[0].GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn == false)
        {
            eyes[0].GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn = true;
            return;
        }
        else if (eyes[1].GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn == false)
        {
            eyes[1].GetComponent<EnemyAttack>().getBulletSpawner().GetComponent<BulletSpawner>().isOn = true;
            return;
        }
    }

    void SpeedUpEyes(float speed)
    {
        foreach (GameObject gm in eyePrefabs)
        {
            if (gm != null)
            {
                gm.GetComponent<EyeRotation>().AddRotationSpeed(speed);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject go in eyePrefabs)
        {
            Destroy(go);
        }
    }

    void phase2()
    {
        turnOnCircleSpawners(phase2Spawners);
        turnOnSpinningSpawners(phase2SpinningSpawners);
    }

    void phase3()
    {
        turnOnSpinningSpawners(phase3Spawners);
    }

    void phase4()
    {
        turnOnSpinningSpawners(phase4Spawners);
    }

    void turnOnCircleSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            s.GetComponent<CircleBulletSpawner>().isOn = true;
        }
    }

    void turnOffCircleSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            s.GetComponent<CircleBulletSpawner>().isOn = false;
        }
    }

    void turnOnSpinningSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            s.GetComponent<BulletSpawner>().isOn = true;
        }
    }

    void turnOffSpinningSpawners(List<GameObject> spawners)
    {
        foreach (GameObject s in spawners)
        {
            s.GetComponent<BulletSpawner>().isOn = false;
        }
    }

    void turnOffAllSpawners()
    {
        turnOffSpinningSpawners(phase2SpinningSpawners);
        turnOffSpinningSpawners(phase3Spawners);
        turnOffCircleSpawners(phase2Spawners);
    }
}

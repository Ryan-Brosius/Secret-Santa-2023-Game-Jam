using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoss : MonoBehaviour
{
    public GameObject eyePrefab;
    private List<GameObject> eyePrefabs = new List<GameObject>();
    public float distanceAway;
    public int numOfEyes;

    public float rotationSpeed;
    public GameObject triangle1;
    public GameObject triangle2;

    public float attackTimer = 10f;
    private float timer = 10f;

    private void Start()
    {
        triangle1 = Instantiate(triangle1, transform.position, Quaternion.identity);
        triangle2 = Instantiate(triangle2, transform.position, Quaternion.identity);

        triangle1.transform.parent = gameObject.transform;
        triangle2.transform.parent = gameObject.transform;

        SpawnEyes();
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
        timer += Time.deltaTime;

        RotateTriangles();

        checkIfEyeDied();

        if (timer >= attackTimer)
        {
            ChooseEyesToAttack();
            timer = 0f;
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
                SpeedUpEyes(3f);
            }
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
}

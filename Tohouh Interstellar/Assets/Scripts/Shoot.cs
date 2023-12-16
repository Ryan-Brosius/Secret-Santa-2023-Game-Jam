using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    public GameObject leftGun;
    public GameObject rightGun;
    public float timeBetweenShots;

    Vector3 leftGunPosition;
    Vector3 rightGunPosition;
    bool shootLeftGun = true;

    float timer = 0f;

    private void Awake()
    {
        leftGunPosition = leftGun.transform.position;
        rightGunPosition = rightGun.transform.position;
    }

    private void Update()
    {
        updateGunPositions();

        timer += Time.deltaTime;
        if (timer >= timeBetweenShots)
        {
            shootGuns();
            timer = 0f;
        }
    }

    void updateGunPositions()
    {
        leftGunPosition = leftGun.transform.position;
        rightGunPosition = rightGun.transform.position;
    }

    void shootGuns()
    {
        if (shootLeftGun)
        {
            shootLeftGun = !shootLeftGun;
            Instantiate(bulletPrefab, leftGunPosition, Quaternion.identity);
        }
        else if (!shootLeftGun)
        {
            shootLeftGun = !shootLeftGun;
            Instantiate(bulletPrefab, rightGunPosition, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotation : MonoBehaviour
{
    Vector3 parentPositon;
    public float rotationSpeed = 4f;
    private float currentRotation;

    private void Start()
    {
        parentPositon = FindObjectOfType<CatBoss>().gameObject.transform.position;
        currentRotation = rotationSpeed;
    }

    private void Update()
    {
        RotateAroundParent();
    }

    void RotateAroundParent()
    {
        parentPositon = FindObjectOfType<CatBoss>().gameObject.transform.position;
        transform.RotateAround(parentPositon, new Vector3(0, 0, 1), currentRotation * Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }

    public void AddRotationSpeed(float speed)
    {
        currentRotation += speed;
    }
}

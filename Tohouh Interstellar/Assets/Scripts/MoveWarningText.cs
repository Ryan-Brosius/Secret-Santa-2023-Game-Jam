using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWarningText : MonoBehaviour
{
    public float speed;
    private Vector3 startDistance;
    public float maxDistance;

    private void Start()
    {
        startDistance = transform.position;
    }

    private void Update()
    {
        checkToDestroy();
        transform.position += new Vector3(speed * Time.deltaTime, 0);
    }

    void checkToDestroy()
    {
        if (Vector3.Distance(transform.position, startDistance) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    float speed = 0.1f;
    float posYDestroy = -2.8f;


    private void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        if (transform.position.y < posYDestroy)
        {
            Destroy(gameObject);
        }
    }
}

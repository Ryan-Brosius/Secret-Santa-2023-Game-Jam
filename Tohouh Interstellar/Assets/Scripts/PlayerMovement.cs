using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    float horizontal;
    float vertical;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        move();
    }

    void move()
    {
        float reduceSpeed = 1f;
        if (Input.GetKey(KeyCode.X))
        {
            reduceSpeed = 0.5f;
        }

        transform.position += new Vector3(horizontal, vertical, 0) * speed * reduceSpeed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    float horizontal;
    float vertical;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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

        Vector2 dir = new Vector2(horizontal, vertical);
        dir.Normalize();

        rb.velocity = transform.TransformDirection(dir) * speed * reduceSpeed;

        //transform.position += new Vector3(horizontal, vertical, 0) * speed * reduceSpeed * Time.deltaTime;
    }
}

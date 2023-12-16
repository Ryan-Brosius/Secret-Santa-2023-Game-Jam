using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float m_Speed;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        //Set the speed of the GameObject
        m_Speed = 10.0f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("bruh");
            //Move the Rigidbody to the right constantly at speed you define (the red arrow axis in Scene view)
            transform.position += transform.right * m_Speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Move the Rigidbody to the left constantly at the speed you define (the red arrow axis in Scene view)
            transform.position += -transform.right * m_Speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //rotate the sprite about the Z axis in the positive direction
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * m_Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //rotate the sprite about the Z axis in the negative direction
            transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * m_Speed, Space.World);
        }
    }
}

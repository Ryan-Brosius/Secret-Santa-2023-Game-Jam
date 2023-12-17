using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public bool bounceOffWalls = false;
    

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        //transform.right = new Vector2(1, -1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Border")){
            if (bounceOffWalls == false)
            {
                Destroy(gameObject);
            }

            //Bounces object if touches the border
            Vector2 wallNormal = (col.ClosestPoint(transform.position) - (Vector2)transform.position).normalized;
            Vector2 incomingDirection = transform.right;
            Vector2 reflectionDirection = Vector2.Reflect(incomingDirection, wallNormal);
            transform.right = reflectionDirection;
        }
    }
}

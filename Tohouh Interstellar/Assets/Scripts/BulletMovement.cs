using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;

    float y_destroy = 3f;

    private void Update()
    {
        move();

        if (gameObject.transform.position.y >= y_destroy)
        {
            Destroy(gameObject);
        }
    }

    void move()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth eh))
        {
            eh.damageEnemy(damage);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed;
    public float time;
    public float distance;
    public int damage;
    public LayerMask WhatIsEnemy;
    public LayerMask WhatIsObstacle;
    public GameObject EnemyShotEffect;
    public GameObject ObstacleShotEffect;

    [SerializeField] bool EnemyBullet;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, WhatIsEnemy | WhatIsObstacle);
        if (hit.collider != null)
        {
            if (WhatIsEnemy == (WhatIsEnemy | (1 << hit.collider.gameObject.layer)))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                    var A = Instantiate(EnemyShotEffect, transform.position, Quaternion.identity);
                    Destroy(A.gameObject, 0.5f);
                }
                if (hit.collider.CompareTag("Player")&&EnemyBullet)
                {
                    hit.collider.GetComponent<Controller>().ChangeHealth(damage);
                    var A = Instantiate(EnemyShotEffect, transform.position, Quaternion.identity);
                    Destroy(A.gameObject,0.5f);
                }
            }
            else if (WhatIsObstacle == (WhatIsObstacle | (1 << hit.collider.gameObject.layer)))
            {
                var A = Instantiate(ObstacleShotEffect, transform.position, Quaternion.identity);
                Destroy(A.gameObject, 0.5f);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}

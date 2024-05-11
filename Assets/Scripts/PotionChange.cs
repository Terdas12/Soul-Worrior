using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionChange : MonoBehaviour
{
    public GameObject potionEffectHealth;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(potionEffectHealth, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

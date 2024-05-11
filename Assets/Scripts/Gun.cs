using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float _startTimeBtwShots;
    public GameObject bull;
    public Transform shotPoint;
    public float rotationSpeed = 5f;
    public int energyConsumptionPerShot = 1;
    [SerializeField] SpriteRenderer spriteRenderer;

    public void Rotate(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(180, 0, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    bool CanShoot = true;
    public bool TryShoot()
    {
        if (!CanShoot)
        {
            return false;
        }
        Instantiate(bull, shotPoint.position, shotPoint.rotation);
        StartCoroutine(AwaitToReload());
        return true;
    }


    private IEnumerator AwaitToReload()
    {
        CanShoot = false;
        yield return new WaitForSeconds(_startTimeBtwShots);
        CanShoot = true;
    }
}

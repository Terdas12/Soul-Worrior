using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject DeathEffect;
    public Transform AttackPos;
    public float StartTimeBtwAttack;
    public float AttackRange;
    public int damage;
    public Animator anim;
    public float attackDistanceThreshold = 1.5f;
    private Transform playerTransform;
    private float timeBtwAttack;
    private Controller _playerHealth;
    public TypeEnemy typeEnemy;
    [SerializeField] Gun gun;
    public GameObject floatingDamage;

    public enum TypeEnemy
    {
        distant,
        close
    }
    private void Start()
    {
        playerTransform = FindAnyObjectByType<Controller>().transform;
        _playerHealth = playerTransform.GetComponent<Controller>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (playerTransform == null)
        {
            // Если игрок не существует, ничего не делаем
            return;
        }

        // Проверяем, находится ли игрок в радиусе атаки
        bool playerInAttackRange = Physics2D.OverlapCircle(AttackPos.position, AttackRange, LayerMask.GetMask("Player"));

        // Если игрок мертв, прекращаем атаку
        if (_playerHealth != null && _playerHealth.health <= 0)
        {
            return;
        }
        if (playerTransform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (!playerInAttackRange && typeEnemy == TypeEnemy.distant)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            Vector2 direction = transform.position - playerTransform.position;
            direction = direction.normalized;
            gun.Rotate(direction);
            gun.TryShoot();
            anim.SetBool("IsRunning", true);
        }
        if (playerInAttackRange && typeEnemy == TypeEnemy.distant)
        {
            Vector2 direction = transform.position - playerTransform.position;
            direction = direction.normalized;
            gun.Rotate(direction);
            gun.TryShoot();
            anim.SetBool("IsRunning", false);
        }
            // Если игрок находится в радиусе атаки, атакуем
        if (playerInAttackRange && typeEnemy == TypeEnemy.close)
        {
            AttackPlayer();
            anim.SetBool("IsRunning", false);
        }
        if (!playerInAttackRange && typeEnemy == TypeEnemy.close)
        {
            // Если игрок не в радиусе атаки, двигаемся к нему
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            anim.SetBool("IsRunning", true);

        }
    }

    private void AttackPlayer()
    {
        // Проверяем время между атаками
        if (timeBtwAttack <= 0)
        {
            anim.SetTrigger("Attack");
            playerTransform.GetComponent<Controller>().ChangeHealth(damage);
            timeBtwAttack = StartTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Vector2 damagePos= new Vector2(transform.position.x, transform.position.y+2.75f);
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage=damage;
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
}

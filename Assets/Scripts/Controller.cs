using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed;
    public GameObject DeathEffect;
    private Animator animator;
    private float horizontalMoveInput;
    private float verticalMoveInput;
    private Rigidbody2D rb;
    private bool _isFireButtonPressed;
    private bool facingRight = true;
    public int health;
    public int energy;
    public int block;
    public Slider SliderHealth;
    public Slider SliderEnergy;
    public Slider SliderBlock;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private List<Gun> guns = new List<Gun>(); // Список оружия
    private Gun currentGun; // Текущее выбранное оружие
    private int currentGunIndex = 0; // Индекс текущего оружия
    private int maxWeapons = 2; // Максимальное количество оружий
    public float searchRadius = 2f; // Радиус поиска оружия
    public LayerMask weaponLayer; // Слой, на котором находится оружие
    public GameObject pickupButton;
    [SerializeField] private Gun _gunHolder;
    [SerializeField] private Image weaponImage;
    public TextMeshProUGUI _textMeshPro;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SliderHealth.maxValue = health;
        SliderEnergy.maxValue = energy;
        SliderBlock.maxValue = block;
        SliderHealth.value = health;
        SliderBlock.value = block;
        SliderEnergy.value = energy;

        // При старте активируем первое оружие
        if (guns.Count > 0)
        {
            currentGun = guns[0];
            currentGun.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (_isFireButtonPressed && energy >= currentGun.energyConsumptionPerShot)
        {
            if (currentGun.TryShoot())
            {
                energy -= currentGun.energyConsumptionPerShot; // Уменьшаем энергию на величину расхода при выстреле
                SliderEnergy.value -= currentGun.energyConsumptionPerShot;
            }
        }
    }
    private void FixedUpdate()
    {

        horizontalMoveInput = _joystick.Horizontal;
        verticalMoveInput = _joystick.Vertical;
        var direction= _joystick.Direction;
        if(direction.x < 0)
        {
            direction.x = -direction.x;
            direction.y = -direction.y;
        }
        _gunHolder.Rotate(direction);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        rb.velocity = new Vector2(horizontalMoveInput * speed, verticalMoveInput * speed);

        if (facingRight == true && horizontalMoveInput > 0 || facingRight == false && horizontalMoveInput < 0)
        {
            Flip();
        }

        if (horizontalMoveInput > 0 || horizontalMoveInput < 0 || verticalMoveInput > 0 || verticalMoveInput < 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void ChangeHealth(int healthValue)
    {
        if (block > 0)
        {
            block -= healthValue;
            SliderBlock.value = block;
        }
        else
        {
            health -= healthValue;
            SliderHealth.value = health;
        }
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }

    public void StartFire()
    {
        _isFireButtonPressed = true;
    }
    public void StopFire()
    {
        _isFireButtonPressed = false;
    }
    public void OnTriggerEnterButton()
    {
        var currentRotation=transform.localScale;
        var Scale = transform.localScale;
        Scale.x = Math.Abs(Scale.x);
        transform.localScale = Scale;
        Collider2D newGun = Physics2D.OverlapCircle(transform.position, searchRadius, weaponLayer);
        Debug.Log(newGun);
        if (newGun != null)
        {
            Gun weapon = newGun.GetComponent<Gun>();
            if(guns.Count == maxWeapons)
            {
                AddAndDropWeapon(weapon);
            }
            else
            {
                AddWeapon(weapon);
            }
        }
        transform.localScale = currentRotation;
    }

    public void SwitchWeapon()
    {
        // Переключаемся на следующее оружие
        if (guns.Count > 1)
        {
            // Отключаем текущее оружие
            currentGun?.gameObject.SetActive(false);

            // Переходим к следующему оружию в списке
            currentGunIndex++;
            if (currentGunIndex >= guns.Count)
            {
                currentGunIndex = 0;
            }

            // Выбираем следующее оружие
            currentGun = guns[currentGunIndex];
            currentGun.gameObject.SetActive(true);
            weaponImage.sprite = currentGun.GetComponent<SpriteRenderer>().sprite;
            UpdateEnergyConsumptionText();
        }
    }
    public void AddWeapon(Gun weapon)
    {
        Gun newGun = Instantiate(weapon, _gunHolder.transform.position, Quaternion.identity);
        newGun.transform.SetParent(_gunHolder.transform);
        Debug.Log(newGun.transform.localScale);
        newGun.transform.rotation = Quaternion.identity;
        newGun.GetComponent<BoxCollider2D>().enabled=false;
        if(transform.localScale.x>0)
        {
            var Scale = newGun.transform.localScale;
            Scale.x = -Math.Abs(Scale.x);
            newGun.transform.localScale = Scale;
        }

        guns.Add(newGun);
        currentGun?.gameObject.SetActive(false);
        currentGun= newGun;
        currentGunIndex = (Array.IndexOf(guns.ToArray(), currentGun) + 1)%guns.Count;
        SwitchWeapon();
        Destroy(weapon.gameObject);
    }

    public void AddAndDropWeapon(Gun weapon)
    {
        guns.Remove(currentGun);
        currentGun.transform.SetParent(null);
        var Scale= currentGun.transform.localScale;
        Scale.x = Math.Abs(Scale.x);
        currentGun.transform.localScale = Scale;
        currentGun.GetComponent<BoxCollider2D>().enabled = true;
        currentGun =null;
        AddWeapon(weapon);

    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            pickupButton.SetActive(true);
        }
        if(collision.CompareTag("Potion"))
        {
            HealthPotion(1);
        }
        if(collision.CompareTag("Armor"))
        {
            ArmorPotion(1);
        }
        if(collision.CompareTag("Energy"))
        {
            EnergyPotion(50);
        }
    }

    public void HealthPotion(int PotionHealth)
    {
        health = (int)Mathf.Clamp(SliderHealth.value+PotionHealth,0,SliderHealth.maxValue);
        SliderHealth.value = health;
    }
    public void ArmorPotion(int Potion)
    {
        block = (int)Mathf.Clamp(SliderBlock.value + Potion, 0, SliderBlock.maxValue);
        SliderBlock.value = block;
    }
    public void EnergyPotion(int Potion)
    {
        energy = (int)Mathf.Clamp(SliderEnergy.value + Potion, 0, SliderEnergy.maxValue);
        SliderEnergy.value = energy;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            pickupButton?.SetActive(false);
        } 
    }
    
    public void UpdateEnergyConsumptionText()
    {
        _textMeshPro.text = currentGun.energyConsumptionPerShot.ToString();
    }
}



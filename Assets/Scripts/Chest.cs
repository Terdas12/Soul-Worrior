using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<Item> _weaponsInChest; // Список оружия в сундуке
    public Animator animator;
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, столкнулся ли сундук с игроком и сундук еще не открыт
        if (collision.CompareTag("Player")&&!isOpen)
        {
            // Открываем сундук
            OpenChest();
        }
    }

    public void OpenChest()
    {
        // Проверка, есть ли оружие в сундуке
        if (_weaponsInChest.Count > 0)
        {
            // Получаем случайное оружие из списка
            Item randomWeapon = _weaponsInChest[Random.Range(0, _weaponsInChest.Count)];

            // Создаем экземпляр выбранного оружия
            GameObject spawnedWeapon = Instantiate(randomWeapon, transform.position, Quaternion.identity).gameObject;

            // Устанавливаем имя спавненного оружия таким же, как у оружия из списка
            spawnedWeapon.name = randomWeapon.name;

            // Ставим флаг "открытости"
            isOpen = true;

            // Запускаем анимацию открытия сундука
            animator.SetTrigger("OpenChest");
        }
        else
        {
            Debug.LogWarning("No weapons in the chest!");
        }
    }
}

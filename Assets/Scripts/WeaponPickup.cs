using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject pickupButton; // Ссылка на объект кнопки для взятия оружия


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowPickupButton(); // Показать кнопку для взятия оружия
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HidePickupButton(); // Скрыть кнопку для взятия оружия
        }
    }

    private void ShowPickupButton()
    {
        pickupButton.SetActive(true); // Показать кнопку для взятия оружия
    }

    private void HidePickupButton()
    {
        pickupButton.SetActive(false); // Скрыть кнопку для взятия оружия
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject pickupButton; // ������ �� ������ ������ ��� ������ ������


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowPickupButton(); // �������� ������ ��� ������ ������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HidePickupButton(); // ������ ������ ��� ������ ������
        }
    }

    private void ShowPickupButton()
    {
        pickupButton.SetActive(true); // �������� ������ ��� ������ ������
    }

    private void HidePickupButton()
    {
        pickupButton.SetActive(false); // ������ ������ ��� ������ ������
    }

}
